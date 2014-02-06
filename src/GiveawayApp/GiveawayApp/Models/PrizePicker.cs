namespace GiveawayApp.Models {
    using Hubs;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    public class PrizePicker {
        private IHubConnectionContext Clients { get; set; }

        public PrizePicker()
            : this(GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients) {
        }

        public PrizePicker(IHubConnectionContext clients) {
            Clients = clients;
        }

        public SignalRUser DrawPrize() {
            var winner = UserRegistry.GetRandomUser();
            var userId = winner.UserId;

            Clients.AllExcept(userId).notifyWinner("Not a Winner");
            Clients.User(userId).notifyWinner("Winner!");

            return winner;
        }
    }
}
