using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Models.ViewModels;
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

            return View();
        }

        // GET: MenuController

        public ActionResult Index()
        {
            var products = _db.Products.Include(c => c.Category).ToList();
            return View(products);
        }


        // GET: MenuController/Create
        public ActionResult Create(Guid id)
        {

            MenuVM vm = new MenuVM
            {
                ProductId = id,
                OneProduct = _db.Products.FirstOrDefault(f => f.Id == id),
            };

            return View(vm);
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Console.WriteLine(vm.DateColumn + " what goes ***********************");
            
            if(vm.DateColumn == null) {
                vm.DateColumn = DateOnly.FromDateTime(DateTime.Now);
            }
            Menu NewMenu = new Menu();
            NewMenu.DateColumn =  vm.DateColumn;
            Console.WriteLine("ID: " + vm.ID + " Date: " + vm.DateColumn + " Product" + vm.OneProduct);
           

            return RedirectToAction("Index");
        }

    } 
}
