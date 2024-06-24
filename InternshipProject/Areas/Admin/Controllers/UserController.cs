using Contracts.Employee;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using SharedDll;
using SharedDll.ApiRoutes;

namespace InternshipProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        [Route(ApiRoutes.RetrieveUsers)]
        public async Task<IActionResult> RetrieveUsers()
        {
            var result = await _accountService.CreateUsersFromOldSystem();

            if (!result)
            {
                ModelState.AddModelError(string.Empty, Constants.UserCreateErrorMessage);
            }

            return RedirectToAction(nameof(AllUsers), "User");
        }

        [Route(ApiRoutes.AllUsers)]
        public async Task<IActionResult> AllUsers(EmployeeFilter filter)
        {

            ViewData["CurrentFilter"] = filter;

            var users = await _accountService.GetAllUsersAsync(filter);

            return View(users);
        }

        [HttpGet(ApiRoutes.CreateUser)]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost(ApiRoutes.CreateUser)]
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

        [HttpGet(ApiRoutes.EditUser)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            var updateRequest = user.ToEmployeeUpdateRequest();

            return View(nameof(UpdateUser), updateRequest);
        }

        [HttpPost(ApiRoutes.EditUserById)]
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


        [HttpGet(ApiRoutes.DeleteUser)]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _accountService.DeleteUserAsync(id);

            return RedirectToAction(nameof(AllUsers), "User");
        }
    }
}
