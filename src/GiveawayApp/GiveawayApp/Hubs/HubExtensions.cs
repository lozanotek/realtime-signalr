namespace GiveawayApp.Hubs
{
    using Models;
    using Microsoft.AspNet.SignalR;

    public static class HubExtensions
    {
        public static SignalRUser GetUser(this Hub hub)
        {
            if (hub == null)
            {
                return null;
            }

            var context = hub.Context;
            if (context == null)
            {
                return null;
            }

            var connectionId = context.ConnectionId;
            var userId = context.User.Identity.Name;

            return new SignalRUser
            {
                ConnectionId = connectionId,
                UserId = userId
            };
        }
    }
}