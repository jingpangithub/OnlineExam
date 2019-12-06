using OnlineExam.Codes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    [Login]
    public class ExamTakeController : BaseController
    {
        BLL.ExamTable exam = new BLL.ExamTable();
        BLL.AnswerTable answer = new BLL.AnswerTable();

        public override ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase file = Request.Files[0];
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "AnswerFile"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return this.Json(new
            {
                url = "/AnswerFile/" +
                fileName
            });
        }

        public override ActionResult AjaxList()
        {
            int start = 0;  
            int length = 0; 

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);
            
            var strSql = "Username = '" + Session["username"] + "'";
            BLL.StudentTable bllStudent = new BLL.StudentTable();
            List<Model.StudentTable> studentList = bllStudent.DataTableToList(bllStudent.GetList(strSql).Tables[0]);

            if (studentList.Count > 0)
            {
                Model.StudentTable studentModel = studentList[0];

                strSql = "Major = N'" + studentList[0].Major + "' and Nowstate = N'已开启'";
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
        public ActionResult Submit(Model.AnswerTable model)
        {
            bool result = false;

            model.Student = Session["username"].ToString();

            var state = answer.Add(model);
            if (state != 0)
            {
                result = true;
            }

            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "新建失败" });
        }
    }
}