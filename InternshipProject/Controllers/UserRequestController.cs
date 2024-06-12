using Contracts.Position;
using Contracts.Request;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ServiceLayer.Mappers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using SharedDll;
using SharedDll.ApiRoutes;
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

        [Route(ApiRoutes.CreateUserRequest)]
        public IActionResult CreateUserRequest()
        {
            return View();
        }
        [HttpPost(ApiRoutes.CreateUserRequest)]
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

                return RedirectToAction(nameof(AllUserRequests), "UserRequest");

            }
            return View();
        }


        [Route(ApiRoutes.AllUserRequests)]
        public async Task<IActionResult> AllUserRequests(UserRequestFilter filter)
        {

            ViewData["CurrentFilter"] = filter;

            var requests = await _userRequestService.GetAllUserRequestsByPage(filter);

            return View(nameof(AllUserRequests), requests);
        }

        [HttpGet(ApiRoutes.AllUserRequestsForUser)]
        public async Task<IActionResult> AllUserRequests(int id)
        {
            var requests = await _userRequestService.GetAllRequestsForTheUserWithId(id);

            return View(nameof(AllUserRequests), requests);
        }

        [HttpGet(ApiRoutes.DeleteUserRequest)]
        public async Task<IActionResult> DeleteUserRequest(int id)
        {
            await _userRequestService.DeleteUserRequestAsync(id);

            return RedirectToAction(nameof(AllUserRequests), "UserRequest");
        }

        [HttpGet(ApiRoutes.EditUserRequest)]
        public async Task<IActionResult> GetUserRequestById(int id)
        {
            var request = await _userRequestService.GetUserRequestByIdAsync(id);

            var result = request!.ToUserRequestUpdateRequest();

            return View(nameof(UpdateUserRequest),result);
        }

        [HttpPost(ApiRoutes.AllUserRequests)]
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

                return RedirectToAction(nameof(AllUserRequests), "UserRequest");

            }
            return View();
        }

        [HttpGet(ApiRoutes.ApproveUserRequest)]
        public async Task<IActionResult> ApproveUserRequest(int id)
        {
            await _userRequestService.ApproveRequestByIdAsync(id);

            return RedirectToAction(nameof(AllUserRequests), "UserRequest");
        }
    }
}
