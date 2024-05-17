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
        public Task<bool> CreateUserRequestAsync(UserRequest userRequest)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRequest>> GetAllRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserRequest?> GetUserRequestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserRequestAsync(UserRequest newUserRequest)
        {
            throw new NotImplementedException();
        }
    }
}
