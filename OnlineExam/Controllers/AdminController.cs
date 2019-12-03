﻿using OnlineExam.Codes;
using OnlineExam.Common;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineExam.Controllers
{
    [Login]
    public class AdminController : BaseController
    {
        OnlineExam.BLL.AdminTable admin = new OnlineExam.BLL.AdminTable();

        // GET: Admin
        public override ActionResult Index()
        {
            return View();
        }
       
        public override ActionResult AjaxList()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数

            string sqlString = "";
            string search = Request.Params["search"];

            sqlString += String.IsNullOrEmpty(search) ? "1 = 1" : ("Username Like ('%" + search + "%')");

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);            

            List<OnlineExam.Model.AdminTable> list = admin.DataTableToList(admin.GetListByPage(sqlString, "", start + 1, start + length).Tables[0]);
            int total = admin.GetRecordCount("");
            int totalFilter = admin.GetRecordCount(sqlString);

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
        public ActionResult Create(Model.AdminTable model)
        {
            bool result = false;
            //model.ID = 1;

            if (!String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Name))
            {
                var state = admin.Add(model);
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
            Model.AdminTable model = admin.GetModel(id);
            if (model != null)
                result = true;

            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.AdminTable model)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Name))
            {
                result = admin.Update(model);                
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
            if (admin.Delete(id))
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
        }
    }
}