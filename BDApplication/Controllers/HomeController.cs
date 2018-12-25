using BDApplication.Models;
using PagedList;
using PagedList.Mvc;
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
            return View(sketcherContext.Sketches.OrderBy(s=>s.SketchDate));
        }


        [HttpPost]
        public ActionResult News(FormCollection collection,string orderby)
        {
            SketcherContext sketcherContext = new SketcherContext();

            string searchtext = collection["searchtext"];

            List<Sketch> sketches = new List<Sketch>();
            
            if (!String.IsNullOrEmpty(collection["searchtext"]))
            { 
                sketches = sketcherContext.Sketches.Where(s => s.SketchName.Contains(searchtext)).ToList();
            }
            else
            {
                sketches = sketcherContext.Sketches.ToList();
            }

            

                switch (orderby)
                {
                    case "1":
                    sketches = sketches.OrderBy(s=>s.SketchDate).ToList();
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
       
            return View(sketches);
        }
    }
}