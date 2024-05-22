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
        private readonly IAccountRepository _accountRepository;
        public UserRequestService(IUserRequestRepository userRequestRepository,
            IAccountRepository accountRepository)
        {
            _userRequestRepository = userRequestRepository;
            _accountRepository = accountRepository;
        }
        public async Task<bool> CreateUserRequestAsync(ClaimsPrincipal user,UserRequestCreateRequest userRequest)
        {
            var userSignedIn = await _accountRepository.GetUserThatIsSignedInAsync(user);

            var updatedUserRequest = userRequest with { UserId = userSignedIn.Id };

            var request = updatedUserRequest.ToUserRequest();

            var result = await _userRequestRepository.CreateAsync(request);

            if (result is not null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteUserRequestAsync(int id)
        {
            var userRequestToDelete = await _userRequestRepository.GetByIdAsync(id);

            if (userRequestToDelete is null)
            {
                return false;
            }

            userRequestToDelete.IsDeleted = true;

            var updated = _userRequestRepository.DeleteAsync(userRequestToDelete);

            if (!updated.IsCompleted)
            {
                return false;
            }

            return true;
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

            var result = await _userRequestRepository.UpdateAsync(existingRequest.ToUserRequest(newUserRequest));
            
            if(result is null)
            {
                return false;
            }

            return true;
        }
    }
}
