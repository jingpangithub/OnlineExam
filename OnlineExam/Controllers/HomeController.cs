using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExam.Codes;
using OnlineExam.Common;
using Maticsoft.DBUtility;

namespace OnlineExam.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Login(Feedback = 200)]
        public ActionResult Index()
        {

            return View();
        }
    }
}