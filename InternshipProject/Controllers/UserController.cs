using Contracts.Employee;
using DataLayer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Mappers;
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
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUserAsync(createRequest, createRequest.Password);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, Constants.UserCreateErrorMessage);

                    return View(createRequest);
                }

                return RedirectToAction("AllUsers", "User");

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

                var result = await _accountService.UpdateUserAsync(updateRequest);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, Constants.UserUpdateErrorMessage);

                    return View(updateRequest);
                }

                return RedirectToAction("AllUsers", "User");

            }
            return View();
        }


        [Route("DeleteUser")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _accountService.DeleteUserAsync(id);

            return RedirectToAction("AllUsers", "User");
        }
    }
}
