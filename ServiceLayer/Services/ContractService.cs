using DataLayer.Models.Contract;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task SaveContractAsync(UserContract contract)
        {
            if (contract.ContractPdf != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await contract.ContractPdf.CopyToAsync(memoryStream);

                    contract.ContractPdfUrl = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            await _contractRepository.CreateAsync(contract);
        }
        public async Task<byte[]> GetContractFileAsync(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);

            if (contract == null || string.IsNullOrEmpty(contract.ContractPdfUrl))
            {
                return null;
            }

            return Convert.FromBase64String(contract.ContractPdfUrl);
        }
    }
}
