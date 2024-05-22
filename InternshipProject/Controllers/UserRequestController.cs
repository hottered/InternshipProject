using Contracts.Position;
using Contracts.Request;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Controllers
{
    public class UserRequestController : Controller
    {
        private readonly IUserRequestService _userRequestService;
        private readonly UserManager<Employee> _userManager;
        public UserRequestController(IUserRequestService userRequestService)
        {
            _userRequestService = userRequestService;
        }

        [Route("CreateUserRequest")]
        public IActionResult CreateUserRequest()
        {
            return View();
        }
        [Route("CreateUserRequest")]
        [HttpPost]
        public async Task<IActionResult> CreateUserRequest(UserRequestCreateRequest createRequest)
        {
            if (ModelState.IsValid)
            {

                var result = await _userRequestService.CreateUserRequestAsync(User,createRequest);

                if (!result)
                {
                    ModelState.AddModelError("", "There was an error while creating the user position. Please try again!");

                    return View(createRequest);
                }
                ModelState.Clear();

                return RedirectToAction("Index", "Home");


            }
            return View();
        }
    }
}
