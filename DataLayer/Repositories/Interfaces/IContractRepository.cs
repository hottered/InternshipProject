using DataLayer.Models.Contract;
using DataLayer.Repositories.GenericRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IContractRepository : IRepository<UserContract>
    {
        //public Task AddContractAsync(UserContract contract);
    }
}
