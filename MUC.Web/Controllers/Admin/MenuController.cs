using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Utilities;
using System.Data;

namespace MUC.Web.Controllers.Admin
{
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MenuController(ApplicationDbContext db)
        {
            _db = db;
        }


        [AllowAnonymous]
        public ActionResult DailyMenu()
        {
            var products = _db.Products.Include(c => c.Category).ToList();
            return View(products);
        }

        // GET: MenuController

        public ActionResult Index()
        {
            var products = _db.Products.Include(c=> c.Category).ToList();
            return View(products);
        }
             

        // GET: MenuController/Create
        public ActionResult Create(Guid id)
        {
            var product = _db.Products.Include(c => c.Category).FirstOrDefault(i => i.Id == id);
            //Console.WriteLine("is there a product:  " + product.Author);
            return View(product);
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Date","Something is missing");
                return View(product);
            }
            return RedirectToAction("Index");
        }

        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
