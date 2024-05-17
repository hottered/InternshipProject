using DataLayer.Data;
using DataLayer.Domain;
using DataLayer.Models.Position;
using DataLayer.Models.Request;
using DataLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DbInitializer
{
    public class AppDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<Employee>>();

                _context.Database.EnsureCreated();

                var position = await _context.Positions.FirstOrDefaultAsync();

                //POSITIONS
                if (position is null)
                {
                    position = new UserPosition
                    {
                        Caption = "Developer",
                        Description = "Description"
                    };
                    await _context.Positions.AddAsync(position);
                }
                //USERS
                var admin = await _context.Users.FirstOrDefaultAsync(x => x.Email == "admin@test.rs");
                var hr = await _context.Users.FirstOrDefaultAsync(x => x.Email == "hr@test.rs");
                var user1 = await _context.Users.FirstOrDefaultAsync(x => x.Email == "user1@test.rs");

                if (admin is null && hr is null && user1 is null)
                {
                    admin = new Employee
                    {
                        FirstName = nameof(RolesEnum.Admin),
                        LastName = nameof(RolesEnum.Admin),
                        Address = "AdminAddress",
                        IDNumber = nameof(RolesEnum.Admin),
                        DaysOffNumber = 0,
                        Email = "admin@test.rs",
                        EmailConfirmed = true,
                        UserName = "admin@test.rs",
                        NormalizedUserName = "ADMIN@TEST.RS".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    hr = new Employee
                    {
                        FirstName = nameof(RolesEnum.HR),
                        LastName = nameof(RolesEnum.HR),
                        Address = "HrAddress",
                        IDNumber = nameof(RolesEnum.HR),
                        DaysOffNumber = 0,
                        Email = "hr@test.rs",
                        EmailConfirmed = true,
                        UserName = "hr@test.rs",
                        NormalizedUserName = "HR@TEST.RS".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    user1 = new Employee
                    {
                        FirstName = "User1",
                        LastName = "User1",
                        Address = "User1Address",
                        IDNumber = "User1IdNumber",
                        DaysOffNumber = 0,
                        Email = "user1@test.rs",
                        EmailConfirmed = true,
                        UserName = "user1@test.rs",
                        NormalizedUserName = "USER1@TEST.RS".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Position = position,
                    };

                    var result = await _userManager.CreateAsync(admin, "Sifra.1234");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, nameof(RolesEnum.Admin));
                    }
                    var resultHr = await _userManager.CreateAsync(hr, "Sifra.1234");
                    if (resultHr.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(hr, nameof(RolesEnum.HR));

                    }
                    await _userManager.CreateAsync(user1, "Sifra.1234");

                }

                //REQUESTS

                var request1 = await _context.Requests.FirstOrDefaultAsync(x => x.LeaveType == "Vacation");
                var request2 = await _context.Requests.FirstOrDefaultAsync(x => x.LeaveType == "Sick");
                var requests = new List<UserRequest>();

                if (request1 is null && request2 is null)
                {
                    request1 = new UserRequest
                    {
                        LeaveType = "Vacation",
                        CommentEmployee = "Leaving and never coming back",
                        CommentHR = "Goodbye!",
                        EmployeeID = user1.Id
                    };
                    request2 = new UserRequest
                    {
                        LeaveType = "Sick",
                        CommentEmployee = "Leaving and never returning",
                        CommentHR = "GoodByte!",
                        EmployeeID = user1.Id

                    };
                    requests.Add(request1);
                    requests.Add(request2);
                    await _context.Requests.AddAsync(request1);
                    await _context.Requests.AddAsync(request2);
                }             

                await _context.SaveChangesAsync();
            }
        }
    }
}
