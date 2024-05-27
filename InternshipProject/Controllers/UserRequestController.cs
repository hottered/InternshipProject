using Contracts.Position;
using Contracts.Request;
using DataLayer.Domain;
using DataLayer.Models;
using DataLayer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Mappers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using System.Security.Claims;

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
                var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var result = await _userRequestService.CreateUserRequestAsync(userId,createRequest);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, Constants.UserRequestCreateErrorMessage);

                    return View(createRequest);
                }

                return RedirectToAction("Index", "Home");

            }
            return View();
        }


        [HttpGet]
        [Route("AllUserRequests")]
        public async Task<IActionResult> AllUserRequests()
        {
            var requests = await _userRequestService.GetAllRequestsAsync();

            return View("AllUserRequests", requests);
        }

        [HttpGet]
        [Route("AllUserRequests/{id}")]
        public async Task<IActionResult> AllUserRequests(int id)
        {
            var requests = await _userRequestService.GetAllRequestsForTheUserWithId(id);

            return View("AllUserRequests",requests);
        }

        [Route("DeleteUserRequest")]
        [HttpGet]
        public async Task<IActionResult> DeleteUserRequest(int id)
        {
            await _userRequestService.DeleteUserRequestAsync(id);

            return RedirectToAction("AllUserRequests", "UserRequest");
        }

        [Route("UpdateUserRequest")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserRequest(int id)
        {
            var request = await _userRequestService.GetUserRequestByIdAsync(id);

            var result = request.ToUserRequestUpdateRequest();

            return View(result);
        }

        [Route("UpdateUserRequest")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserRequest(UserRequestUpdateRequest updateRequest)
        {
            if (ModelState.IsValid)
            {

                var result = await _userRequestService.UpdateUserRequestAsync(updateRequest);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, Constants.UserPosisitonCUpdateErrorMessage);

                    return View(updateRequest);
                }

                return RedirectToAction("AllUserRequests", "UserRequest");

            }
            return View();
        }

        [Route("ApproveUserRequest")]
        [HttpGet]
        public async Task<IActionResult> ApproveUserRequest(int id)
        {
            await _userRequestService.ApproveRequestByIdAsync(id);

            return RedirectToAction("AllUserRequests", "UserRequest");
        }
    }
}
