using DataLayer.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IUserRequestRepository
    {
        Task<UserRequest> GetRequestByIdAsync(int id);
        Task<List<UserRequest>> GetAllRequestsAsync();
        Task<bool> CreateRequestAsync(UserRequest userRequest);
        Task<bool> UpdateRequestAsync(UserRequest userRequest);
        Task<bool> DeleteRequestAsync(int id);
    }
}
