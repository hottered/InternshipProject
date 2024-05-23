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
            builder.Entity<Employee>().HasQueryFilter(user => !user.IsDeleted);

            builder.Entity<UserPosition>().HasQueryFilter(position => !position.IsDeleted);

            builder.Entity<UserRequest>().HasQueryFilter(position => !position.IsDeleted);

            base.OnModelCreating(builder);
            SeedRoles(builder);
        }
        
        private static void SeedRoles(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityRole<int>>().HasData(

                    new IdentityRole<int>() { Id = 1, Name = nameof(RolesEnum.Admin), ConcurrencyStamp = "1", NormalizedName = nameof(RolesEnum.Admin).ToUpper() },
                    new IdentityRole<int>() { Id = 2, Name = nameof(RolesEnum.HR), ConcurrencyStamp = "2", NormalizedName = nameof(RolesEnum.HR).ToUpper() }

                );
            
        }
    }
}
