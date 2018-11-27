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
            sketcherContext.Comments.Add(new Comment { CommentDescription = description,SketchId=sketchid, UserId=1});
            sketcherContext.SaveChanges();
        }
    }
}