using OnlineExam.Codes;
using OnlineExam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Maticsoft.DBUtility;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace OnlineExam.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session["status"] = Request.Params["status"];

            return View();
        }

        //User属于SQL保留字，建表注意表名设计
        [Login(Feedback = -7)]
        public ActionResult Login(string Username, string Password)
        {
            OnlineExam.BLL.UserTable bllUser = new OnlineExam.BLL.UserTable();
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return this.Json(new { result = 0, data = "" });
            }
            // Password = encryptPwd(Password);

            string strSql = "Username='" + Username + "' and Password='" + Password + "'";
            List<Model.UserTable> userList = bllUser.DataTableToList(bllUser.GetList(strSql).Tables[0]);

            //authority
            if (userList.Count > 0)
            {
                Model.UserTable userModel = userList[0];
                string d = userModel.UserType;

                if (d.Equals("管理员"))
                    strSql = "select * from AccessTable where ID = 1 union all select * from AccessTable where ID = 2";
                if (d.Equals("教师"))
                    strSql = "select * from AccessTable where ID = 1";
                else
                    strSql = "select * from AccessTable where ID = 1";

                List<Model.AccessTable> tableList = new BLL.AccessTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

                //get granted table
                string sessionString = "";
                if (tableList.Count != 0)
                {
                    sessionString += "[";
                    foreach (Model.AccessTable tableModel in tableList)
                    {
                        sessionString += new JavaScriptSerializer().Serialize(tableModel) + ",";
                    }
                    sessionString = sessionString.Remove(sessionString.Length - 1);
                    sessionString += "]";
                }


                //set session time out
                Session.Timeout = 30;
                Session["username"] = userModel.Username;
                Session["access"] = sessionString;
                return this.Json(new { result = 1, data = "" });
            }
            else
            {
                return this.Json(new { result = 0, data = "" });
            }
        }

        /*此方法已过时
        public string encryptPwd(string pwd)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
        }
        */

        //MD5散列
        private static string encryptPwd(string pwd)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pwd));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            return RedirectToAction("Index", "Login");
            //return this.Json(new { result = 1, data = "" });
        }
    }
}