using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineExam.Models;

namespace OnlineExam.Areas.Admin.Controllers
{
    public class teachersController : Controller
    {
        private TeacherModel db = new TeacherModel();

        // GET: teachers
        public ActionResult Index()
        {
            return View(db.teacher.ToList());
        }

        // GET: teachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacher = db.teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //性别下拉选择
        private List<SelectListItem> GetGenderList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "男",
                    Value = "男"
                },new SelectListItem
                {
                    Text = "女",
                    Value = "女"
                }
            };
        }

        // GET: teachers/Create
        public ActionResult Create()
        {
            ViewBag.GenderList = GetGenderList();
            return View();
        }

        // POST: teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tid,tname,tsex,tage,tdepartment,tpassword")] teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderList = GetGenderList();
            return View(teacher);
        }

        // GET: teachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacher = db.teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            ViewBag.GenderList = GetGenderList();
            return View(teacher);
        }

        // POST: teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tid,tname,tsex,tage,tdepartment,tpassword")] teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderList = GetGenderList();
            return View(teacher);
        }

        // GET: teachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacher = db.teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            teacher teacher = db.teacher.Find(id);
            db.teacher.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
