using System.Web.Mvc;
using OnlineExam.Codes;

namespace OnlineExam.Controllers
{
    public class HomeController : Controller
    {
        [Login(Feedback = 200)]
        public ActionResult Index()
        {
            return View();
        }
    }
}