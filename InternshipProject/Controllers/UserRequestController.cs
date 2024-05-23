using Contracts.Position;
using Contracts.Request;
using DataLayer.Models;
using DataLayer.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Controllers
{
    public class UserRequestController : Controller
    {
        private readonly IUserRequestService _userRequestService;
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
                    ModelState.AddModelError("", Constants.UserRequestCreateErrorMessage);

                    return View(createRequest);
                }
                ModelState.Clear();

                return RedirectToAction("Index", "Home");


            }
            return View();
        }

        [HttpGet]
        [Route("AllUserRequests")]
        public async Task<IActionResult> AllUserRequests()
        {
            var requests = await _userRequestService.GetAllRequestsAsync();

            return View(requests);
        }
    }
}
