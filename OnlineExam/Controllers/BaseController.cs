using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    public class BaseController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult AjaxList()
        {
            return View();
        }

        public virtual ActionResult Info(int i)
        {
            return View();
        }

        public virtual ActionResult Delete(int i)
        {
            return View();
        }

        public virtual ActionResult Confirm()
        {
            return View();
        }
    }
}