using Contracts.Position;
using Contracts.Request;
using DataLayer.Models;
using DataLayer.Models.Pagination;
using DataLayer.Models.Request;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Services
{
    public class UserRequestService : IUserRequestService
    {
        private readonly IUserRequestRepository _userRequestRepository;
        private readonly IAccountRepository _accountRepository;
        public UserRequestService(
            IUserRequestRepository userRequestRepository
            , IAccountRepository accountRepository)
        {
            _userRequestRepository = userRequestRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<UserRequest>> GetAllRequestsForTheUserWithId(int id)
        {
            return await _userRequestRepository.AllRequestsForUserWithId(id);
        }

        public async Task<bool> CreateUserRequestAsync(int userId, UserRequestCreateRequest userRequest)
        {
            if (userRequest.StartDate >= userRequest.EndDate)
            {
                return false;
            }

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

            return existingRequest is not null
                    && await _userRequestRepository.UpdateUserRequestAsync(existingRequest.ToUserRequest(newUserRequest));

        }

        public async Task<bool> ApproveRequestByIdAsync(int id)
        {
            var request = await _userRequestRepository.GetByIdAsync(id);

            if (request is null)
            {
                return false;
            }

            var user = await _accountRepository.GetUserByIdAsync(request.EmployeeId);

            if (user is null)
            {
                return false;
            }

            TimeSpan difference = request.EndDate - request.StartDate;

            int wholeDays = difference.Days;

            if (wholeDays > user.DaysOffNumber)
            {
                return false;
            }

            user.DaysOffNumber = user.DaysOffNumber - wholeDays;
            request.Approved = true;

            await _accountRepository.UpdateUserAsync(user);
            await _userRequestRepository.UpdateAsync(request);

            return true;
        }

        public async Task<PaginatedList<UserRequest>> GetAllUserRequestsByPage(string comment,int pageNumber)
        {
            comment ??= string.Empty;

            var requestsQueryable = _userRequestRepository.AllRequestsQueryableBasedOnFilter(comment);

            var pageSize = 3;

            return await PaginatedList<UserRequest>.CreateAsync(requestsQueryable, pageNumber, pageSize);
        }

    }
}
