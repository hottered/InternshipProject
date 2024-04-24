using DataLayer.Data;
using DataLayer.Domain;
using DataLayer.Models;
using DataLayer.Models.Position;
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
        private readonly IPositionRepository _positionRepository;
        private readonly IUserRequestRepository _userRequestRepository;
        public Initializer(
            IAccountRepository accountRepository,
            IPositionRepository positionRepository,
            IUserRequestRepository userRequestRepository)
        {
            _accountRepository = accountRepository;
            _positionRepository = positionRepository;
            _userRequestRepository = userRequestRepository;
        }
        public  async Task InitializeAsync()
        {
            var anyPositions =  (await _positionRepository.GetAllPositionsAsync()).Any();
            if(!anyPositions)
            {
                var position = new UserPosition
                {
                    Caption = "Developer",
                    Description = "Description"
                };
                await _positionRepository.CreatePositionAsync(position);
            }
            var anyUsers = (await _accountRepository.GetAllUsersAsync()).Any();
            if(!anyUsers)
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
                var user1 = new Employee
                {
                    FirstName = "User1",
                    LastName = "User1",
                    Address = "User1Address",
                    IDNumber = "User1IdNumber",
                    DaysOffNumber = 0,
                    Email = "User1@test.rs",
                    EmailConfirmed = true,
                    UserName = RolesEnum.HR.ToString(),
                    NormalizedUserName = "USER1@TEST.RS".ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PositionId = 1
                };
                //await _accountRepository.CreateUserAsync(admin)
            }
        }
    }
}
