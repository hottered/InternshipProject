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
using SharedDll;

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
                        Caption = Constants.PositionCaption,
                        Description = Constants.PositionDescription,
                    };

                    await _context.Positions.AddAsync(position);
                }
                //USERS
                var admin = await _context.Users.FirstOrDefaultAsync(x => x.Email == Constants.AdminEmail);

                var hr = await _context.Users.FirstOrDefaultAsync(x => x.Email == Constants.HrEmail);

                var user1 = await _context.Users.FirstOrDefaultAsync(x => x.Email == Constants.User1Email);

                if (admin is null && hr is null && user1 is null)
                {
                    admin = new Employee
                    {
                        FirstName = nameof(RolesEnum.Admin),
                        LastName = nameof(RolesEnum.Admin),
                        Address = Constants.AdminAddress,
                        IDNumber = nameof(RolesEnum.Admin),
                        DaysOffNumber = 0,
                        Email = Constants.AdminEmail,
                        EmailConfirmed = true,
                        UserName = Constants.AdminEmail,
                        NormalizedUserName = Constants.AdminEmail.ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    hr = new Employee
                    {
                        FirstName = nameof(RolesEnum.HR),
                        LastName = nameof(RolesEnum.HR),
                        Address = Constants.HrAddress,
                        IDNumber = nameof(RolesEnum.HR),
                        DaysOffNumber = 0,
                        Email = Constants.HrEmail,
                        EmailConfirmed = true,
                        UserName = Constants.HrEmail,
                        NormalizedUserName = Constants.HrEmail.ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    user1 = new Employee
                    {
                        FirstName = Constants.User1LastName,
                        LastName = Constants.User1LastName,
                        Address = Constants.User1Address,
                        IDNumber = Constants.User1Address,
                        DaysOffNumber = 0,
                        Email = Constants.User1Email,
                        EmailConfirmed = true,
                        UserName = Constants.User1Email,
                        NormalizedUserName = Constants.User1Email.ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Position = position,
                    };

                    var result = await _userManager.CreateAsync(admin, Constants.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, nameof(RolesEnum.Admin));
                    }

                    var resultHr = await _userManager.CreateAsync(hr, Constants.Password);

                    if (resultHr.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(hr, nameof(RolesEnum.HR));

                    }
                    await _userManager.CreateAsync(user1, Constants.Password);

                }

                //REQUESTS

                var request1 = await _context.Requests.FirstOrDefaultAsync(x => x.LeaveType == nameof(LeaveTypeEnum.Sick));

                var request2 = await _context.Requests.FirstOrDefaultAsync(x => x.LeaveType == nameof(LeaveTypeEnum.Vacation));

                if (request1 is null && request2 is null)
                {
                    
                    request1 = new UserRequest
                    {
                        LeaveType = nameof(LeaveTypeEnum.Sick),
                        CommentEmployee = Constants.CommentEmployee,
                        CommentHR = Constants.CommentHR,
                        EmployeeId = user1.Id
                    };

                    request2 = new UserRequest
                    {
                        LeaveType = nameof(LeaveTypeEnum.Vacation),
                        CommentEmployee = Constants.CommentEmployee,
                        CommentHR = Constants.CommentHR,
                        EmployeeId = user1.Id

                    };

                    var requests = new List<UserRequest> { request1, request2 };

                    _context.Requests.AddRange(requests);
                    
                }             

                await _context.SaveChangesAsync();
            }
        }
    }
}
