using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(BDApplication.Startup))]
namespace BDApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;
            app.MapSignalR();
        }
    }
}