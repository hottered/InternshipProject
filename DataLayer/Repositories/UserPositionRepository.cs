using Contracts.Position;
using Contracts.Request;
using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Models.Position;
using DataLayer.Models.Request;
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
    public class UserPositionRepository : BaseRepository<UserPosition>, IUserPositionRepository
    {
        public UserPositionRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CreateUserPositionAsync(UserPosition userPosition)
        {
            await _dbContext.Positions.AddAsync(userPosition);

            var res = await _dbContext.SaveChangesAsync();

            return res > 0;
        }
        public async Task<List<UserPosition>> GetAllUserPositionsAsync(UserPositionFilter filter)
        {

            return await _dbContext.Positions
                .Filter(filter)
                .Paginate(filter)
                .ToListAsync();
        }
        public async Task<long> GetAllUserPositionsCountAsync(UserPositionFilter filter)
        {
            return await _dbContext.Positions
                .Filter(filter)
                .CountAsync();
        }
        public async Task<bool> UpdateUserPositionAsync(UserPosition userPosition)
        {
            _dbContext.Positions.Update(userPosition);

            var res = await _dbContext.SaveChangesAsync();

            return res > 0;
        }
    }
}
