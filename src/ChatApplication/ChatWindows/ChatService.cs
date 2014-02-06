namespace ChatWindows {
    using System;
    using Microsoft.AspNet.SignalR.Client;

    public class ChatService {
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event ChatSend Received;

        private HubConnection Connection { get; set; }
        private IHubProxy Proxy { get; set; }

        public async void Connect(string serviceUrl) {
            Connection = new HubConnection(serviceUrl);
            Proxy = Connection.CreateHubProxy("ChatHub");
            Proxy.On<string, string>("BroadcastMessage", OnSend);

            await Connection.Start();
            OnConnected();
        }

        public void Disconnect() {
            if (Connection == null) {
                return;
            }

            if (Connection.State != ConnectionState.Connected) {
                return;
            }

            Connection.Stop();
            OnDisconnected();
        }

        protected virtual void OnDisconnected() {
            var handler = Disconnected;
            
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        public virtual async void Send(string name, string message) {
            await Proxy.Invoke<string>("Send", name, message);
        }

        protected virtual void OnSend(string name, string message) {
            var handler = Received;
            
            if (handler != null) {
                handler(name, message);
            }
        }

        protected virtual void OnConnected() {
            var handler = Connected;
            
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }
    }
}