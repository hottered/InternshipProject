using Contracts.Employee;
using InternshipProject.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize, Route("allUsers")]
        public async Task<IActionResult> AllUsers()
        {
            var users = await _accountService.GetAllUsersAsync(); 

            return View(users);
        }

        [Route("CreateUser")]
        public IActionResult CreateUser()
        {
            return View();
        }
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(EmployeeCreateRequest createRequest)
        {
            if(ModelState.IsValid)
            {
                var employee = createRequest.ToEmployee();

                var result = await _accountService.CreateUserAsync(employee, createRequest.Passwrod);

                if (!result)
                {
                    ModelState.AddModelError("", "There was an error while creating the user. Please try again!");
                    
                    return View();
                }
                ModelState.Clear();
            }
            return View();
        }

        [Route("UpdateUser")]
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            var updateRequest = user.ToEmployeeUpdateRequest();

            return View(updateRequest);
        }


        [Route("UpdateUser")]
        public IActionResult UpdateUser()
        {
            return View();
        }

        [Route("UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(EmployeeUpdateRequest updateRequest)
        {
            if (ModelState.IsValid)
            {

                var employee = await _accountService.GetUserByIdAsync(updateRequest.Id);

                var employeeUpdate = employee.ToEmployee(updateRequest);

                var result = await _accountService.UpdateUserAsync(employeeUpdate);

                if (!result)
                {
                    ModelState.AddModelError("", "There was an error while updating the user. Please try again!");
                    
                    return View();
                }

                ModelState.Clear();
            }
            return View();
        }
    }
}
