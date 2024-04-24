using DataLayer.Data;
using DataLayer.Models.Position;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly AppDbContext _context;

        public PositionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserPosition>> GetAllPositionsAsync()
        {
            return await _context.Positions.ToListAsync();
        }
        public async Task<bool> CreatePositionAsync(UserPosition userPosition)
        {
            try
            {
                await _context.Positions.AddAsync(userPosition);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<UserPosition?> GetPositionByIdAsync(int id)
        {
            return await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> UpdatePositionAsync(UserPosition userPosition)
        {
            try
            {
                var existingPosition = await _context.Positions.FirstOrDefaultAsync(x => x.Id == userPosition.Id);

                if (existingPosition == null)
                {
                    return false;
                }

                existingPosition.Caption = userPosition.Caption;
                existingPosition.Description = userPosition.Description;

                _context.Positions.Update(existingPosition);

                await _context.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }
        public async Task<bool> DeletePositionAsync(int id)
        {
            try
            {
                var existingPosition = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);

                if (existingPosition == null)
                {
                    return false;
                }

                _context.Positions.Remove(existingPosition);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
