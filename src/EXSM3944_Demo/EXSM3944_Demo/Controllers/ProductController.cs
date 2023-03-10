using EXSM3944_Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXSM3944_Demo.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> Products = new List<Product>();

        // GET: ProductController
        public ActionResult Index()
        {
            if (Products.Count <1) Products.Add(new Product() { ID = 1, Name = "Sample", Description = "Sample Product" });
            return View(Products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(Products.Single(product => product.ID == id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]Product product)
        {
            // Validate Arguments Exist
            // MVC does this usually.

            // Format Arguments
            // We'll probably have to do this.
            product.Name = product.Name.Trim();
            product.Description = product.Description.Trim();

            // Single Argument Validation
            // MVC does this if you annotate your model properly.

            // Multi Argument Validation
            // MVC probably won't do this but it isn't necessary for basic models.

            // Determine Return

            try
            {
                Products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Products.Single(product => product.ID == id));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm]Product product)
        {
            try
            {
                Product target = Products.Single(productSearch => productSearch.ID == product.ID);
                target.ID = product.ID;
                target.Name = product.Name.Trim();
                target.Description = product.Description.Trim();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Products.Single(product => product.ID == id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] Product product)
        {
            try
            {
                Products.Remove(Products.Single(productSearch => productSearch.ID == id));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
