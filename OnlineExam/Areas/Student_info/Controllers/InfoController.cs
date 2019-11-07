using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Areas.Student_info.Controllers
{
    public class InfoController : Controller
    {
        // GET: Student_info/Info
        public ActionResult Index(String id)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(id);
            }
            else
            {
                return View(id);
            }
        }
    }
}