﻿using Contracts.Request;
using DataLayer.Models.Request;
using DataLayer.Repositories.GenericRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IUserRequestRepository : IRepository<UserRequest>
    {
        public Task<bool> CreateUserRequestAsync(UserRequest userRequest);

        public Task<bool> UpdateUserRequestAsync(UserRequest userRequest);

        public Task<List<UserRequest>> AllRequestsForUserWithId(int id);

        public Task<List<UserRequest>> AllRequestsQueryableBasedOnFilter(UserRequestFilter filter);

        public Task<long> GetAllRequestsCountAsync(UserRequestFilter filter);

        public Task<long> GetAllStandbyRequestsCountAsync();

        public Task<List<UserRequest>> GetAllRequestsForUsersAsync();

    }
}
