using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Position;
using DataLayer.Models.Request;
using DataLayer.Domain;

namespace DataLayer.Data
{
    public class AppDbContext : IdentityDbContext<Employee, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserPosition> Positions { get; set; }
        public DbSet<UserRequest> Requests { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }
        private static void SeedRoles(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityRole<int>>().HasData(

                    new IdentityRole<int>() { Id = 1, Name = RolesEnum.Admin.ToString(), ConcurrencyStamp = "1", NormalizedName = RolesEnum.Admin.ToString().ToUpper() },
                    new IdentityRole<int>() { Id = 2, Name = RolesEnum.HR.ToString(), ConcurrencyStamp = "2", NormalizedName = RolesEnum.HR.ToString().ToUpper() }

                );
            
        }
    }
}
