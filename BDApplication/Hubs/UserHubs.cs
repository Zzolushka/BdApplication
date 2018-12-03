using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BDApplication.Models;
using Microsoft.AspNet.SignalR;

namespace BDApplication.Hubs
{
    public class UserHubs : Hub
    {
        public void ChangePhotoPath(string url,string username)
        {
            SketcherContext sketcherContext = new SketcherContext();
            sketcherContext.Users.FirstOrDefault(u => u.UserName == username).UserPhotoPath = url;
            sketcherContext.SaveChanges();
        }
    }
}