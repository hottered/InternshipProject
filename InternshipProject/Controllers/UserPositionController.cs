using Contracts.Employee;
using Contracts.Position;
using DataLayer.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ServiceLayer.Mappers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Controllers
{
    public class UserPositionController : Controller
    {
        private readonly IUserPositionService _userPositionService;
        public UserPositionController(IUserPositionService userPositionService)
        {
            _userPositionService = userPositionService;
        }

        [Route("AllUserPositions")]
        public async Task<IActionResult> AllUserPositions(string searchString,int pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;

            var positions = await _userPositionService.GetUserPositionsBasedOnPage(searchString,pageNumber);

            return View(positions);
        }

        [Route("CreateUserPosition")]
        public IActionResult CreateUserPosition()
        {
            return View();
        }
        [Route("CreateUserPosition")]
        [HttpPost]
        public async Task<IActionResult> CreateUserPosition(UserPositionCreateRequest createRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userPositionService.CreateUserPositionAsync(createRequest);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty,Constants.UserPosisitonCreateErrorMessage);

                    return View(createRequest);
                }

                return RedirectToAction("AllUserPositions", "UserPosition");


            }
            return View();
        }
         

        [Route("UpdateUserPosition")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserPosition(int id)
        {
            var position = await _userPositionService.GetUserPositionByIdAsync(id);

            var updateRequest = position.ToEmployeeUpdateRequest();

            return View(updateRequest);
        }


        [Route("UpdateUserPosition")]
        public IActionResult UpdateUserPosition()
        {
            return View();
        }

        [Route("UpdateUserPosition")]
        [HttpPost]
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

                return RedirectToAction("AllUserPositions", "UserPosition");

            }
            return View();
        }

        [Route("DeleteUserPosition")]
        [HttpGet]
        public async Task<IActionResult> DeleteUserPosition(int id)
        {
            await _userPositionService.DeleteUserPositionAsync(id);

            return RedirectToAction("AllUserPositions", "UserPosition");
        }

    }
}
