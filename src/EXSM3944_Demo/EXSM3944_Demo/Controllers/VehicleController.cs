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
                // TODO: Validate VIN uppercase.
                // TODO: Validate VIN unique.
                // TODO: Validate ModelYear less than or equal to current year plus one.
                // TODO: Valudate PurchaseDate on or before current date.
                // TODO: Validate SaleDate after PurchaseDate.
                vehicle.UserID = User.Identity.Name;
                Vehicles.Add(vehicle);
                return RedirectToAction(nameof(Index));
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
                target.Model = vehicle.Model;
                target.Manufacturer = vehicle.Manufacturer;
                target.Colour = vehicle.Colour;
                target.ModelYear = vehicle.ModelYear;
                target.PurchaseDate = vehicle.PurchaseDate;
                target.SaleDate = vehicle.SaleDate;
                return RedirectToAction(nameof(Index));
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
