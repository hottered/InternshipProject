using Contracts.Contract;
using Contracts.Request;
using DataLayer.Models.Contract;
using InternshipProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContractController : Controller
    {
        private readonly IContractService _contractService;
        private readonly IBlobService _blobService;

        public ContractController(
            IContractService contractService,
            IBlobService blobService)
        {
            _contractService = contractService;
            _blobService = blobService;
        }
        public async Task<IActionResult> AllUserContracts(UserContractFilter filter)
        {

            ViewData["CurrentFilter"] = filter;

            var requests = await _contractService.GetAllContractsAsync(filter);

            return View(nameof(AllUserContracts), requests);
        }
    }
}
