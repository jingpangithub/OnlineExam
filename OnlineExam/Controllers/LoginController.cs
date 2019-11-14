using OnlineExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineExam.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(LoginModel model)
        {   
            if(model == null)
                return View();
            else
            {
                System.Console.WriteLine("************************************");
                if (!ModelState.IsValid)
                {
                    //用户输入服务端验证,此处处理验证不通过的提示代码             
                    return View();
                }

                string result = model.Login();

                if (result == null)
                {
                    //用户名或者密码不正确的提示代码 
                    return View();
                }
                else if (result == "admin")
                {
                    return Redirect(Url.Action("Index", new { area = "Admin", controller = "Admin" }));
                }
                else if (result == "student")
                {
                    return Redirect(Url.Action("Index", new { area = "Student", controller = "Student" }));
                }
                else if (result == "teacher")
                {
                    return Redirect(Url.Action("Index", new { area = "Teacher", controller = "Teacher" }));
                }
                else
                {

                    ActionResult empty = new EmptyResult();
                    return empty;
                }
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]//该注解可以防止跨站攻击
        ////[ActionName("Index")]//该注解可以更改路由中Action名称
        //public ActionResult LoginCheck(LoginModel model)
        //{
        //    System.Console.WriteLine("************************************");
        //    if (!ModelState.IsValid)
        //    {
        //        //用户输入服务端验证,此处处理验证不通过的提示代码             
        //        return View();
        //    }

        //    string result = model.Login();

        //    if (result == null)
        //    {
        //        //用户名或者密码不正确的提示代码 
        //        return View();
        //    }
        //    else if (result == "admin")
        //    {
        //        return Redirect(Url.Action("Index", new { area = "Admin", controller = "Admin" }));
        //    }
        //    else
        //    {
        //        ////用户登陆核心代码
        //        //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
        //        //     1,
        //        //     model.userName,
        //        //     DateTime.Now,
        //        //     DateTime.Now.AddHours(240),//记住密码的时间
        //        //     false,//是否保存cookie 记住密码
        //        //     result //获取的用户权限列表 用逗号分割的字符串
        //        //     );
        //        //string encryptedTickt = FormsAuthentication.Encrypt(authTicket);
        //        //HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTickt);
        //        //Response.Cookies.Add(authCookie);

        //        //Response.Redirect(Url.Action("Index","Login"), true);

        //        ActionResult empty = new EmptyResult();
        //        return empty;
        //    }
        //    //return View();
        //}
    }
}