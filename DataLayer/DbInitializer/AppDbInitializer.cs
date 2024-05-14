using DataLayer.Data;
using DataLayer.Domain;
using DataLayer.Migrations;
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


                var anyPositions =  await _context.Positions.AnyAsync();
                if (!anyPositions)
                {
                    var position = new UserPosition
                    {
                        Caption = "Developer",
                        Description = "Description"
                    };
                    await _context.Positions.AddAsync(position);
                }
                var anyRequests = await _context.Requests.AnyAsync();
                if (!anyRequests)
                {
                    var request1 = new UserRequest
                    {
                        LeaveType = "Vacation",
                        CommentEmployee = "Leaving and never coming back",
                        CommentHR = "Goodbye!"
                    };
                    var request2 = new UserRequest
                    {
                        LeaveType = "Vacation",
                        CommentEmployee = "Leaving and never returning",
                        CommentHR = "GoodByte!"
                    };
                    await _context.Requests.AddAsync(request1);
                    await _context.Requests.AddAsync(request2);
                }
                await _context.SaveChangesAsync();
                var anyUsers = await _context.Users.AnyAsync();
                if (!anyUsers)
                {
                    var admin = new Employee
                    {
                        FirstName = nameof(RolesEnum.Admin),
                        LastName = nameof(RolesEnum.Admin),
                        Address = "AdminAddress",
                        IDNumber = nameof(RolesEnum.Admin),
                        DaysOffNumber = 0,
                        Email = "admin@test.rs",
                        EmailConfirmed = true,
                        UserName = nameof(RolesEnum.Admin),
                        NormalizedUserName = "ADMIN@TEST.RS".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var hr = new Employee
                    {
                        FirstName = nameof(RolesEnum.HR),
                        LastName = nameof(RolesEnum.HR),
                        Address = "HrAddress",
                        IDNumber = nameof(RolesEnum.HR),
                        DaysOffNumber = 0,
                        Email = "hr@test.rs",
                        EmailConfirmed = true,
                        UserName = nameof(RolesEnum.HR),
                        NormalizedUserName = "HR@TEST.RS".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    //var position = await _userPositionRepository.GetByIdAsync(1);
                    //var requests = await _userRequestRepository.GetAllAsync();
                    var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == 1);
                    var requests = await _context.Requests.ToListAsync();

                    var user1 = new Employee
                    {
                        FirstName = "User1",
                        LastName = "User1",
                        Address = "User1Address",
                        IDNumber = "User1IdNumber",
                        DaysOffNumber = 0,
                        Email = "User1@test.rs",
                        EmailConfirmed = true,
                        UserName = "User1",
                        NormalizedUserName = "USER1@TEST.RS".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Position = position,
                        Request = requests,
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

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
