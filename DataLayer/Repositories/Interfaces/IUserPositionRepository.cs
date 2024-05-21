using DataLayer.Models.Position;
using DataLayer.Repositories.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IUserPositionRepository : IRepository<UserPosition>
    {

    }
}
