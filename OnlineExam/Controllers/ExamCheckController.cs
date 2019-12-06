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
            DataTable dt = DbHelperSQL.Query("SELECT ( case when a.name = 'StudentTable' then N'学生总数' " +
           " when a.name = 'AnswerTable' then N'提交总数' " +
             " else a.name end) as name, " +
     "  b.rows FROM sysobjects AS a INNER JOIN sysindexes AS b ON a.id = b.id WHERE(a.type = 'u') AND(b.indid IN(0, 1)) ORDER BY a.name, b.rows DESC").Tables[0];
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
    }
}