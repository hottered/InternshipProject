using DataLayer.Data;
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
    public class PositionRepository : BaseRepository<UserPosition>,IPositionRepository
    {
        public PositionRepository(AppDbContext context) : base(context) { }
        
    }
}
