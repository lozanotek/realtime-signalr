using System.Configuration;
using System.Web.Http;
using Jarvis.Hubs;
using Jarvis.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Jarvis.Controllers
{
    public class ActionController : ApiController
    {
        private IHubConnectionContext<dynamic> Clients { get; set; }

        private static IHubConnectionContext<dynamic> GetHubClients()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotifyHub>();
            return hubContext.Clients;
        }

        public ActionController() : this(GetHubClients())
        {
        }

        public ActionController(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public void Post([FromBody]TwilioData data)
        {
            var body = data.Body;
            if (string.IsNullOrEmpty(body))
            {
                return;
            }

            body = body.ToLower();
            if (!body.Contains("play"))
            {
                return;
            }

            body = body
                .Replace("play", "")
                .ToLower()
                .Trim();

            var videoKey = ConfigurationManager.AppSettings[body];
            if (string.IsNullOrEmpty(videoKey))
            {
                return;
            }

            Clients.All.deliverMessage(data.From, videoKey);
        }
    }
}
