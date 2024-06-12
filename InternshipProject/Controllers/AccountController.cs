using Contracts.Employee;
using DataLayer.Models;
using DataLayer.Models.Login;
using DataLayer.Models.Register;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using SharedDll;

namespace InternshipProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<Employee> _signInManager;
        public AccountController(
            IAccountService accountService,
            SignInManager<Employee> signInManager)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [Route(nameof(Signup))]
        public IActionResult Signup()
        {
            return View();
        }
        [Route(nameof(Signup))]
        [HttpPost]
        public async Task<IActionResult> Signup(EmployeeCreateRequest createRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUserAsync(createRequest, createRequest.Password);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, Constants.UserCreateErrorMessage);
                    return View(createRequest);
                }
            }
            return View();
        }

        [Route(nameof(Login))]
        public IActionResult Login()
        {
            return View();
        }

        [Route(nameof(Login))]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RemmemberMe, false);

                if (result.Succeeded)
                {
                    var isAdmin = await _accountService.SignedInAsAdmin(loginModel.Email);

                    if (isAdmin)
                    {
                        return RedirectToAction(nameof(AdminController.AdminPage),"Admin");
                    }

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                ModelState.AddModelError(string.Empty, Constants.LoginError);
            }

            return View(loginModel);
        }

        [Route(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}

