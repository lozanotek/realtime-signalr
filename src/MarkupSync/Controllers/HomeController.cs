using Microsoft.AspNetCore.Mvc;

namespace MarkupSync.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
