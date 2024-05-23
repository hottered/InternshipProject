using Contracts.Position;
using Contracts.Request;
using DataLayer.Models;
using DataLayer.Models.Request;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UserRequestService : IUserRequestService
    {
        private readonly IUserRequestRepository _userRequestRepository;
        public UserRequestService(IUserRequestRepository userRequestRepository)
        {
            _userRequestRepository = userRequestRepository;
        }
        public async Task<bool> CreateUserRequestAsync(int userId,UserRequestCreateRequest userRequest)
        {
            var updatedUserRequest = userRequest with { UserId = userId };

            var request = updatedUserRequest.ToUserRequest();

            var result = await _userRequestRepository.CreateUserRequestAsync(request);

            return result;
        }

        public async Task<bool> DeleteUserRequestAsync(int id)
        {
            var userRequestToDelete = await _userRequestRepository.GetByIdAsync(id);

            if (userRequestToDelete is null)
            {
                return false;
            }

            userRequestToDelete.IsDeleted = true;

            var updated = await _userRequestRepository.UpdateUserRequestAsync(userRequestToDelete);

            return updated;
        }

        public async Task<List<UserRequest>> GetAllRequestsAsync()
        {
            return await _userRequestRepository.GetAllAsync();
        }

        public async Task<UserRequest?> GetUserRequestByIdAsync(int id)
        {
            return await _userRequestRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateUserRequestAsync(UserRequestUpdateRequest newUserRequest)
        {

            var existingRequest = await _userRequestRepository.GetByIdAsync(newUserRequest.Id);

            if(existingRequest is null) 
            {
                return false;
            }

            var result = await _userRequestRepository.UpdateUserRequestAsync(existingRequest.ToUserRequest(newUserRequest));

            return result;
        }
    }
}
