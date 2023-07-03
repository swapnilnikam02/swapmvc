using swapmvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace swapmvc.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        AppDbContext db=new AppDbContext(); 
        public ActionResult Index()
        {
            var data=db.Categories.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var row=db.Categories.Where(model=>model.CategoryId==id).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
   public ActionResult Edit(Category c) 
        {
            db.Entry(c).State=EntityState.Modified;
            int a=db.SaveChanges();
            if(a>0)
            {
                ViewBag.updatemsg = "<script>alert('Category has updated sucessfully.')</script>";
                ModelState.Clear();
            }
            else
            {

                ViewBag.updatemsg = "<script>alert('Category has not updated.')</script>";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var del=db.Categories.Where(model=>model.CategoryId == id).FirstOrDefault();
            db.Categories.Remove(del);
            db.Entry(del).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}