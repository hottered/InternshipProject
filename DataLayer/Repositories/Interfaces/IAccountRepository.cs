using DataLayer.Models;
using DataLayer.Models.Login;
using DataLayer.Models.Register;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> CreateUserAsync(SignUpModel model);
        public Task<IdentityResult> CreateUserAsync(Employee model, string password);
        public Task<List<Employee>> GetAllUsersAsync();
        public  Task<Employee?> GetUserByIdAsync(int id);
        public Task<bool> UpdateUserAsync(Employee employee);

    }
}
