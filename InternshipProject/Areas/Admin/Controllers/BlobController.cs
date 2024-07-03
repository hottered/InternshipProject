using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Download(string blobName)
        {
            blobName = "contract.pdf";
            var stream = await _blobService.GetBlobAsync(blobName);

            if (stream == null)
            {
                return NotFound();
            }

            return File(stream, "application/octet-stream", blobName);
        }
        public IActionResult Upload()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected.");
            }

            var result = await _blobService.UploadFileBlobAsync(file);
            ViewBag.Message = "File uploaded successfully.";
            ViewBag.Url = result;
            return View();
        }
    }
}
