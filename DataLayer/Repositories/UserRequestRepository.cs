using DataLayer.Data;
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
        public override async Task DeleteAsync(UserRequest entity)
        {
            _dbContext.Requests.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
