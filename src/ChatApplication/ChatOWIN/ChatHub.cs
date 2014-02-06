namespace ChatOWIN {
    using Microsoft.AspNet.SignalR;

    public class ChatHub : Hub {
        public void Send(string name, string message) {
            Clients.All.broadcastMessage(name, message);
        }
    }
}
