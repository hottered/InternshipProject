﻿using DataLayer.Data;
using DataLayer.Models.Request;
using DataLayer.Repositories.GenericRepository;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public  class UserRequestRepository : BaseRepository<UserRequest>,IUserRequestRepository
    {
        public UserRequestRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<bool> CreateUserRequestAsync(UserRequest userRequest)
        {
            await _dbContext.Requests.AddAsync(userRequest);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateUserRequestAsync(UserRequest userRequest)
        {
            _dbContext.Requests.Update(userRequest);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
