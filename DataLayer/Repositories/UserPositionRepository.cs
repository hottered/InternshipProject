﻿using DataLayer.Data;
using DataLayer.Models.Position;
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
    public class UserPositionRepository : BaseRepository<UserPosition>, IUserPositionRepository
    {
        public UserPositionRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CreateUserPositionAsync(UserPosition userPosition)
        {
            await _dbContext.Positions.AddAsync(userPosition);

            var res = await _dbContext.SaveChangesAsync();

            return res > 0;
        }

        public IQueryable<UserPosition> GetUserPositionsQuryableFiltered(string searchString, int pageNumber)
        {
            return _dbContext.Positions.Where(e => e.Caption!.Contains(searchString) || e.Description!.Contains(searchString));

        }

        public async Task<bool> UpdateUserPositionAsync(UserPosition userPosition)
        {
            _dbContext.Positions.Update(userPosition);

            var res = await _dbContext.SaveChangesAsync();

            return res > 0;
        }
    }
}
