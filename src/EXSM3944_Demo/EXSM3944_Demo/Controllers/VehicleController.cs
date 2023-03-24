using EXSM3944_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXSM3944_Demo.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private static List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        // GET: VehicleController
        public ActionResult Index()
        {
            return View(Vehicles.Where(vehicle => vehicle.UserID == User.Identity.Name));
        }

        // GET: VehicleController/Details/5
        public ActionResult Details(string id)
        {
            return View(Vehicles.Single(vehicle => vehicle.VIN == id));
        }

        // GET: VehicleController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]Vehicle vehicle )
        {
            try
            {
                vehicle.VIN = vehicle.VIN.Trim().ToUpper();
                if (Vehicles.Any(x => x.VIN == vehicle.VIN))
                {
                    ModelState.AddModelError(nameof(vehicle.VIN), "VIN already exists.");
                }
                if (vehicle.ModelYear > DateTime.Now.Year + 1)
                {
                    ModelState.AddModelError(nameof(vehicle.ModelYear), "Model year cannot be higher than next year.");
                }
                if (vehicle.PurchaseDate > DateTime.Now)
                {
                    ModelState.AddModelError(nameof(vehicle.PurchaseDate), "You cannot have purchased a vehicle in the future.");
                }
                if (vehicle.SaleDate < vehicle.PurchaseDate)
                {
                    ModelState.AddModelError(nameof(vehicle.SaleDate), "You cannot have sold a vehicle before you purchased it.");
                }
                vehicle.UserID = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    Vehicles.Add(vehicle);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vehicle);
                }
            }
            catch
            {
                return View(vehicle);
            }
        }

        // GET: VehicleController/Edit/5
        public ActionResult Edit(string id)
        {
            return View(Vehicles.Single(vehicle => vehicle.VIN == id));
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, [FromForm] Vehicle vehicle)
        {
            try
            {
                Vehicle target = Vehicles.Single(vehicle => vehicle.VIN == id);
                vehicle.VIN = vehicle.VIN.Trim().ToUpper();
                if (Vehicles.Any(x => x.VIN == vehicle.VIN))
                {
                    ModelState.AddModelError(nameof(vehicle.VIN), "VIN already exists.");
                }
                if (vehicle.ModelYear > DateTime.Now.Year + 1)
                {
                    ModelState.AddModelError(nameof(vehicle.ModelYear), "Model year cannot be higher than next year.");
                }
                if (vehicle.PurchaseDate > DateTime.Now)
                {
                    ModelState.AddModelError(nameof(vehicle.PurchaseDate), "You cannot have purchased a vehicle in the future.");
                }
                if (vehicle.SaleDate < vehicle.PurchaseDate)
                {
                    ModelState.AddModelError(nameof(vehicle.SaleDate), "You cannot have sold a vehicle before you purchased it.");
                }
                if (ModelState.IsValid)
                {
                    target.Model = vehicle.Model;
                    target.Manufacturer = vehicle.Manufacturer;
                    target.Colour = vehicle.Colour;
                    target.ModelYear = vehicle.ModelYear;
                    target.PurchaseDate = vehicle.PurchaseDate;
                    target.SaleDate = vehicle.SaleDate;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vehicle);
                }
            }
            catch
            {
                return View(vehicle);
            }
        }

        // GET: VehicleController/Delete/5
        public ActionResult Delete(string id)
        {
            return View(Vehicles.Single(vehicle => vehicle.VIN == id));
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, [FromForm] Vehicle vehicle)
        {
            try
            {
                Vehicles.Remove(Vehicles.Single(vehicle => vehicle.VIN == id));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(vehicle);
            }
        }
    }
}
