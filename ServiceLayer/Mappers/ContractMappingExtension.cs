using DataLayer.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mappers
{
    public static class ContractMappingExtension
    {
        public static UserContract ToUserContract(this UserContractViewModel userContractViewModel)
        {
            return new UserContract
            {
                ContractNumber = userContractViewModel.ContractNumber,
                StartDate = userContractViewModel.StartDate,
                EndDate = userContractViewModel.EndDate,
                DateCreated = userContractViewModel.DateCreated,
                EmployeeId = userContractViewModel.EmployeeId,
            };
        }
    }
}
