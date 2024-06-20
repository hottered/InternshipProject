using Contracts.Request;
using DataLayer.Models.Pagination;
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

        public Task<bool> CreateUserRequestAsync(int userId, UserRequestCreateRequest userRequest);

        public Task<bool> UpdateUserRequestAsync(UserRequestUpdateRequest newUserRequest);

        public Task<bool> DeleteUserRequestAsync(int id);

        public Task<List<UserRequest>> GetAllRequestsForTheUserWithId(int id);

        public Task<bool> ApproveRequestByIdAsync(int id);

        public Task<PaginatedList<UserRequestGetResponse>> GetAllUserRequestsByPage(UserRequestFilter filter);

        public Task<List<UserRequest>> GetAllRequestsForUsersAsync();

        public Task<bool> RejectRequestByIdAsync(int id);

    }
}
