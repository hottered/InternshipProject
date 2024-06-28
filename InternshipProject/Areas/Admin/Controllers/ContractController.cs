using DataLayer.Models.Contract;
using InternshipProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContractController : Controller
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UserContract contract)
        {
            if (ModelState.IsValid)
            {
                await _contractService.SaveContractAsync(contract);
                return RedirectToAction(nameof(HomeController.Index),"Home");
            }

            return View(contract);
        }

        public IActionResult UploadSuccess()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var fileBytes = await _contractService.GetContractFileAsync(id);
            if (fileBytes == null)
            {
                return NotFound();
            }

            return File(fileBytes, "application/pdf", "contract.pdf");
        }
    }
}
