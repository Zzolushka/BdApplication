using BDApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDApplication.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            SketcherContext db = new SketcherContext();

            return View(db.Rooms);
        }

        public ActionResult AddRoom()
        {
            Random rnd = new Random();
            using (SketcherContext db = new SketcherContext())
            {

                Room newRoom = new Room() { Image = "https://res.cloudinary.com/djrk897te/image/upload/v1535048158/2_vhpsaw.jpg", Name = "New Room", Id = rnd.Next(0, 100) };
                db.Rooms.Add(newRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        public ActionResult DrowImage(int id)
        {
            if (id != 0 && id != null)
            {
                ViewBag.id = id;

                SketcherContext db = new SketcherContext();
                if (db.Rooms.SingleOrDefault(x => x.Id == id) != null)
                {
                    ViewBag.image = db.Rooms.SingleOrDefault(x => x.Id == id).Image;
                    return View();
                }
                else
                    return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }


    }
}