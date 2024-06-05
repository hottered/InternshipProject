using Contracts.Employee;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using SharedDll;

namespace InternshipProject.Controllers
{
    public class UserController : Controller
    {

        private readonly IAccountService _accountService;
        private readonly HttpClient _httpClient;
        
        public UserController(
            IAccountService accountService,
            HttpClient httpClient)
        {
            _accountService = accountService;
            _httpClient = httpClient;
        }

        [Route("/random-users/add")]
        public async Task<IActionResult> RetrieveUsers()
        {
            var result = await _accountService.CreateUsersFromOldSystem();

            if (!result)
            {
                ModelState.AddModelError(string.Empty, Constants.UserCreateErrorMessage);
            }

            return RedirectToAction(nameof(AllUsers), "User");
        }

        [Route("/users/all")]
        public async Task<IActionResult> AllUsers(EmployeeFilter filter)
        {

            ViewData["CurrentFilter"] = filter;

            var users = await _accountService.GetAllUsersAsync(filter);

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

                return RedirectToAction(nameof(AllUsers), "User");

            }
            return View();
        }

        [HttpGet("/users/{id}/edit")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            var updateRequest = user.ToEmployeeUpdateRequest();

            return View(nameof(UpdateUser),updateRequest);
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

                return RedirectToAction(nameof(AllUsers), "User");

            }
            return View();
        }


        [HttpGet("/users/{id}/delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _accountService.DeleteUserAsync(id);

            return RedirectToAction(nameof(AllUsers), "User");
        }
    }
}
