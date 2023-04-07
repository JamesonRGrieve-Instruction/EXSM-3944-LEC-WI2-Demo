using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXSM3944_Demo.Data;
using EXSM3944_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using EXSM3944_Demo.Models.DTO;

namespace EXSM3944_Demo.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly PersonDatabaseContext _context;

        public PersonController(PersonDatabaseContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var personDatabaseContext = _context.People.Include(p => p.Job).Include(p => p.Job.Industry);
            return View(await personDatabaseContext.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Person/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Description");
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,JobID,FirstName,LastName")] Person person)
        {
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
        public IActionResult DynamicCreate()
        {
            SelectList JobItems = new SelectList(_context.Jobs, "ID", "Name");
            SelectList IndustryItems = new SelectList(_context.Industry, "ID", "Name");
            ViewData["JobID"] = JobItems.Prepend(new SelectListItem("Create a new Job...", "0"));
            ViewData["IndustryID"] = IndustryItems.Prepend(new SelectListItem("Create a new Industry...", "0"));
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DynamicCreate([Bind($"{nameof(DynamicPerson.PersonFirstName)},{nameof(DynamicPerson.PersonLastName)},{nameof(DynamicPerson.PersonJobID)},{nameof(DynamicPerson.JobName)},{nameof(DynamicPerson.JobDescription)},{nameof(DynamicPerson.JobIndustryID)},{nameof(DynamicPerson.IndustryName)},{nameof(DynamicPerson.IndustryDescription)}")] DynamicPerson dtoDynamicPerson)
        {
            if (ModelState.IsValid)
            {
                if (dtoDynamicPerson.JobIndustryID == 0 && dtoDynamicPerson.PersonJobID != 0)
                {
                    throw new Exception("You cannot have a Job with no Industry!");
                }

                if (dtoDynamicPerson.JobIndustryID == 0)
                {
                    Industry newIndustry = new Industry()
                    {
                        Name = dtoDynamicPerson.IndustryName,
                        Description = dtoDynamicPerson.IndustryDescription
                    };
                    _context.Industry.Add(newIndustry);
                    _context.SaveChanges();
                    dtoDynamicPerson.JobIndustryID = newIndustry.ID;

                }
                if (dtoDynamicPerson.PersonJobID == 0)
                {
                    Job newJob = new Job()
                    {
                        Name = dtoDynamicPerson.JobName,
                        Description = dtoDynamicPerson.JobDescription,
                        IndustryID = dtoDynamicPerson.JobIndustryID
                    };
                    _context.Jobs.Add(newJob);
                    _context.SaveChanges();
                    dtoDynamicPerson.PersonJobID = newJob.ID;
                }
                Person newPerson = new Person()
                {
                    UserID = User.Identity.Name,
                    FirstName = dtoDynamicPerson.PersonFirstName,
                    LastName = dtoDynamicPerson.PersonLastName,
                    JobID = dtoDynamicPerson.PersonJobID
                };
                _context.Add(newPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SelectList JobItems = new SelectList(_context.Jobs, "ID", "Name");
                SelectList IndustryItems = new SelectList(_context.Industry, "ID", "Name");
                ViewData["JobID"] = JobItems.Prepend(new SelectListItem("Create a new Job...", "0"));
                ViewData["IndustryID"] = IndustryItems.Prepend(new SelectListItem("Create a new Industry...", "0"));
                return View(dtoDynamicPerson);
            }

        }

        // GET: Person/Edit/5
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

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,JobID,FirstName,LastName")] Person person)
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

        // GET: Person/Delete/5
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

        // POST: Person/Delete/5
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
