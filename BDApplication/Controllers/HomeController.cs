using BDApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SketcherContext sketcherContext = new SketcherContext();
            return View(sketcherContext.Sketches.Take(4));
        }

        [HttpGet]
        public ActionResult News()
        {
            SketcherContext sketcherContext = new SketcherContext();
            return View(sketcherContext.Sketches.Take(100));
        }

        [HttpPost]
        public ActionResult News(string searchtext)
        {
            SketcherContext sketcherContext = new SketcherContext();

            List<Sketch> sketches = new List<Sketch>();
            
            if (!String.IsNullOrEmpty(searchtext))
            { 
                sketches = sketcherContext.Sketches.Where(s => s.SketchName.Contains(searchtext)).ToList();
            }
          return View(sketches);
        }
    }
}