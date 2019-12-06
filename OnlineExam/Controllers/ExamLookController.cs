using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    public class ExamLookController : BaseController
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

            var strSql = "Username = '" + Session["username"] + "'";
            BLL.StudentTable bllStudent = new BLL.StudentTable();
            List<Model.StudentTable> studentList = bllStudent.DataTableToList(bllStudent.GetList(strSql).Tables[0]);

            if (studentList.Count > 0)
            {
                Model.StudentTable studentModel = studentList[0];

                strSql = "Major = N'" + studentList[0].Major + "' and Nowstate = N'已结束'";
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
    }
}