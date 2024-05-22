using Contracts.Request;
using DataLayer.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IUserRequestService 
    {
        public Task<List<UserRequest>> GetAllRequestsAsync();



        public Task<UserRequest?> GetUserRequestByIdAsync(int id);

        public Task<bool> CreateUserRequestAsync(ClaimsPrincipal user, UserRequestCreateRequest userRequest);

        public Task<bool> UpdateUserRequestAsync(UserRequestUpdateRequest newUserRequest);

        public Task<bool> DeleteUserRequestAsync(int id);


    }
}
