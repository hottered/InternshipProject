using DataLayer.Models.Contract;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;   
        }

        public async Task AddContractAsync(UserContractViewModel contract)
        {
            await _contractRepository.CreateAsync(contract.ToUserContract());
        }

        public Task DeleteContractAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserContract>> GetAllContractsAsync()
        {
            return await _contractRepository.GetAllAsync();
        }

        public async Task<UserContract> GetContractByIdAsync(int id)
        {
            return await _contractRepository.GetByIdAsync(id);
        }

        public async Task UpdateContractAsync(UserContract contract)
        {
            await _contractRepository.UpdateAsync(contract);
        }
    }
}
