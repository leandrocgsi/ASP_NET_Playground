using Microsoft.AspNetCore.Mvc;

namespace restful_api_with_aspnet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger/ui/index");
        }
    }
}
