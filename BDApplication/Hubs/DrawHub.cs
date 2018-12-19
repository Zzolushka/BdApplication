using BDApplication.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BDApplication.Hubs
{
    public class DrawHub : Hub
    {
        SketcherContext db = new SketcherContext();

        public Task Send(string groupName, Data data)
        {
            return Clients.Group(groupName).addLine(data);
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public void SaveImage(int id, string image)
        {
            if (db.Rooms.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Rooms.SingleOrDefault(x => x.Id == id).Image = image;
                db.SaveChanges();
            }
        }

        public void ChangeName(int id, string name)
        {
            if (db.Rooms.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Rooms.SingleOrDefault(x => x.Id == id).Name = name;
                db.SaveChanges();
            }
        }

    }
}