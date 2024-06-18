using Contracts.Employee;
using DataLayer.Models;
using DataLayer.Models.Pagination;
using DataLayer.Models.Position;
using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public  Task<List<Employee>> GetAllUsersAsync();

        public Task<Employee?> GetUserByIdAsync(int id);

        public Task<bool> CreateUserAsync(EmployeeCreateRequest employee,string password);

        public Task<bool> DeleteUserAsync(int id);

        public Task<bool> UpdateUserAsync(EmployeeUpdateRequest employee);

        public Task<PaginatedList<EmployeeGetResponse>> GetAllUsersAsync(EmployeeFilter filter);

        public Task<bool> CreateUsersFromOldSystem();

        public Task<bool> SignedInAsAdmin(string username);

    }
}
