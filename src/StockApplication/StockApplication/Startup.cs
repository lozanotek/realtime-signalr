using System;
using System.Configuration;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: Microsoft.Owin.OwinStartup(typeof(StockApplication.Startup))]

namespace StockApplication
{
    public static class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            RegisterServiceBus();
            app.MapSignalR();
        }

        #region Infrastructure
        public static void RegisterServiceBus()
        {
            var connectionString = ConfigurationManager.AppSettings["signalr.ServiceBus"];
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("signalr.ServiceBus", EnvironmentVariableTarget.User);
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return;
            }

            GlobalHost.DependencyResolver.UseServiceBus(connectionString, "StockTicker");
        }
        #endregion
    }
}
