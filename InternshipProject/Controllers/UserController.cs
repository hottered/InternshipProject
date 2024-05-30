using Contracts.Employee;
using DataLayer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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

        [Route("/users/all")]
        public async Task<IActionResult> AllUsers(string searchString, int pageNumber,string currentFilter)
        {
            searchString = (searchString != null) ? searchString : currentFilter;

            ViewData["CurrentFilter"] = searchString;

            var users = await _accountService.GetUsersBasedOnPage(searchString,pageNumber);

            return View(users);
        }

        [HttpGet("/users/new")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost("/users/new")]
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

        [HttpGet("/users/{id}/edit")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            var updateRequest = user.ToEmployeeUpdateRequest();

            return View("UpdateUser",updateRequest);
        }

        [HttpPost("/users")]
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


        [HttpGet("/users/{id}/delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _accountService.DeleteUserAsync(id);

            return RedirectToAction("AllUsers", "User");
        }
    }
}
