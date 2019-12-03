using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
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