using Contracts.Position;
using DataLayer.Models;
using DataLayer.Models.Position;
using DataLayer.Repositories.Interfaces;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UserPositionService : IUserPositionService
    {
        private readonly IUserPositionRepository _userPositionRepository;

        public UserPositionService(IUserPositionRepository userPositionRepository)
        {
            _userPositionRepository = userPositionRepository;
        }

        public async Task<bool> CreateUserPositionAsync(UserPositionCreateRequest userPosition)
        {
            //var existingPosition = await _userPositionRepository.GetByIdAsync(userPosition.Id);

            //if (existingPosition is not null)
            //{
            //    return false;
            //}

            var createRequest = userPosition.ToUserPosition();

            var result = await _userPositionRepository.CreateAsync(createRequest);

            if(result is null) 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUserPositionAsync(int id)
        {
            var userPositionToDelete = await _userPositionRepository.GetByIdAsync(id);

            if (userPositionToDelete is null)
            {
                return false;
            }

            userPositionToDelete.IsDeleted = true;

            var updated = _userPositionRepository.DeleteAsync(userPositionToDelete);

            if (!updated.IsCompleted)
            {
                return false;
            }

            return true;

        }

        public async Task<List<UserPosition>> GetAllUserPositionsAsync()
        {
            return await _userPositionRepository.GetAllAsync();
        }

        public async Task<UserPosition?> GetUserPositionByIdAsync(int id)
        {
            return await _userPositionRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateUserPositionAsync(UserPositionUpdateRequest userPosition)
        {

            var position = await _userPositionRepository.GetByIdAsync(userPosition.Id);

            if(position is null)
            {
                return false;
            }

            var positionUpdate = position.ToUserPosition(userPosition);

            var updated = await _userPositionRepository.UpdateAsync(positionUpdate);

            if(updated is null)
            {
                return false;
            }

            return true;
        }
    }
}
