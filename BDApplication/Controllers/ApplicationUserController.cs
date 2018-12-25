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
            Random rnd = new Random();
            if(user.UserName==null || user.UserPassword== null)
            {
                if(user.UserName == null)
                {
                    ModelState.AddModelError("UserName", "Введите имя пользователя");
                }
                else
                {
                    ModelState.AddModelError("UserPassword", "Введите пароль пользователя");
                }
                return View();
            }
            else 
            if (sketcherContext.Users.FirstOrDefault(u => u.UserName == user.UserName) == null)
            {
                user.UserPhotoPath = "https://res.cloudinary.com/djrk897te/image/upload/v1543826818/widgetdocs/user-default_jysqcc.png";
                sketcherContext.Users.Add(user);
                sketcherContext.SaveChanges();
                sketcherContext.Rooms.Add(new Room() { Image = "", Name = "New Room", Id = rnd.Next(0, 100), UserId = user.UserId });
                sketcherContext.SaveChanges();
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("UserName","Такой пользователь уже есть");
                return View();
            }
        }

        public ActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(User user)
        {
            if (sketcherContext.Users.FirstOrDefault(u => u.UserName == user.UserName) != null)
            {
                if(user.UserPassword!=sketcherContext.Users.FirstOrDefault(u => u.UserName == user.UserName).UserPassword)
                {
                    ModelState.AddModelError("UserPassword", "Неверный пароль");
                    return View();
                }


                FormsAuthentication.SetAuthCookie(user.UserName, true);
                return RedirectToAction("ShowUserPage");
            }
            else
            {
                ModelState.AddModelError("UserName", "Нету такого пользователя");
                return View();
            }
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

        [HttpGet]
        [Authorize]
        public ActionResult ShowUserPage()
        {
            var currentUser =  sketcherContext.Users.FirstOrDefault(u=>u.UserName==User.Identity.Name);
            var sketches = sketcherContext.Sketches.Where(s => s.UserId == currentUser.UserId);
            ViewBag.sketches = sketches;
            ViewBag.UserName = User.Identity.Name;
            return View(currentUser);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ShowUserPage(FormCollection collection, string orderby)
        {
            var currentUser = sketcherContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.UserName = User.Identity.Name;

            string searchtext = collection["searchtext"];

            List<Sketch> sketches = new List<Sketch>();

            if (!String.IsNullOrEmpty(collection["searchtext"]))
            {
                sketches = sketcherContext.Sketches.Where(s => s.UserId==currentUser.UserId).Where(s => s.SketchName.Contains(searchtext)).ToList();
            }
            else
            {
                sketches = sketcherContext.Sketches.Where(s => s.UserId == currentUser.UserId).ToList();
            }



            switch (orderby)
            {
                case "1":
                    sketches = sketches.OrderBy(s => s.SketchDate).ToList();
                    break;
                case "2":
                    sketches = sketches.OrderBy(s => s.SketchName).ToList();
                    break;
                case "3":
                    sketches = sketches.OrderBy(s => s.SketchCategory).ToList();
                    break;
                default:
                    break;
            }

            ViewBag.sketches = sketches;

            return View(currentUser);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        




    }
}