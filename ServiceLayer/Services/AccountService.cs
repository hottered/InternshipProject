﻿using Contracts.Employee;
using DataLayer.Models;
using DataLayer.Models.Pagination;
using DataLayer.Repositories.Interfaces;
using Newtonsoft.Json;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using SharedDll;
using SharedDll.ApiRoutes;
using SharedDll.Enums;
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

            _httpClient.BaseAddress = new Uri(ApiRoutes.BaseAddress);

        }
        public async Task<bool> SignedInAsAdmin(string username)
        {
            var employee = await _accountRepository.GetUserByEmailAsync(username);

            return employee is null ? false : await _accountRepository.IsInRoleAsync(employee, nameof(RolesEnum.Admin));
        }
        public async Task<bool> CreateUsersFromOldSystem()
        {
            using var cts  = new CancellationTokenSource();
            try
            {
                using var response = await _httpClient.GetAsync(_httpClient.BaseAddress + ApiRoutes.RandomUsers);

                response.EnsureSuccessStatusCode();

                string data = await response.Content.ReadAsStringAsync();

                var employees = JsonConvert.DeserializeObject<List<EmployeeCreateRequest>>(data);

                if (employees == null)
                {
                    return false;
                }

                using (var transaction = await _accountRepository.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var employee in employees)
                        {
                            var existingUser = await _accountRepository.GetUserByEmailAsync(employee.Email);

                            if (existingUser is not null)
                            {
                                await _accountRepository.RollbackTransactionAsync(transaction);
                                return false;
                            }

                            var employeeDto = employee.ToEmployee();

                            var result = await _accountRepository.CreateUserAsync(employeeDto, employee.Password);
                            if (!result)
                            {
                                await _accountRepository.RollbackTransactionAsync(transaction);
                                return false;
                            }
                        }

                        await _accountRepository.CommitTransactionAsync(transaction);
                        return true;
                    }
                    catch
                    {
                        await _accountRepository.RollbackTransactionAsync(transaction);
                        throw;
                    }
                }
            }
            catch(OperationCanceledException ex) when (cts.IsCancellationRequested) 
            {
                Console.WriteLine($"Request was canceled due to : {ex.Message}");
                return false;
            }
            catch (OperationCanceledException ex) when (ex.InnerException is TimeoutException tex)
            {
                Console.WriteLine($"Request timed out: {ex.Message}, {tex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false;
            }
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

        public async Task<PaginatedList<EmployeeGetResponse>> GetAllUsersAsync(EmployeeFilter filter)
        {
            var count = await _accountRepository.GetAllUsersCountAsync(filter);

            var allUsers = await _accountRepository.GetAllUsersAsync(filter);

            var usersToResponseDto = allUsers.ToEmployeeGetResponseList();

            return await PaginatedList<EmployeeGetResponse>.CreateAsync(usersToResponseDto, count, (int)filter.PageNumber!, (int)filter.PageSize!); 
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
