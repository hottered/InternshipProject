using Contracts.Contract;
using DataLayer.Models.Contract;
using DataLayer.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IContractService
    {
        public Task<bool> AddContractAsync(UserContractViewModel contract);
        public Task<UserContract> GetContractByIdAsync(int id);
        public Task<PaginatedList<UserContract>> GetAllContractsAsync(UserContractFilter filter);
        public Task UpdateContractAsync(UserContract contract);
        public Task DeleteContractAsync(int id);
    }
}
