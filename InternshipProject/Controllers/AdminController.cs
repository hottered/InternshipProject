using Microsoft.AspNetCore.Mvc;

namespace InternshipProject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}
