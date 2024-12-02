using Contracts.Contract;
using DataLayer.Models.Contract;
using DataLayer.Repositories.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IContractRepository : IRepository<UserContract>
    {
        public Task<UserContract> GetContractByContractNumberAsync(string contractNumber,int EmployeeId);
        public Task<List<UserContract>> GetAllUserContractsAsync(UserContractFilter filter);
        public Task<long> GetAllUserContractsCountAsync(UserContractFilter filter);
    }
}
