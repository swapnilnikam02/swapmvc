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
            return View();
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
                ViewBag.updatemsg = "<script>alert('Data update')</script>";
                ModelState.Clear();
            }
            else
            {

                ViewBag.updatemsg = "<script>alert('Data not update')</script>";
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var del=db.Categories.Where(model=>model.CategoryId == id).FirstOrDefault();
            return View(del);
        }
        [HttpPost]
        public ActionResult Delete(Category c)
        {
            db.Entry(c).State = EntityState.Deleted;
            
            int a=db.SaveChanges();
            if(a>0)
            {
                ViewBag.Deletemsg= "<script>alert('Data delete')</script>";
            }
            else
            {
                ViewBag.Deletemsg = "<script>alert('Data not delete')</script>";
            }
            //return RedirectToAction("Index");
            return View();
        }
    }
}