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
        public Task SaveContractAsync(UserContract contract);
        public Task<byte[]> GetContractFileAsync(int id);
    }
}
