using EXSM3944_Demo.Data;
using EXSM3944_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXSM3944_Demo.Controllers
{
    

    [Authorize]
    public class PersonController : Controller
    {
        private static PersonDatabaseContext context = new PersonDatabaseContext();

        // GET: PersonController
        public ActionResult Index()
        {
            //if (!User.Identity.IsAuthenticated) return RedirectToRoute(new { controller = "Identity", action = "Account", id="Login" });
            return View(context.People.Where(person => person.UserID == User.Identity.Name));
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
                context.People.Add(person);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(person);
            }
        }
    }
}
