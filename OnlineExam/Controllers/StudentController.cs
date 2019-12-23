using OnlineExam.Codes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    [Login]
    public class StudentController : BaseController
    {
        BLL.StudentTable student = new BLL.StudentTable();

        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult AjaxList()
        {
            int start = 0;  
            int length = 0; 

            string sqlString = "";
            string choose = Request.Params["choose"];
            string search = Request.Params["search"];

            sqlString += String.IsNullOrEmpty(choose) ? "1 = 1" : ("Major = N'" + choose + "'");
            sqlString += " AND ";
            sqlString += String.IsNullOrEmpty(search) ? "1 = 1" : ("(Username Like ('%" + search + "%') or Name Like (N'%" + search + "%'))");

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            List<Model.StudentTable> list = student.DataTableToList(student.GetListByPage(sqlString, "", start + 1, start + length).Tables[0]);
            int total = student.GetRecordCount("");
            int totalFilter = student.GetRecordCount(sqlString);

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
                        Major = o.Major,
                    })
                }
            });
        }

        [Login(Feedback = 100)]
        public ActionResult Create(Model.StudentTable model)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Name))
            {
                model.Password = encryptPwd(model.Password);

                var state = student.Add(model);
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
            Model.StudentTable model = student.GetModel(id);
            if (model != null)
                result = true;

            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.StudentTable model)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Name))
            {
                model.Password = encryptPwd(model.Password);

                result = student.Update(model);
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
            if (student.Delete(id))
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
        }

        [HttpPost]
        public string Import(HttpPostedFileBase file)
        {
            if(file == null)
            {
                return "文件为空";
            }

            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "StudentFile"));
            string path = Path.Combine(filePath, fileName);
            file.SaveAs(path);

            DataTable excelTable = new DataTable();
            excelTable = ImportExcel.GetExcelDataTable(path);

            DataTable dbdata = new DataTable();
            dbdata.Columns.Add("id");
            dbdata.Columns.Add("username");
            dbdata.Columns.Add("password");
            dbdata.Columns.Add("name");
            dbdata.Columns.Add("sex");
            dbdata.Columns.Add("major");

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dbdata.NewRow();
                dr_["id"] = 0;
                dr_["username"] = dr["学号"];
                dr["密码"] = encryptPwd(dr["密码"].ToString());
                dr_["password"] = dr["密码"];
                dr_["name"] = dr["姓名"];
                dr_["sex"] = dr["性别"];
                dr_["major"] = dr["专业"];
                dbdata.Rows.Add(dr_);
            }

            RemoveEmpty(dbdata);

            List<Model.StudentTable> list = student.DataTableToList(dbdata);
            if(list.Count > 0)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    Model.StudentTable studentModel = list[i];
                    student.Add(studentModel);
                }
            }            

            return "导入成功";
        }

        protected void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
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
    }
}