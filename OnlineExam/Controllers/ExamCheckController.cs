using Maticsoft.DBUtility;
using OnlineExam.Codes;
using OnlineExam.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    [Login]
    public class ExamCheckController : BaseController
    {
        BLL.ExamTable exam = new BLL.ExamTable();

        public override ActionResult Index()
        {
            DataTable dt = DbHelperSQL.Query("SELECT ( case when id = OBJECT_ID('IPTable') then N'学生登录总数'" +
                "when id = OBJECT_ID('AnswerTable') then N'提交总数'" +
                "end) as name,rows FROM sysindexes " +
                "WHERE id = OBJECT_ID('IPTable') AND indid < 2" +
                "OR id = OBJECT_ID('AnswerTable') AND indid < 2").Tables[0];
            string bigData = ModelConvertHelper.ConvertToModel(dt);
            ViewBag.BigData = bigData;

            return View();
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
                strSql = "Nowstate = N'已开启'";
            }
            else
            {
                strSql = "Nowstate = N'已开启' and Teacher = '" + Session["username"] + "'";
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
        public ActionResult End(Model.ExamTable model)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(model.Teacher) && !String.IsNullOrEmpty(model.Major))
            {
                model.Nowstate = "已结束";
                result = exam.Update(model);
                if (result)
                {
                    result = true;
                }
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "操作失败" });
        }

        public ActionResult IPAjaxList()
        {
            int start = 0;
            int length = 0;

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            var strSql = "";

            BLL.IPTable ip = new BLL.IPTable();
            List<Model.IPTable> list = ip.DataTableToList(ip.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int filterTotal = ip.GetRecordCount(strSql);
            int total = ip.GetRecordCount("");

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
                        Username = o.Username,
                        IP = o.IP,
                    })
                }
            });
        }

        public ActionResult AnAjaxList()
        {
            int start = 0;
            int length = 0;

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            var strSql = "";

            BLL.AnswerTable answer = new BLL.AnswerTable();
            List<Model.AnswerTable> list = answer.DataTableToList(answer.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int filterTotal = answer.GetRecordCount(strSql);
            int total = answer.GetRecordCount("");

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
                        Student = o.Student,
                        Filepath = o.Filepath,
                    })
                }
            });
        }

        public ActionResult Remove(int id)
        {
            BLL.IPTable ip = new BLL.IPTable();
            if (ip.Delete(id))
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
        }
    }
}