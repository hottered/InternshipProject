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
        
    }
}
