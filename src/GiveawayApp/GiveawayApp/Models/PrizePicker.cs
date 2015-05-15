namespace GiveawayApp.Models
{
    using Hubs;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    public class PrizePicker
    {
        private IHubConnectionContext<dynamic> Clients { get; set; }

        private static IHubConnectionContext<dynamic> GetHubClients()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotifyHub>();
            return hubContext.Clients;
        } 

        public PrizePicker() : this(GetHubClients())
        {
        }

        public PrizePicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public SignalRUser DrawPrize()
        {
            var winner = UserRegistry.GetRandomUser();
            var userId = winner.UserId;

            Clients.AllExcept(userId).notifyWinner("Loser!");
            Clients.User(userId).notifyWinner("Winner!");

            return winner;
        }
    }
}
