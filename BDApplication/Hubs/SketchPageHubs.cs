using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BDApplication.Models;
using Microsoft.AspNet.SignalR;

namespace BDApplication.Hubs
{
    public class SketchPageHubs : Hub
    {
        public void SendCommnet(string description,int sketchid,int userid)
        {
            SketcherContext sketcherContext = new SketcherContext();
            sketcherContext.Users.FirstOrDefault(u => u.UserId == userid).Comments.Add(new Comment { CommentDescription = description, SketchId = sketchid });
            sketcherContext.SaveChanges();
        }
    }
}