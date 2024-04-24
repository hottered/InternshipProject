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
        public async Task<List<Employee>> GetAllUsersAsync()
        {
            return await _accountRepository.GetAllUsersAsync();
        }
        public async Task<Employee?> GetUserByIdAsync(int id)
        {
            return await _accountRepository.GetUserByIdAsync(id);
        }
    }
}
