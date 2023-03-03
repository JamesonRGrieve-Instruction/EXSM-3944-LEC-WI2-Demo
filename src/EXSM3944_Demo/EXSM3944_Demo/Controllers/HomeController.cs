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
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SimpleViewDemo(string id)
        {
            if (id == null) ViewData["Error"] = new ArgumentNullException(nameof(id));

            ViewData["ID"] = id;
            return View();
        }

        public IActionResult SimpleFormDemo(string firstName, string lastName)
        {
            // On the initial load it will be a "GET" request. If the form submits it will be a "POST" request.
            if (HttpContext.Request.Method == "POST")
            {
                // We only want to validate in the event of a form submission. Otherwise we will start the form with a bunch of "null argument" errors.
                ValidationException error = new ValidationException();
                if (firstName == null) error.InnerExceptions.Add(new ArgumentNullException(nameof(firstName)));
                if (lastName == null) error.InnerExceptions.Add(new ArgumentNullException(nameof(lastName)));
                if (error.InnerExceptions.Count > 0) ViewData["Error"] = error;
                ViewData["FirstName"] = firstName;
                ViewData["LastName"] = lastName;
            }

            return View();
        }
        public IActionResult SimpleFormDemoOutput(string firstName, string lastName)
        {
            ViewData["FirstName"] = firstName;
            ViewData["LastName"] = lastName;
            return View();
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}