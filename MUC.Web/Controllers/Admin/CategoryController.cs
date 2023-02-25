using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Utilities;
using NuGet.Versioning;
using System.Data;

namespace MUC.Web.Controllers.Admin
{
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList.OrderBy(c => c.DisplayOrder));
        }

        // get action
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order can't be the same as the Name");
                return View(obj);
            }
            if (_db.Categories.Any(n => n.Name == obj.Name))
            {
                ModelState.AddModelError("Name", "The Category name already exists!");
                return View(obj);
            }
            if(_db.Categories.Any(displayOrder => displayOrder.DisplayOrder == obj.DisplayOrder))
            {
                ModelState.AddModelError("DisplayOrder", "You can't have the same display order as another category");
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View(obj);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var categoryInDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryInDb == null)
            {
                return NotFound();
            }
            return View(categoryInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order can't be the same as the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View(obj);
        }

        public IActionResult Details(Guid? id)
        {
            var categoryInDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            return View(categoryInDb);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var categoryInDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryInDb == null)
            {
                return NotFound();
            }
            return View(categoryInDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid? id)
        {
            var categoryInDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryInDb != null)
            {
                _db.Categories.Remove(categoryInDb);
                _db.SaveChanges();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index", "Category");
            }
            return NotFound();
        }
    }
}
