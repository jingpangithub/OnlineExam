using OnlineExam.Codes;
using OnlineExam.Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maticsoft.DBUtility;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace OnlineExam.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            Session["status"] = Request.Params["status"];

            return View();
        }

        [Login(Feedback = -7)]
        public ActionResult Login(string Username, string Password)
        {
            BLL.AdminTable bllAdmin = new BLL.AdminTable();
            BLL.TeacherTable bllTeacher = new BLL.TeacherTable();
            BLL.StudentTable bllStudent = new BLL.StudentTable();

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return this.Json(new { result = 0, data = "" });
            }

            string strSql = "Username='" + Username + "' and Password='" + Password + "'";

            List<AdminTable> adminList = bllAdmin.DataTableToList(bllAdmin.GetList(strSql).Tables[0]);
            List<TeacherTable> teacherList = bllTeacher.DataTableToList(bllTeacher.GetList(strSql).Tables[0]);
            List<StudentTable> studentList = bllStudent.DataTableToList(bllStudent.GetList(strSql).Tables[0]);

            if(adminList.Count > 0)
            {
                AdminTable adminModel = adminList[0];
                strSql = "select * from AccessTable where ID = 1 or ID = 2 or ID = 3 or ID = 4 or ID = 5 or ID = 6";

                List<AccessTable> tableList = new BLL.AccessTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

                string sessionString = "";
                if (tableList.Count != 0)
                {
                    sessionString += "[";
                    foreach (AccessTable tableModel in tableList)
                    {
                        sessionString += new JavaScriptSerializer().Serialize(tableModel) + ",";
                    }
                    sessionString = sessionString.Remove(sessionString.Length - 1);
                    sessionString += "]";
                }
                
                Session.Timeout = 30;
                Session["username"] = adminModel.Username;
                Session["access"] = sessionString;
                Session["name"] = adminModel.Name;
                Session["type"] = "管理员";

                return this.Json(new { result = 1, data = "" });
            }
            else if (teacherList.Count > 0)
            {
                TeacherTable teacherModel = teacherList[0];
                strSql = "select * from AccessTable where ID = 3 or ID = 4 or ID = 5 or ID = 6";

                List<AccessTable> tableList = new BLL.AccessTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

                string sessionString = "";
                if (tableList.Count != 0)
                {
                    sessionString += "[";
                    foreach (AccessTable tableModel in tableList)
                    {
                        sessionString += new JavaScriptSerializer().Serialize(tableModel) + ",";
                    }
                    sessionString = sessionString.Remove(sessionString.Length - 1);
                    sessionString += "]";
                }

                Session.Timeout = 30;
                Session["username"] = teacherModel.Username;
                Session["access"] = sessionString;
                Session["name"] = teacherModel.Name;
                Session["type"] = "教师";

                return this.Json(new { result = 1, data = "" });
            }
            else if (studentList.Count > 0)
            {
                StudentTable studentModel = studentList[0];
                strSql = "select * from AccessTable where ID = 7 or ID = 8";

                List<AccessTable> tableList = new BLL.AccessTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

                string sessionString = "";
                if (tableList.Count != 0)
                {
                    sessionString += "[";
                    foreach (AccessTable tableModel in tableList)
                    {
                        sessionString += new JavaScriptSerializer().Serialize(tableModel) + ",";
                    }
                    sessionString = sessionString.Remove(sessionString.Length - 1);
                    sessionString += "]";
                }

                Session.Timeout = 30;
                Session["username"] = studentModel.Username;
                Session["access"] = sessionString;
                Session["name"] = studentModel.Name;
                Session["type"] = "学生";

                return this.Json(new { result = 1, data = "" });
            }           
            else
            {
                return this.Json(new { result = 0, data = "" });
            }
        }

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
        }
    }
}