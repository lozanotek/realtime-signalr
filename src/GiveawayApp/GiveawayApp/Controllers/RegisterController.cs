namespace GiveawayApp.Controllers {
    using System.Web.Mvc;
    using System.Web.Security;

    public class RegisterController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email) {
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
