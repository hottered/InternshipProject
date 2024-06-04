using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSystem.Models;

namespace WebApiSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetUsers()
        {
            var users = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new Employee { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            };

            return Ok(users);
        }
    }
}
