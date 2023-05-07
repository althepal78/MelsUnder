
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Models.ViewModels;
using MUC.Utilities;
using Newtonsoft.Json;
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
        public IActionResult DailyMenu()
        {
            var date = DateTime.UtcNow;
            Menu todayMenu = _db.Menus.
                              Where(d => d.DateColumn.ToUniversalTime() == date.ToUniversalTime()).
                              Include(pm => pm.ProductMenus).
                              ThenInclude(p => p.Product).
                              FirstOrDefault();
            return View(todayMenu);
        }

        // GET: MenuController
        public IActionResult Index()
        {
            var products = _db.Products.Include(c => c.Category).ToList();
            return View(products);
        }


        // GET: MenuController/Create
        public IActionResult Create(Guid pid)
        {

            MenuVM vm = new MenuVM();
            vm.OneProduct = _db.Products.Include(s => s.ProductMenus).ThenInclude(m => m.Menu).FirstOrDefault(p => p.Id == pid);
            vm.ProductId = pid;
            List<DateTime> DateList = new List<DateTime>();
            if (vm.OneProduct.ProductMenus != null)
            {
                foreach (var item in vm.OneProduct.ProductMenus)
                {
                    DateList.Add(item.Menu.DateColumn);
                }
                string json = JsonConvert.SerializeObject(DateList, new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd" });
                ViewBag.Dates = json;
            }

            return View(vm);
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuVM vm)
        {
            vm.OneProduct = _db.Products.FirstOrDefault(p => p.Id == vm.ProductId);
            if (!ModelState.IsValid)
            {

                return View(vm);
            }
            Menu menu = _db.Menus.FirstOrDefault(m => m.DateColumn.ToUniversalTime() == vm.DateColumn.ToUniversalTime());
            Console.WriteLine(vm.DateColumn);
            if (menu == null)
            {
                Menu m = new Menu
                {
                    DateColumn = vm.DateColumn.ToUniversalTime(),
                };
                _db.Menus.Add(m);
                _db.SaveChanges();

                ProductMenu pm = new ProductMenu
                {
                    MenuID = m.ID,
                    ProductID = vm.ProductId
                };
                _db.ProductMenus.Add(pm);
                _db.SaveChanges();
            }
            else
            {
                ProductMenu pm = new ProductMenu
                {
                    MenuID = menu.ID,
                    ProductID = vm.ProductId
                };
                if (_db.ProductMenus.Where(p => p.ProductID == vm.ProductId && p.MenuID == menu.ID).FirstOrDefault() is not null)
                {
                    ModelState.AddModelError("NewMenu", "Already in the menu of " + menu.DateColumn.ToString("MMMM dd , yyyy"));

                    return View(vm);
                }
                _db.ProductMenus.Add(pm);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid pid, DateTime date)
        {
            Menu deleteIt = _db.Menus.Include(p => p.ProductMenus).FirstOrDefault(d => d.DateColumn.ToUniversalTime() == date.ToUniversalTime());

            if (deleteIt != null)
            {
                foreach (var item in deleteIt.ProductMenus.ToList())
                {
                    if (item.ProductID == pid)
                    {
                        ProductMenu pm = _db.ProductMenus.FirstOrDefault(p => p.ProductID == item.ProductID && p.MenuID == item.MenuID);
                        _db.ProductMenus.Remove(pm);
                        _db.SaveChanges();
                    }
                }

            }
            return RedirectToAction("Index");
        }

    }
}
