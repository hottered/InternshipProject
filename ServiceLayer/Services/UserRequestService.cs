using DataLayer.Models.Request;
using DataLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> CreateUserRequestAsync(UserRequest userRequest)
        {
            var existingRequest = await _userRequestRepository.GetByIdAsync(userRequest.Id);

            if (existingRequest is not null) {

                return false;

            }

            var result = await _userRequestRepository.CreateAsync(userRequest);

            if (result is not null)
            {
                return true;
            }

            return false;
        }

        public async Task<List<UserRequest>> GetAllRequestsAsync()
        {
            return await _userRequestRepository.GetAllAsync();
        }

        public async Task<UserRequest?> GetUserRequestByIdAsync(int id)
        {
            return await _userRequestRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateUserRequestAsync(UserRequest newUserRequest)
        {
            var result = await _userRequestRepository.UpdateAsync(newUserRequest);
            
            if(result is null)
            {
                return false;
            }

            return true;
        }
    }
}
