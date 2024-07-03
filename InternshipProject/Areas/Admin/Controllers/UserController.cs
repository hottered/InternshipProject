using Contracts.Employee;
using DataLayer.Models.Contract;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Mappers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using SharedDll;
using SharedDll.ApiRoutes;
using System.Diagnostics.Contracts;

namespace InternshipProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IBlobService _blobService;
        private readonly IContractService _contractService;
        private readonly HttpClient _httpClient;

        public UserController(
            IAccountService accountService,
            HttpClient httpClient,
            IBlobService blobService,
            IContractService contractService
            )
        {
            _accountService = accountService;
            _httpClient = httpClient;
            _blobService = blobService;
            _contractService = contractService;
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


        public IActionResult AddContract(int id)
        {
            var contractViewModel = new UserContractViewModel
            {
                EmployeeId = id
            };

            return View(contractViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddContract(UserContractViewModel contractViewModel, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("ContractPdf", "File is required.");
                return View(contractViewModel);
            }

            var result = await _blobService.UploadFileBlobAsync(file);

            await _contractService.AddContractAsync(contractViewModel);

            ViewBag.Message = "File uploaded successfully.";

            ViewBag.Url = result;

            return View();


        }
    }
}
