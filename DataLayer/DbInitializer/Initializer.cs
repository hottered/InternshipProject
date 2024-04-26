using DataLayer.Data;
using DataLayer.Domain;
using DataLayer.Models;
using DataLayer.Models.Position;
using DataLayer.Models.Request;
using DataLayer.Repositories;
using DataLayer.Repositories.GenericRepository.Interfaces;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbInitializer
{
    public class Initializer
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRepository<UserRequest> _userRequestRepository;
        private readonly IRepository<UserPosition> _userPositionRepository;
        
        public Initializer(
            IRepository<UserRequest> userRequestRepository,
            IRepository<UserPosition> userPositionRepository,
            IAccountRepository accountRepository)
        {
            _userPositionRepository = userPositionRepository;
            _userRequestRepository = userRequestRepository;
            _accountRepository = accountRepository;
        }
        public async Task InitializeAsync()
        {
            var anyPositions = (await _userPositionRepository.GetAllAsync()).Any();
            if (!anyPositions)
            {
                var position = new UserPosition
                {
                    Caption = "Developer",
                    Description = "Description"
                };
                await _userPositionRepository.CreateAsync(position);
            }
            var anyRequests = (await _userRequestRepository.GetAllAsync()).Any();
            if (!anyRequests)
            {
                var request1 = new UserRequest
                {
                    LeaveType = "Vacation",
                    CommentEmployee = "Odlazim i ne vracam se",
                    CommentHR = "Odlazi cao"
                };
                var request2 = new UserRequest
                {
                    LeaveType = "Vacation",
                    CommentEmployee = "Odlazim i ne vracam se",
                    CommentHR = "Odlazi cao"
                };
                await _userRequestRepository.CreateAsync(request1);
                await _userRequestRepository.CreateAsync(request2);
            }
            var anyUsers = (await _accountRepository.GetAllUsersAsync()).Any();
            if (!anyUsers)
            {
                var admin = new Employee
                {
                    FirstName = RolesEnum.Admin.ToString(),
                    LastName = RolesEnum.Admin.ToString(),
                    Address = "AdminAddress",
                    IDNumber = RolesEnum.Admin.ToString(),
                    DaysOffNumber = 0,
                    Email = "admin@test.rs",
                    EmailConfirmed = true,
                    UserName = RolesEnum.Admin.ToString(),
                    NormalizedUserName = "ADMIN@TEST.RS".ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var hr = new Employee
                {
                    FirstName = RolesEnum.HR.ToString(),
                    LastName = RolesEnum.HR.ToString(),
                    Address = "HrAddress",
                    IDNumber = RolesEnum.HR.ToString(),
                    DaysOffNumber = 0,
                    Email = "hr@test.rs",
                    EmailConfirmed = true,
                    UserName = RolesEnum.HR.ToString(),
                    NormalizedUserName = "HR@TEST.RS".ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var position = await _userPositionRepository.GetByIdAsync(1);
                var requests = await _userRequestRepository.GetAllAsync();
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
                    Request = requests.ToList(),
                };

                var result = await _accountRepository.CreateUserAsync(admin, "Sifra.1234");
                if (result.Succeeded)
                {
                    await _accountRepository.AssignRoleAsync(admin, RolesEnum.Admin.ToString());
                }
                var resultHr = await _accountRepository.CreateUserAsync(hr, "Sifra.1234");
                if (resultHr.Succeeded)
                {
                    await _accountRepository.AssignRoleAsync(hr, RolesEnum.HR.ToString());

                }
                await _accountRepository.CreateUserAsync(user1, "Sifra.1234");

            }

        }
    }
}
