namespace GiveawayApp.Controllers
{
    using System.Web.Mvc;
    using Models;

    public class PickController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string password)
        {
            if (!Security.ValidatePassword(password))
            {
                return View();
            }

            var prizePicker = new PrizePicker();
            var winner = prizePicker.DrawPrize();
            return View(winner);
        }
    }
}
