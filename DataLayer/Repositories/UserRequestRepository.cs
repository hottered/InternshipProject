using Contracts.Request;
using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Models.Request;
using DataLayer.Repositories.GenericRepository;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedDll.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public  class UserRequestRepository : BaseRepository<UserRequest>,IUserRequestRepository
    {
        public UserRequestRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<UserRequest>> AllRequestsForUserWithId(int id)
        {
            return await _dbContext.Requests.Where(x => x.EmployeeId == id).ToListAsync();
        }

        public async Task<List<UserRequest>> AllRequestsQueryableBasedOnFilter(UserRequestFilter filter)
        {
            return await _dbContext.Requests
                .Filter(filter)
                .Include(x=>x.Employee)
                .Paginate(filter)
                .ToListAsync();
        }
        public async Task<long> GetAllRequestsCountAsync(UserRequestFilter filter)
        {
            return await _dbContext.Requests
                .Filter(filter)
                .CountAsync();
        }
        public async Task<long> GetAllStandbyRequestsCountAsync()
        {
            return await _dbContext.Requests
                .Where(x => x.Approved == RequestApprovalEnum.StandBy)
                .CountAsync();
        }
        public async Task<List<UserRequest>> GetAllRequestsForUsersAsync()
        {
            return await _dbContext.Requests.Include(x => x.Employee).ToListAsync();
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
