using DataLayer.Models;
using DataLayer.Models.Login;
using DataLayer.Models.Register;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternshipProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly SignInManager<Employee> _signInManager;
        public AccountController(
            IAccountRepository accountRepository,
            SignInManager<Employee> signInManager)
        {
            _accountRepository = accountRepository;
            _signInManager = signInManager;
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(signUpModel);

                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(signUpModel);
                }
                ModelState.Clear();
            }
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                //var result = await _accountRepository.PasswordSignInAsync(loginModel);
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RemmemberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid credidentials");
            }
            return View(loginModel);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            //await _accountRepository.SignOutAsync();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize,Route("allUsers")]
        public async Task<IActionResult> AllUsers()
        {
            // Retrieve all users from the database
            var users = await _accountRepository.GetAllUsersAsync(); // Assuming you're using Entity Framework Core

            return View(users);
        }
    }
}

