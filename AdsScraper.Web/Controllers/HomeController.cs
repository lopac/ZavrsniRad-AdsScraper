using System.Web.Mvc;
using AdsScraper.Web.ViewModels;

namespace AdsScraper.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View(new CarPickerViewModel());
        }
    }
}