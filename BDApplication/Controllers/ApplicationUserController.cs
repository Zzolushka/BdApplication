using BDApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BDApplication.Controllers
{
    public class ApplicationUserController : Controller
    {
        public SketcherContext sketcherContext = new SketcherContext();
        // GET: ApplicationUser
        public ActionResult Index()
        {
            return View(sketcherContext.Users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            user.UserPhotoPath = "https://res.cloudinary.com/djrk897te/image/upload/v1543826818/widgetdocs/user-default_jysqcc.png";
            sketcherContext.Users.Add(user);
            sketcherContext.SaveChanges();
            FormsAuthentication.SetAuthCookie(user.UserName, true);
            return RedirectToAction("Index");
        }

        public ActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(User user)
        {
            var currentUser=sketcherContext.Users.Find(user);
            if (currentUser != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = sketcherContext.Users.Find(id);
            sketcherContext.Users.Remove(user);
            sketcherContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            return View(sketcherContext.Users.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            sketcherContext.Entry(user).State = EntityState.Modified;
            sketcherContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ShowUserPage()
        {
            var currentUser =  sketcherContext.Users.FirstOrDefault(u=>u.UserName==User.Identity.Name);
            ViewBag.UserName = User.Identity.Name;
            return View(currentUser);
        }




    }
}