using Contracts.Position;
using InternshipProject.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Mappers;
using ServiceLayer.Services.Interfaces;
using SharedDll;
using SharedDll.ApiRoutes;

namespace InternshipProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserPositionController : Controller
    {

        private readonly IUserPositionService _userPositionService;
        public UserPositionController(IUserPositionService userPositionService)
        {
            _userPositionService = userPositionService;
        }

        [Route(ApiRoutes.AllUserPositions)]
        public async Task<IActionResult> AllUserPositions(UserPositionFilter filter)
        {
            ViewData["CurrentFilter"] = filter;

            var positions = await _userPositionService.GetAllUserPositionsAsync(filter);

            return View(positions);
        }

        [HttpGet(ApiRoutes.CreateUserPosition)]
        public IActionResult CreateUserPosition()
        {
            return View();
        }
        [HttpPost(ApiRoutes.CreateUserPosition)]
        public async Task<IActionResult> CreateUserPosition(UserPositionCreateRequest createRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userPositionService.CreateUserPositionAsync(createRequest);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, Constants.UserPosisitonCreateErrorMessage);

                    return View(createRequest);
                }

                return RedirectToAction(nameof(AllUserPositions), "UserPosition");


            }
            return View();
        }

        [HttpGet(ApiRoutes.EditUserPosition)]
        public async Task<IActionResult> GetUserPositionById(int id)
        {
            var position = await _userPositionService.GetUserPositionByIdAsync(id);

            var updateRequest = position.ToEmployeeUpdateRequest();

            return View(nameof(UpdateUserPosition), updateRequest);
        }

        [HttpPost(ApiRoutes.EditUserPositionById)]
        public async Task<IActionResult> UpdateUserPosition(UserPositionUpdateRequest updateRequest)
        {
            if (ModelState.IsValid)
            {

                var result = await _userPositionService.UpdateUserPositionAsync(updateRequest);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, Constants.UserPosisitonCUpdateErrorMessage);

                    return View(updateRequest);
                }

                return RedirectToAction(nameof(AllUserPositions), "UserPosition");

            }
            return View();
        }

        [HttpGet(ApiRoutes.DeleteUserPosition)]
        public async Task<IActionResult> DeleteUserPosition(int id)
        {
            await _userPositionService.DeleteUserPositionAsync(id);

            return RedirectToAction(nameof(AllUserPositions), "UserPosition");
        }
    }
}
