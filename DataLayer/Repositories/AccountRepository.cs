
using DataLayer.Models;
using DataLayer.Models.Login;
using DataLayer.Models.Register;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Employee> _userManager;
        public AccountRepository(
            UserManager<Employee> userManager
            )
        {
            _userManager = userManager;
        }
        public async Task<bool> CreateUserAsync(SignUpModel model)
        {
            var user = new Employee()
            {
                Email = model.Email,
                UserName = model.Email,
                Address = ""

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) { return true; };
            return false;
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

    }
}
