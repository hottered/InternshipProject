using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AllUserPositions()
        {
            var users = await _userPositionService.GetAllUserPositionsAsync();

            return View(users);
        }
    }
}
