using OnlineExam.Codes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    [Login]
    public class TeacherController : BaseController
    {
        BLL.TeacherTable teacher = new BLL.TeacherTable();

        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult AjaxList()
        {
            int start = 0;
            int length = 0;

            string sqlString = "";
            string search = Request.Params["search"];

            sqlString += String.IsNullOrEmpty(search) ? "1 = 1" : ("Username Like ('%" + search + "%') or Name Like (N'%" + search + "%')");

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            List<Model.TeacherTable> list = teacher.DataTableToList(teacher.GetListByPage(sqlString, "", start + 1, start + length).Tables[0]);
            int total = teacher.GetRecordCount("");
            int totalFilter = teacher.GetRecordCount(sqlString);

            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = totalFilter,
                    data = list.Select(o => new
                    {
                        ID = o.ID,
                        Username = o.Username,
                        Password = o.Password,
                        Name = o.Name,
                        Sex = o.Sex,
                    })
                }
            });
        }

        [Login(Feedback = 100)]
        public ActionResult Create(Model.TeacherTable model)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Name))
            {
                model.Password = encryptPwd(model.Password);

                var state = teacher.Add(model);
                if (state != 0)
                {
                    result = true;
                }

            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "新建失败" });
        }

        public override ActionResult Info(int id)
        {
            bool result = false;
            Model.TeacherTable model = teacher.GetModel(id);
            if (model != null)
                result = true;

            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.TeacherTable model)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Name))
            {
                model.Password = encryptPwd(model.Password);

                result = teacher.Update(model);
                if (result)
                {
                    result = true;
                }
            }

            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "修改失败" });

        }

        public override ActionResult Delete(int id)
        {
            if (teacher.Delete(id))
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
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
    }
}