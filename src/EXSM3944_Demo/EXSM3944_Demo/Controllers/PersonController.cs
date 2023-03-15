using EXSM3944_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXSM3944_Demo.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private static List<Person> People { get; set; } = new List<Person>();

        // GET: PersonController
        public ActionResult Index()
        {
            //if (!User.Identity.IsAuthenticated) return RedirectToRoute(new { controller = "Identity", action = "Account", id="Login" });
            return View(People.Where(person => person.UserID == User.Identity.Name));
        }


        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]Person person)
        {
            try
            {
                person.UserID = User.Identity.Name;
                People.Add(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(person);
            }
        }
    }
}
