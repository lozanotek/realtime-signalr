namespace GiveawayApp.Models {
    using System;

    [Serializable]
    public class SignalRUser {
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
    }
}
