using swapmvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace swapmvc.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            //return View(products);
            var products = db.Products.Include("Category").OrderBy(p => p.ProductId);

            var pagedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = products.Count();

            return View(pagedProducts);
        }
        public ActionResult List()
        {
            var data = db.Products.ToList();
            return View(data);
        }
        
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", p.CategoryId);
            return View(p);
        }
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = db.Products.Include("Category").FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

           ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }


        public ActionResult Delete(int id)
        {
            var del = db.Products.Where(model => model.ProductId == id).FirstOrDefault();
            db.Products.Remove(del);
            db.Entry(del).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}