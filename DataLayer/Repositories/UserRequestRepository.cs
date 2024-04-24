using DataLayer.Data;
using DataLayer.Models.Request;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public  class UserRequestRepository : IUserRequestRepository
    {
        private readonly AppDbContext _context;
        public UserRequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserRequest> GetRequestByIdAsync(int id)
        {
            return await _context.Requests
                .Include(ur => ur.Employee)
                .FirstOrDefaultAsync(ur => ur.Id == id);
        }
        public async Task<List<UserRequest>> GetAllRequestsAsync()
        {
            return await _context.Requests
                .Include(ur => ur.Employee)
                .ToListAsync();
        }
        public async Task<bool> CreateRequestAsync(UserRequest userRequest)
        {
            try
            {
                _context.Requests.Add(userRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateRequestAsync(UserRequest userRequest)
        {
            try
            {
                _context.Requests.Update(userRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteRequestAsync(int id)
        {
            var userRequest = await _context.Requests.FindAsync(id);
            if (userRequest == null)
                return false;

            try
            {
                _context.Requests.Remove(userRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
