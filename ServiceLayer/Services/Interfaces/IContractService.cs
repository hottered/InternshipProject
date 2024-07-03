using DataLayer.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IContractService
    {
        public Task AddContractAsync(UserContractViewModel contract);
        public Task<UserContract> GetContractByIdAsync(int id);
        public Task<List<UserContract>> GetAllContractsAsync();
        public Task UpdateContractAsync(UserContract contract);
        public Task DeleteContractAsync(int id);
    }
}
