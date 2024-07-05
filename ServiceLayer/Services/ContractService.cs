using Contracts.Contract;
using DataLayer.Models.Contract;
using DataLayer.Models.Pagination;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
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

        public async Task<bool> AddContractAsync(UserContractViewModel contract)
        {
            var contractByNumber = await _contractRepository.GetContractByContractNumberAsync(contract.ContractNumber,contract.EmployeeId);

            if (contractByNumber is not null)
            {
                return false;
            }
            
            var result = await _contractRepository.CreateAsync(contract.ToUserContract());

            return result is not null;
        }

        public Task DeleteContractAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<UserContract>> GetAllContractsAsync(UserContractFilter filter)
        {
            var count = await _contractRepository.GetAllUserContractsCountAsync(filter);

            var contractsQueryable = await _contractRepository.GetAllUserContractsAsync(filter);

            return await PaginatedList<UserContract>.CreateAsync(contractsQueryable, count, (int)filter.PageNumber!, (int)filter.PageSize!);

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
