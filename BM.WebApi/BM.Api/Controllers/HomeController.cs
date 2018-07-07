using System.Web.Mvc;

namespace BM.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/swagger/ui/index");
        }
    }
}
