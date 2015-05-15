[assembly: Microsoft.Owin.OwinStartup(typeof(ChatOWIN.ChatStartup))]

namespace ChatOWIN
{
    using Owin;

    public class ChatStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
