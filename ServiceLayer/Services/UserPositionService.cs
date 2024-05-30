using Contracts.Position;
using DataLayer.Models;
using DataLayer.Models.Pagination;
using DataLayer.Models.Position;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            var createRequest = userPosition.ToUserPosition();

            var result = await _userPositionRepository.CreateUserPositionAsync(createRequest);

            return result;
        }

        public async Task<bool> DeleteUserPositionAsync(int id)
        {
            var userPositionToDelete = await _userPositionRepository.GetByIdAsync(id);

            if (userPositionToDelete is null)
            {
                return false;
            }

            userPositionToDelete.IsDeleted = true;

            var result = await _userPositionRepository.UpdateUserPositionAsync(userPositionToDelete);

            return result;

        }

        public async Task<List<UserPosition>> GetAllUserPositionsAsync()
        {
            return await _userPositionRepository.GetAllAsync();
        }

        public async Task<UserPosition?> GetUserPositionByIdAsync(int id)
        {
            return await _userPositionRepository.GetByIdAsync(id);
        }

        public async Task<PaginatedList<UserPosition>> GetUserPositionsBasedOnPage(string searchString, int pageNumber)
        {
            if (searchString == null)
            {
                searchString = string.Empty;
            }

            var userPositions = _userPositionRepository.GetUserPositionsQuryableFiltered(searchString, pageNumber);

            var pageSize = 3;

            return await PaginatedList<UserPosition>.CreateAsync(userPositions, pageNumber, pageSize);
           
        }


        public async Task<bool> UpdateUserPositionAsync(UserPositionUpdateRequest userPosition)
        {

            var position = await _userPositionRepository.GetByIdAsync(userPosition.Id);

            if(position is null)
            {
                return false;
            }

            var positionUpdate = position.ToUserPosition(userPosition);

            var updated = await _userPositionRepository.UpdateUserPositionAsync(positionUpdate);

            return updated;
        }
    }
}
