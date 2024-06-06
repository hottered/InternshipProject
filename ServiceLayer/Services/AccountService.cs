using Contracts.Employee;
using DataLayer.Models;
using DataLayer.Models.Pagination;
using DataLayer.Repositories.Interfaces;
using Newtonsoft.Json;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using SharedDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly HttpClient _httpClient;
        public AccountService(
            IAccountRepository accountRepository,
            HttpClient httpClient)
        {
            _accountRepository = accountRepository;
            _httpClient = httpClient;

            Uri baseAddress = new Uri(Constants.BaseAddress);
            _httpClient.BaseAddress = baseAddress;

        }
        public async Task<bool> CreateUsersFromOldSystem()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + Constants.RandomUsers);

            if (!response.IsSuccessStatusCode)
            {
                return false;
                
            }

            string data = await response.Content.ReadAsStringAsync();

            var employees = JsonConvert.DeserializeObject<List<EmployeeCreateRequest>>(data);

            bool result = false;

            foreach(var employee in employees)
            {
                var existingUser = await _accountRepository.GetUserByEmailAsync(employee.Email);

                if (existingUser is not null)
                {
                    return false;
                }

                var employeeDto = employee.ToEmployee();

                result = await _accountRepository.CreateUserAsync(employeeDto, employee.Password);
            }
            return result;
        }
        public async Task<bool> CreateUserAsync(EmployeeCreateRequest employee, string password)
        {
            
            var existingUser = await _accountRepository.GetUserByEmailAsync(employee.Email);

            if(existingUser is not null)
            {
                return false;
            }

            var employeeToCreate = employee.ToEmployee();

            var result = await _accountRepository.CreateUserAsync(employeeToCreate, password);

            return result;
            
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var userToDelete = await _accountRepository.GetUserByIdAsync(id);

            if(userToDelete is null)
            {
                return false;
            }

            userToDelete.IsDeleted = true;
            userToDelete.DateDeleted = DateTime.UtcNow;

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

        public async Task<PaginatedList<Employee>> GetAllUsersAsync(EmployeeFilter filter)
        {
            var count = await _accountRepository.GetAllUsersCountAsync(filter);

            var usersQueryable = await _accountRepository.GetAllUsersAsync(filter);

            return await PaginatedList<Employee>.CreateAsync(usersQueryable,count, (int)filter.PageNumber!, (int)filter.PageSize!); 
        }

        public async Task<bool> UpdateUserAsync(EmployeeUpdateRequest updateRequest)
        {
            var employee = await _accountRepository.GetUserByIdAsync(updateRequest.Id);

            if(employee is null)
            {
                return false;
            }

            var employeeUpdate = employee.ToEmployee(updateRequest);

            return await _accountRepository.UpdateUserAsync(employeeUpdate);
        }
    }
}
