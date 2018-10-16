using System.Web.Mvc;

namespace DziaroSopas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }
        
        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }
    }
}