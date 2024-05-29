using Contracts.Position;
using Contracts.Request;
using DataLayer.Domain;
using DataLayer.Models;
using DataLayer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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

        [Route("/user-requests/new")]
        public IActionResult CreateUserRequest()
        {
            return View();
        }
        [HttpPost("/user-requests/new")]
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


        [HttpGet("/user-requests/all")]
        public async Task<IActionResult> AllUserRequests(int pageNumber)
        {
            var requests = await _userRequestService.GetAllUserRequestsByPage(pageNumber);

            return View("AllUserRequests", requests);
        }

        [HttpGet("/user-requests/user/{id}/{pageNumber}")]
        public async Task<IActionResult> AllUserRequests(int id,int pageNumber)
        {
            var requests = await _userRequestService.GetAllRequestsForTheUserWithId(id);

            return View("AllUserRequests",requests);
        }

        [HttpGet("/user-requests/{id}/delete")]
        public async Task<IActionResult> DeleteUserRequest(int id)
        {
            await _userRequestService.DeleteUserRequestAsync(id);

            return RedirectToAction("AllUserRequests", "UserRequest");
        }

        [HttpGet("/user-requests/{id}/edit")]
        public async Task<IActionResult> GetUserRequestById(int id)
        {
            var request = await _userRequestService.GetUserRequestByIdAsync(id);

            var result = request.ToUserRequestUpdateRequest();

            return View("UpdateUserRequest",result);
        }

        [HttpPost("/user-requests")]
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

        [HttpGet("/user-requests/{id}/approve")]
        public async Task<IActionResult> ApproveUserRequest(int id)
        {
            await _userRequestService.ApproveRequestByIdAsync(id);

            return RedirectToAction("AllUserRequests", "UserRequest");
        }
    }
}
