using Contracts.Employee;
using DataLayer.Models;
using DataLayer.Models.Login;
using DataLayer.Models.Register;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<bool> CreateUserAsync(Employee model, string password);
        public Task<List<Employee>> GetAllUsersAsync();
        public  Task<Employee?> GetUserByIdAsync(int id);
        public Task<Employee?> GetUserByEmailAsync(string username);
        public Task<bool> UpdateUserAsync(Employee employee);
        public Task AssignRoleAsync(Employee employee, string role);
        public Task<List<Employee>> GetAllUsersAsync(EmployeeFilter filter);
        public Task<long> GetAllUsersCountAsync(EmployeeFilter filter);

    }
}
