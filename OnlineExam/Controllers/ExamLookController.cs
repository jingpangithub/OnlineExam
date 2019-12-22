using ICSharpCode.SharpZipLib.Zip;
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

        [HttpPost]
        public void DownloadFile(int id)
        {
            string strSql = "select * from AnswerTable where Exam = '" + id + "' and Student = '" + Session["username"] + "'";
            List<Model.AnswerTable> tableList = new BLL.AnswerTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

            if (tableList.Count != 0)
            {
                string path = "";
                foreach (Model.AnswerTable tableModel in tableList)
                {
                    path += tableModel.Filepath + "|";
                }

                ZipFileDownload(path.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries), DateTime.Now.ToString("yyyyMMddhhMmss") + "_Answer.zip");
            }
        }

        private void ZipFileDownload(string[] files, string zipFileName)
        {
            MemoryStream ms = new MemoryStream();
            byte[] buffer = null;

            using (ZipFile file = ZipFile.Create(ms))
            {
                file.BeginUpdate();

                file.NameTransform = new MyNameTransform();
                foreach (var item in files)
                {
                    file.Add(Server.MapPath(item));
                }
                file.CommitUpdate();
                buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);   //读取文件内容(1次读ms.Length/1024M)
                ms.Flush();
                ms.Close();
            }
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/x-zip-compressed";
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(zipFileName));
            Response.BinaryWrite(buffer);
            Response.Flush();
            Response.End();
        }
    }
}