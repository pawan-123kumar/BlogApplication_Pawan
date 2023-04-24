using BlogApplication_Vikrant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace BlogApplication_Vikrant.Controllers
{
   
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       

        public ActionResult Index(string searchString)
        {
            var blogs = from b in db.Blogs where b.Status != "Disable"
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(b => b.Title.Contains(searchString) || b.PostedName.Contains(searchString));
            }

            return View(blogs);
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Enable(int? id)
        {
           Blog blog = db.Blogs.Find(id);
            blog.Status = "Enable";
            db.Entry(blog).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DisabledPosts", "Home");
        }
        public ActionResult Disable(int? id)
        {
            Blog blog = db.Blogs.Find(id);
            blog.Status = "Disable";
            db.Entry(blog).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(Roles ="Admin")]
        public ActionResult DisabledPosts()
        {

            var blogs = from b in db.Blogs
                        where b.Status == "Disable"
                        select b;

            return View(blogs);
        }

    }
}

