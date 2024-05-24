using Contracts.Position;
using DataLayer.Models.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IUserPositionService
    {
        public Task<List<UserPosition>> GetAllUserPositionsAsync();

        public Task<UserPosition?> GetUserPositionByIdAsync(int id);
        
        public Task<bool> CreateUserPositionAsync(UserPositionCreateRequest userPosition);

        public Task<bool> DeleteUserPositionAsync(int id);

        public Task<bool> UpdateUserPositionAsync(UserPositionUpdateRequest userPosition);

        public Task<List<UserPosition>> GetUserPositionsBasedOnPage(int pageNumber);
    }
}
