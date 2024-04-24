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
            //modelBuilder.Entity<UserPosition>()
            //    .HasMany(e=>e.Employee)
            //    .WithOne(p =>p.Position)
            //    .HasForeignKey(k=>k.PositionId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<IdentityRole<int>>().HasData(

                    new IdentityRole<int>() { Id = 1, Name = RolesEnum.Admin.ToString(), ConcurrencyStamp = "1", NormalizedName = RolesEnum.Admin.ToString().ToUpper() },
                    new IdentityRole<int>() { Id = 2, Name = RolesEnum.HR.ToString(), ConcurrencyStamp = "2", NormalizedName = RolesEnum.HR.ToString().ToUpper() }

                );
            //POSITION
            //var position = new UserPosition()
            //{
            //    Id = 1,
            //    Caption = "Developer",
            //    Description = "Employee who develops new things",
            //};
            //modelBuilder.Entity<UserPosition>().HasData(position);

            ////REQUEST
            //var request1 = new UserRequest
            //{
            //    Id = 1,
            //    LeaveType = "Bolovanje",
            //    CommentEmployee = "Moram na odmor mnogo radim",
            //    CommentHR = "Ne moze odmor moras da radis"

            //};
            //var request2 = new UserRequest
            //{
            //    Id = 2,
            //    LeaveType = "Odmor",
            //    CommentEmployee = "Zelim na odmor mnogo radim",
            //    CommentHR = "Moze odmor moras da radis"
            //};

            ////USERS
            //var admin = new Employee
            //{
            //    Id = 1,
            //    Email = "admin@test.rs",
            //    EmailConfirmed = true,
            //    FirstName = RolesEnum.Admin.ToString(),
            //    LastName = RolesEnum.Admin.ToString(),
            //    UserName = RolesEnum.Admin.ToString(),
            //    NormalizedUserName = "ADMIN@TEST.RS".ToUpper(),
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //PasswordHasher<Employee> ph = new PasswordHasher<Employee>();
            //admin.PasswordHash = ph.HashPassword(admin, "Sifra.1234");

            //var user = new Employee
            //{
            //    Id = 2,
            //    Email = "user@test.rs",
            //    EmailConfirmed = true,
            //    FirstName = "User1",
            //    LastName = "User1",
            //    UserName = "User1",
            //    NormalizedUserName = "USER@TEST.RS".ToUpper(),
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    PositionId = 1,
            //};
            //PasswordHasher<Employee> userPassword = new PasswordHasher<Employee>();
            //user.PasswordHash = userPassword.HashPassword(user, "Sifra.1234");

            //var user2 = new Employee
            //{
            //    Id = 3,
            //    Email = "user2@test.rs",
            //    EmailConfirmed = true,
            //    FirstName = "User2",
            //    LastName = "User2",
            //    UserName = "User2",
            //    NormalizedUserName = "USER2@TEST.RS".ToUpper(),
            //    SecurityStamp = Guid.NewGuid().ToString(),
                
            //};
            //PasswordHasher<Employee> user2Password = new PasswordHasher<Employee>();
            //user2.PasswordHash = user2Password.HashPassword(user2, "Sifra.1234");

            //modelBuilder.Entity<Employee>().HasData(admin,user,user2);


            //modelBuilder.Entity<IdentityUserRole<int>>().HasData( 
                    
            //        new IdentityUserRole<int>
            //        {
            //            RoleId = 1,
            //            UserId = 1,
            //        }
            //    );
            
        }
    }
}
