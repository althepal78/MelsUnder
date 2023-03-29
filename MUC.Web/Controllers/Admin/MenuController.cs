
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Models.ViewModels;
using MUC.Utilities;
using System.Data;
using System.Net.WebSockets;

namespace MUC.Web.Controllers.Admin {
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class MenuController : Controller {
        private readonly ApplicationDbContext _db;

        public MenuController(ApplicationDbContext db) {
            _db = db;
        }


        [AllowAnonymous]
        public IActionResult DailyMenu() {
            var date = DateOnly.FromDateTime(DateTime.Now);
            Menu todayMenu = _db.Menus.
                              Where(d => d.DateColumn == DateOnly.FromDateTime(DateTime.Now)).
                              Include(pm => pm.ProductMenus).
                              ThenInclude(p => p.Product).
                              FirstOrDefault();
            return View(todayMenu);
        }

        // GET: MenuController

        public IActionResult Index() {
            var products = _db.Products.Include(c => c.Category).ToList();
            return View(products);
        }


        // GET: MenuController/Create
        public IActionResult Create(Guid pid, Guid mid) {

            var pr = _db.ProductMenus.FirstOrDefault(f => f.ProductID == pid && f.MenuID == mid);
            if(pr != null) {

                return View();
            }

           

            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuVM vm) {
            vm.OneProduct = _db.Products.FirstOrDefault(p => p.Id == vm.ProductId);
            if (!ModelState.IsValid) {

                return View(vm);
            }
            Menu menu = _db.Menus.FirstOrDefault(m => m.DateColumn == vm.DateColumn);
            Console.WriteLine(vm.DateColumn);
            if (menu == null) {
                Menu m = new Menu {
                    DateColumn = vm.DateColumn,
                };
                _db.Menus.Add(m);
                _db.SaveChanges();

                ProductMenu pm = new ProductMenu {
                    MenuID = m.ID,
                    ProductID = vm.ProductId
                };
                _db.ProductMenus.Add(pm);
                _db.SaveChanges();
            }
            else {
                ProductMenu pm = new ProductMenu {
                    MenuID = menu.ID,
                    ProductID = vm.ProductId
                };
                if (_db.ProductMenus.Where(p => p.ProductID == vm.ProductId && p.MenuID == menu.ID).FirstOrDefault() is not null) {
                    ModelState.AddModelError("NewMenu", "Already in the menu of " + menu.DateColumn.ToString("MMMM dd , yyyy"));

                    return View(vm);
                }
                _db.ProductMenus.Add(pm);
                _db.SaveChanges();
            }


            Console.WriteLine(vm.MenuId + "****************");
            return RedirectToAction("Index");
        }

    }
}
