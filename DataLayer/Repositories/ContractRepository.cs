using Contracts.Contract;
using Contracts.Position;
using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Models.Contract;
using DataLayer.Models.Position;
using DataLayer.Repositories.GenericRepository;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task<UserContract> GetContractByContractNumberAsync(string contractNumber,int EmployeeId)
        {
            return await _dbContext.Contracts.Where(x => x.ContractNumber == contractNumber || x.EmployeeId == EmployeeId).FirstOrDefaultAsync();
        }
        public async Task<List<UserContract>> GetAllUserContractsAsync(UserContractFilter filter)
        {
            return await _dbContext.Contracts
                .Filter(filter)
                .Include(x=>x.Employee)
                .Paginate(filter)
                .ToListAsync();
        }
        public async Task<long> GetAllUserContractsCountAsync(UserContractFilter filter)
        {
            return await _dbContext.Contracts
                .Filter(filter)
                .CountAsync();
        }
    }
}
