using DataLayer.Models;
using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public  Task<List<Employee>> GetAllUsersAsync();
        public Task<Employee?> GetUserByIdAsync(int id);

        //public Task<>
    }
}
