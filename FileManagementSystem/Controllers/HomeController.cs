using FileManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using FileManagementSystem.Service;

namespace FileManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMetadataExtractManager _metadataExtractManager;

        public HomeController(ILogger<HomeController> logger, IMetadataExtractManager metadataExtractManager)
        {
            _logger = logger;
            _metadataExtractManager = metadataExtractManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page()
        {
            return View();
        }

        public IActionResult Metadata(string fileType)
        {
            ViewBag.FileInfo = fileType;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Process(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return Json("Error");
            }
            List<FileDetailModel> fileDetails = _metadataExtractManager.Process(folderPath);
            return Json(fileDetails);
        }

        public ActionResult FetchFileCountByType()
        {
            List<FileDetailModel> fileDetails = _metadataExtractManager.FetchFileCountByType();
            return Json(fileDetails);
        }

        public ActionResult FetchFilesByType(string fileType)
        {
            List<FileMetadataModel> fileInfo = _metadataExtractManager.FetchFilesByType(fileType);
            return Json(fileInfo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}