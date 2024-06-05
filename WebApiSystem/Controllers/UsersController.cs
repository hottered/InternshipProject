using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSystem.FakeData;
using WebApiSystem.Models;

namespace WebApiSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataGenerator _dataGenerator;

        public UsersController(DataGenerator dataGenerator)
        {
            _dataGenerator = dataGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var users = _dataGenerator.GenerateEmployees(3);

            return Ok(users);
        }
    }
}
