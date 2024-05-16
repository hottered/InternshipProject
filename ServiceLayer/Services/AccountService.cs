using DataLayer.Models;
using DataLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> CreateUserAsync(Employee employee, string password)
        {
            var existingUser = await _accountRepository.GetUserByIdAsync(employee.Id);

            if(existingUser != null)
            {
                return false;
            }

            var result = await _accountRepository.CreateUserAsync(employee, password);

            return result;
            
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var userToDelete = await _accountRepository.GetUserByIdAsync(id);

            if(userToDelete != null)
            {
                return false;
            }

            userToDelete.IsDeleted = true;

            var result = await _accountRepository.UpdateUserAsync(userToDelete);

            return result;

        }

        public async Task<List<Employee>> GetAllUsersAsync()
        {
            return await _accountRepository.GetAllUsersAsync();
        }

        public async Task<Employee?> GetUserByIdAsync(int id)
        {
            return await _accountRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(Employee employee)
        {
            return await _accountRepository.UpdateUserAsync(employee);
        }
    }
}
