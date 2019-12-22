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

        [HttpPost]
        public void DownloadFile(int id)
        {
            string strSql = "select * from AnswerTable where Exam = '" + id + "'";
            List<Model.AnswerTable> tableList = new BLL.AnswerTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

            if (tableList.Count != 0)
            {
                string path = "";
                foreach (Model.AnswerTable tableModel in tableList)
                {
                    path += tableModel.Filepath + "|";
                }

                ZipFileDownload(path.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries), DateTime.Now.ToString("yyyyMMddhhmmss") + "_Answer.zip");
            }
        }

        public ActionResult Clear(int id)
        {
            bool result = false;

            BLL.ExamTable exam = new BLL.ExamTable();
            string strSql = "select * from ExamTable where Id = '" + id + "'";
            List<Model.ExamTable> tableList = new BLL.ExamTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

            if (tableList.Count != 0)
            {
                foreach (Model.ExamTable tableModel in tableList)
                {
                    string path = Server.MapPath(tableModel.Filepath);
                    bool exi = System.IO.File.Exists(path);
                    if (exi)
                        System.IO.File.Delete(path);
                }
            }

            exam.Delete(id);

            BLL.AnswerTable answer = new BLL.AnswerTable();
            string strSql1 = "select * from AnswerTable where Exam = '" + id + "'";
            List<Model.AnswerTable> tableList1 = new BLL.AnswerTable().DataTableToList(DbHelperSQL.Query(strSql1).Tables[0]);

            if (tableList1.Count != 0)
            {
                foreach (Model.AnswerTable tableModel in tableList1)
                {
                    string path = Server.MapPath(tableModel.Filepath);
                    bool exi = System.IO.File.Exists(path);
                    if (exi)
                        System.IO.File.Delete(path);

                    answer.Delete(tableModel.ID);
                }
            }

            BLL.IPTable IP = new BLL.IPTable();
            string strSql2 = "select * from IPTable";
            List<Model.IPTable> tableList2 = new BLL.IPTable().DataTableToList(DbHelperSQL.Query(strSql2).Tables[0]);

            if (tableList2.Count != 0)
            {
                foreach (Model.IPTable tableModel in tableList2)
                {
                    IP.Delete(tableModel.ID);
                }
            }

            result = true;

            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
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