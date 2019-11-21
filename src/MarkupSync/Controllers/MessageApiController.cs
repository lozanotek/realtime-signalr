using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MarkupSync.Services;

namespace MarkupSync.Controllers
{
    public class MessageApiController : Controller
    {
        private readonly IMessageService messageService;

        public MessageApiController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [AllowAnonymous, HttpGet("/messages")]
        public JsonResult GetCurrentFeed()
        {
            var currentMessage = messageService.GetActiveMessage();
            return new JsonResult(currentMessage);
        }
    }
}
