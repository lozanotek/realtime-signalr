using Microsoft.AspNetCore.Mvc;
using MarkupSync.Services;

namespace MarkupSync.Controllers
{
    public class HomeController : Controller
    {
       private readonly IMessageService messageService;

        public HomeController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            var currentMessage = messageService.GetActiveMessage();
            return View("Index", currentMessage);
        }
    }
}
