using Contracts.Employee;
using Contracts.Request;
using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Models;
using DataLayer.Models.Login;
using DataLayer.Models.Register;
using DataLayer.Models.Request;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;

namespace DataLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Employee> _userManager;
        private readonly AppDbContext _dbContext;
        public AccountRepository(
            UserManager<Employee> userManager,
            AppDbContext dbContext
            )
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            await transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            await transaction.RollbackAsync();
        }

        public async Task<bool> CreateUserAsync(Employee model,string password)
        {
            var result = await _userManager.CreateAsync(model,password);
            if(result.Succeeded) { return true; };
            return false;
        }
        public async Task<List<Employee>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<Employee?> GetUserByIdAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> UpdateUserAsync(Employee employee)
        {
            await _userManager.UpdateAsync(employee);
            return true;
        }
        public async Task AssignRoleAsync(Employee employee,string role)
        {
            await _userManager.AddToRoleAsync(employee, role);
        }

        public async Task<Employee?> GetUserByEmailAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<long> GetAllUsersCountAsync(EmployeeFilter filter)
        {
            return await _userManager.Users
                .Filter(filter)
                .CountAsync();
        }
        public async Task<List<Employee>> GetAllUsersAsync(EmployeeFilter filter)
        {
            return await _userManager.Users
                .Filter(filter)
                .Paginate(filter)
                .ToListAsync();
        }
    }
}
