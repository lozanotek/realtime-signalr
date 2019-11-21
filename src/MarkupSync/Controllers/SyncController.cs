using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using MarkupSync.Hubs;
using MarkupSync.Models;
using MarkupSync.Services;

namespace MarkupSync.Controllers
{
    [Authorize]
    [Route("sync")]
    public class SyncController : Controller
    {
        private readonly IHubContext<MessageHub> messageHub;
        private readonly IMessageService messageService;

        public SyncController(IHubContext<MessageHub> messageHub, IMessageService messageService)
        {
            this.messageHub = messageHub;
            this.messageService = messageService;
        }

        [HttpPost("message")]
        public IActionResult SyncMessage([FromBody]Message message)
        {
            if (message == null)
            {
                return BadRequest("Invalid sync data");
            }

            var content = message.Content;
            if (!string.IsNullOrWhiteSpace(content))
            {
                message.Content = content.Trim();
            }
            
            messageService.SetActiveMessage(message);

            messageHub.Clients.All.SendAsync("syncMessage", message);
            return Ok(message);
        }
    }
}
