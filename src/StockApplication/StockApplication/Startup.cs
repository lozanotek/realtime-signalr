[assembly: Microsoft.Owin.OwinStartup(typeof(StockApplication.Startup))]

namespace StockApplication {
    using System.Configuration;
    using Owin;
    using Microsoft.AspNet.SignalR;

    public static class Startup {
        public static void Configuration(IAppBuilder app) {
            RegisterServiceBus();
            app.MapSignalR();
        }

        #region Infrastructure
        public static void RegisterServiceBus() {
            var connectionString = ConfigurationManager.AppSettings["signalR.ServiceBus"];
            if (string.IsNullOrEmpty(connectionString)) {
                return;
            }

            GlobalHost.DependencyResolver.UseServiceBus(connectionString, "StockTicker");
        }
        #endregion
    }
}
