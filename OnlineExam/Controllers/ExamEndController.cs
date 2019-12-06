using OnlineExam.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    [Login]
    public class ExamEndController : BaseController
    {
        BLL.ExamTable exam = new BLL.ExamTable();

        public override ActionResult Index()
        {
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
                strSql = "Nowstate = N'已结束'";
            }
            else
            {
                strSql = "Nowstate = N'已结束' and Teacher = '" + Session["username"] + "'";
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

        public ActionResult Clear(int id)
        {
            if (exam.Delete(id))
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
        }
    }
}