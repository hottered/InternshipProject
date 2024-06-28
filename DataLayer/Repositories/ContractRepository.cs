using DataLayer.Data;
using DataLayer.Models.Contract;
using DataLayer.Repositories.GenericRepository;
using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ContractRepository : BaseRepository<UserContract>,IContractRepository
    {
        public ContractRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
