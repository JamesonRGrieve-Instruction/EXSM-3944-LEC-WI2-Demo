using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXSM3944_Demo.Data;
using EXSM3944_Demo.Models;
using EXSM3944_Demo.Models.DTO;

namespace EXSM3944_Demo.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonDatabaseContext _context;

        public PeopleController(PersonDatabaseContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var personDatabaseContext = _context.People.Where(person => person.UserID == User.Identity.Name);
            return View(await personDatabaseContext.ToListAsync());
        }

        // GET: People
        public async Task<IActionResult> List()
        {
            var personDatabaseContext = _context.People.Where(person => person.UserID == User.Identity.Name).Include(p => p.Job);
            return View(await personDatabaseContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Details/5
        public async Task<IActionResult> DetailedDetails(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Job)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Description");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JobID,FirstName,LastName")] Person person)
        {
            person.UserID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Description", person.JobID);
            return View(person);
        }


        // GET: People/Create
        public IActionResult CreateWithJob()
        {
            SelectList JobItems = new SelectList(_context.Jobs, "ID", "Description");
            ViewData["JobID"] = JobItems.Prepend(new SelectListItem("Create a new Job...", "0")); 
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithJob([Bind("PersonFirstName,PersonLastName,PersonJobID,JobName,JobDescription")] PersonJob dtoPersonJob)
        {
            if (ModelState.IsValid)
            {
                Person newPerson = new Person()
                {
                    UserID = User.Identity.Name,
                    FirstName = dtoPersonJob.PersonFirstName,
                    LastName = dtoPersonJob.PersonLastName
                };
                if (dtoPersonJob.PersonJobID != 0)
                {
                    newPerson.JobID = dtoPersonJob.PersonJobID;
                }
                else
                {
                    Job newJob = new Job()
                    {
                        Name = dtoPersonJob.JobName,
                        Description = dtoPersonJob.JobDescription
                    };
                    _context.Jobs.Add(newJob);
                    _context.SaveChanges();
                    newPerson.JobID = newJob.ID;
                }
                _context.Add(newPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SelectList JobItems = new SelectList(_context.Jobs, "ID", "Description", dtoPersonJob.PersonJobID);
            ViewData["JobID"] = JobItems.Prepend(new SelectListItem("Create a new Job...", "0"));
            return View(dtoPersonJob);
        }


        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Description", person.JobID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JobID,FirstName,LastName")] Person person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Description", person.JobID);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Job)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'PersonDatabaseContext.People'  is null.");
            }
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
