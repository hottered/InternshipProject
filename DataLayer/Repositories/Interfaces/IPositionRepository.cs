using DataLayer.Models.Position;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        public Task<List<UserPosition>> GetAllPositionsAsync();
        public Task<bool> CreatePositionAsync(UserPosition userPosition);
        public Task<UserPosition?> GetPositionByIdAsync(int id);
        public Task<bool> UpdatePositionAsync(UserPosition userPosition);
        public Task<bool> DeletePositionAsync(int id);
    }
}
