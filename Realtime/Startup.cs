﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Realtime.Startup))]

namespace Realtime
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Pour plus d'informations sur la configuration de votre application, visitez https://go.microsoft.com/fwlink/?LinkID=316888

            app.MapSignalR();
        }
    }
}
