using BDApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDApplication.Controllers
{
    public class SketchController : Controller
    {
        SketcherContext sketcherContext = new SketcherContext();
        // GET: Sketch
        public ActionResult Index()
        {
            return View(sketcherContext.Sketches);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sketch sketch)
        {
            sketch.User = sketcherContext.Users.FirstOrDefault(u=>u.UserName==User.Identity.Name);
            sketcherContext.Sketches.Add(sketch);
            sketcherContext.SaveChanges();
            return RedirectToAction("ShowUserPage","ApplicationUser");
        }

        public ActionResult ShowSketchPage(int sketchid)
        {
            if(sketcherContext.Users.FirstOrDefault(u=>u.UserName == User.Identity.Name)!=null)
            {
                ViewBag.userId = sketcherContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;
            }
            var sketch = sketcherContext.Sketches.FirstOrDefault(s => s.SketchId == sketchid);
            return View(sketch);
        }





        


    }
}