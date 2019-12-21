using Maticsoft.DBUtility;
using OnlineExam.Codes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    [Login]
    public class ExamStartController : BaseController
    {
        BLL.ExamTable exam = new BLL.ExamTable();

        public override ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase file = Request.Files[0];
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "ExamFile"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return this.Json(new
            {
                url = "/ExamFile/" +
                fileName
            });
        }

        public override ActionResult AjaxList()
        {
            int start = 0;
            int length = 0;

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);
            
            var strSql = "";
            if (Session["type"].Equals("管理员"))
            {
                strSql = "Nowstate = N'未开启'";
            }
            else
            {
                strSql = "Nowstate = N'未开启' and Teacher = '" + Session["username"] + "'";
            }

            List<Model.ExamTable> list = exam.DataTableToList(exam.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int filterTotal = exam.GetRecordCount(strSql);
            int total = exam.GetRecordCount("");            

            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = filterTotal,
                    data = list.Select(o => new
                    {
                        ID = o.ID,
                        Teacher = o.Teacher,
                        Major = o.Major,
                        Start = o.Start,
                        Last = o.Last,
                        Filepath = o.Filepath,
                    })
                }
            });
        }

        [Login(Feedback = 100)]
        public ActionResult Create(Model.ExamTable model)
        {
            bool result = false;
          
            if (!String.IsNullOrEmpty(model.Teacher) && !String.IsNullOrEmpty(model.Major))
            {
                BLL.ExamTable exam = new BLL.ExamTable();
                model.Nowstate = "未开启";
                var state = exam.Add(model);

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
            Model.ExamTable model = exam.GetModel(id);
            if (model != null)
                result = true;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.ExamTable model)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(model.Teacher) && !String.IsNullOrEmpty(model.Major))
            {
                model.Nowstate = "未开启";
                result = exam.Update(model);
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
            BLL.ExamTable exam = new BLL.ExamTable();
            string strSql = "select * from ExamTable where Id = '" + id + "'";
            List<Model.ExamTable> tableList = new BLL.ExamTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

            if (tableList.Count != 0)
            {
                foreach (Model.ExamTable tableModel in tableList)
                {
                    string path = Server.MapPath(tableModel.Filepath);
                    bool exi = System.IO.File.Exists(path);
                    if(exi)
                        System.IO.File.Delete(path);
                }
            }

            if (exam.Delete(id))
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
        }

        [Login(Feedback = 100)]
        public ActionResult Start(Model.ExamTable model)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(model.Teacher) && !String.IsNullOrEmpty(model.Major))
            {
                model.Nowstate = "已开启";
                result = exam.Update(model);
                if (result)
                {
                    result = true;
                }
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "开启失败" });
        }
    }
}