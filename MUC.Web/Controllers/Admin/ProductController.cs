using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Models.ViewModels;
using MUC.Utilities;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;

namespace MUC.Web.Controllers.Admin
{
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: ProductController
        public IActionResult Index()
        {
            var products = _db.Products.Include(c => c.Category).ToList();
            return View(products);
        }

        // view just one product       
        // GET: ProductController/Details/5
        public IActionResult Details(Guid id)
        {
            Product product = _db.Products.Include(c => c.Category).FirstOrDefault(i => i.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductController/Create
        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            return View(productVM);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IIActionResult Create(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (_db.Products.Any(name => name.Title == obj.Product.Title))
                {
                    ModelState.AddModelError("Title", "The name of the food or beverage is already in the database");
                    return View(obj);
                }
              
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImgURL = @"\images\products\" + fileName + extension;
                }
           

                //Console.WriteLine(obj.Product.Category.Name);
                _db.Products.Add(obj.Product);
                _db.SaveChanges();
                TempData["success"] = "Product Was Created Successfully";
               
                return RedirectToAction("Index", "Product");
            }

            return View(obj);
        }


        // GET: ProductController/Edit/5
        public IActionResult Edit(Guid id)
        {
            ProductVM productVM = new()
            {
                Product = _db.Products.Include(c => c.Category).FirstOrDefault(i => i.Id == id),
                CategoryList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
                        
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IIActionResult Edit(ProductVM obj, IFormFile? file)
        {
            var product = _db.Products.AsNoTracking().Include(c=> c.Category).FirstOrDefault(i => i.Id == obj.Product.Id);
            if(product== null)
            {
                ModelState.AddModelError("product", "Id is invalid");
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Product.ImgURL != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImgURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImgURL = @"\images\products\" + fileName + extension;
                }
                if(file == null)
                {
                    obj.Product.ImgURL = product.ImgURL;
                }   
                _db.Products.Update(obj.Product);

                _db.SaveChanges();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View(obj);
        }


        // GET: ProductController/Delete/5
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Product product = _db.Products.FirstOrDefault(c => c.Id == id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(Guid id)
        {
            Product obj = _db.Products.FirstOrDefault(i => i.Id == id);

            if (obj == null)
            {
                return NotFound("Something went wrong");

            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImgURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index", "Product");
        }
    }
}


