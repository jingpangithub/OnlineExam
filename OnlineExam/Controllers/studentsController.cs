using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineExam.Models;

namespace OnlineExam.Controllers
{
    public class studentsController : Controller
    {
        private StudentModel db = new StudentModel();

        //表单检索
        private List<SelectListItem> GetMajorList()
        {
            var majors = db.student.OrderBy(m => m.smajor).Select(m => m.smajor).Distinct();

            var items = new List<SelectListItem>();
            foreach(string major in majors)
            {
                items.Add(new SelectListItem
                {
                    Text = major,
                    Value = major
                });
            }

            return items;
        }      

        // GET: students
        public ActionResult Index(string Major, string Name)
        {
            var students = db.student as IQueryable<student>;

            if (!String.IsNullOrEmpty(Name))
            {
                students = students.Where(m => m.sname.Contains(Name));
            }
            if (!String.IsNullOrEmpty(Major))
            {
                students = students.Where(m => m.smajor == Major);
            }

            ViewBag.MajorList = GetMajorList();
            return View(students.ToList());
        }

        // GET: students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
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

        // GET: students/Create
        public ActionResult Create()
        {
            ViewBag.GenderList = GetGenderList();
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sid,sname,ssex,sage,smajor,spassword")] student student)
        {
            if (ModelState.IsValid)
            {
                db.student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderList = GetGenderList();
            return View(student);
        }

        // GET: students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.GenderList = GetGenderList();
            return View(student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sid,sname,ssex,sage,smajor,spassword")] student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderList = GetGenderList();
            return View(student);
        }

        // GET: students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            student student = db.student.Find(id);
            db.student.Remove(student);
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
