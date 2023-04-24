using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApplication_Vikrant.Models;

namespace BlogApplication_Vikrant.Controllers
{
    public class BlogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            Guid Id1 = (Guid)Session["Id"];
            var userData = (from user in db.Blogs
                            where user.PostedBy == Id1
                            select user).ToList();

            return View(userData);
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Blog blog)
        {
            string filename = Path.GetFileName(file.FileName);
        string _filename = DateTime.Now.ToString("yymmssfff") + filename;
        string extension = Path.GetExtension(file.FileName);


        string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
        blog.Image = "~/Content/Images/" + _filename;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (file.ContentLength< 100000)
                    {
                        blog.PostedBy = (Guid) Session["Id"];
                         blog.PostedName = (String)Session["username"];
                        db.Blogs.Add(blog);
                        db.SaveChanges();

                        file.SaveAs(path);
                        ViewBag.msg = "Student Added";
                        ModelState.Clear();
                        return RedirectToAction("Index");

    }
                    else
                    {
                        ViewBag.msg = "File Size should be Less than 1 Mb";
                    }
                }
                else
{
    ViewBag.msg = "Invalid File Type";
}
return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            Session["ImgPath"] = blog.Image;
            Session["PostedId"] = blog.PostedBy;
            Session["PostedByName"] = blog.PostedName;

            return View(blog);
        }


        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.PostedBy = (Guid)Session["PostedId"];
                blog.PostedName = (String)Session["PostedByName"];
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);


                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
                    blog.Image = "~/Content/Images/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength < 100000)
                        {
                           
                            string OldImgPath = Request.MapPath(Session["ImgPath"].ToString());
                            blog.PostedBy = (Guid)Session["PostedId"];
                            blog.PostedName = (String)Session["PostedByName"];
                            db.Entry(blog).State = EntityState.Modified;
                            db.SaveChanges();

                            file.SaveAs(path);
                            if (System.IO.File.Exists(OldImgPath))
                            {
                                System.IO.File.Delete(OldImgPath);
                            }
                            ViewBag.msg = "Student Added";
                            TempData["msg"] = "Data Updated";
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ViewBag.msg = "File Size should be Less than 1 Mb";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Invalid File Type";
                    }
                }
            }
            else
            {
                blog.Image = Session["ImgPath"].ToString();
                blog.PostedBy = (Guid)Session["PostedId"];
                blog.PostedName = (String)Session["PostedByName"];
                db.Entry(blog).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    TempData["msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }
            }

            return View();
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
