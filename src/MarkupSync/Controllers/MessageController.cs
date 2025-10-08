using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MarkupSync.Services;

namespace MarkupSync.Controllers;

[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService messageService;

    public MessageController(IMessageService messageService)
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
