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
        
    }
}
