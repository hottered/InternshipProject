using Contracts.Position;
using DataLayer.Models.Position;
using DataLayer.Repositories.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IUserPositionRepository : IRepository<UserPosition>
    {
        public Task<bool> CreateUserPositionAsync(UserPosition userPosition);
        public Task<bool> UpdateUserPositionAsync(UserPosition userPosition);
        public Task<long> GetAllUserPositionsCountAsync(UserPositionFilter filter);
        public Task<List<UserPosition>> GetAllUserPositionsAsync(UserPositionFilter filter);


    }
}
