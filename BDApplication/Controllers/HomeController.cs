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

        public ActionResult News()
        {
            SketcherContext sketcherContext = new SketcherContext();
            return View(sketcherContext.Sketches.Take(100));
        }
    }
}