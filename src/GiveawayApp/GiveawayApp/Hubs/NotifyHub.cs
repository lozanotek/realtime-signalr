namespace GiveawayApp.Hubs
{
    using System.Threading.Tasks;
    using Models;
    using Microsoft.AspNet.SignalR;

    public class NotifyHub : Hub
    {
        public override Task OnConnected()
        {
            var user = this.GetUser();
            UserRegistry.AddUser(user);

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var user = this.GetUser();
            UserRegistry.AddUser(user);

            return base.OnReconnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var user = this.GetUser();
            UserRegistry.RemoveUser(user);

            return base.OnDisconnected(stopCalled);
        }
    }
}
