using Contracts.Employee;
using Contracts.Position;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ServiceLayer.Mappers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using SharedDll;

namespace InternshipProject.Controllers
{
    public class UserPositionController : Controller
    {
        private readonly IUserPositionService _userPositionService;
        public UserPositionController(IUserPositionService userPositionService)
        {
            _userPositionService = userPositionService;
        }

        [Route("/user-positions/all")]
        public async Task<IActionResult> AllUserPositions(UserPositionFilter filter)
        {

            ViewData["CurrentFilter"] = filter;

            var positions = await _userPositionService.GetAllUserPositionsAsync(filter);

            return View(positions);
        }

        [HttpGet("/user-positions/new")]
        public IActionResult CreateUserPosition()
        {
            return View();
        }
        [HttpPost("/user-positions/new")]
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

        [HttpGet("/user-positions/{id}/edit")]
        public async Task<IActionResult> GetUserPositionById(int id)
        {
            var position = await _userPositionService.GetUserPositionByIdAsync(id);

            var updateRequest = position.ToEmployeeUpdateRequest();

            return View(nameof(UpdateUserPosition), updateRequest);
        }

        [HttpPost("/user-positions")]
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

        [HttpGet("/user-positions{id}/delete")]
        public async Task<IActionResult> DeleteUserPosition(int id)
        {
            await _userPositionService.DeleteUserPositionAsync(id);

            return RedirectToAction(nameof(AllUserPositions), "UserPosition");
        }

    }
}
