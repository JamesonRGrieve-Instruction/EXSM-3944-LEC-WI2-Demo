using EXSM3944_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EXSM3944_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index Action of the HomeController goes to Views/Home/Index.cshtml
        public IActionResult Index()
        {
            return View();
        }

        // Privacy Action of the HomeController goes to Views/Home/Privacy.cshtml
        public IActionResult Privacy(string id)
        {
            ViewData["ID"] = id;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}