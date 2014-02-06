[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(GiveawayApp.Startup))]

namespace GiveawayApp {
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Infrastructure;
    using Owin;

    public class Startup {
        public void Configuration(IAppBuilder app) {
            // Specify that UserId should be pull from the IIdentity.Name
            GlobalHost.DependencyResolver.Register(typeof (IUserIdProvider), () => new PrincipalUserIdProvider());
            app.MapSignalR();
        }
    }
}
