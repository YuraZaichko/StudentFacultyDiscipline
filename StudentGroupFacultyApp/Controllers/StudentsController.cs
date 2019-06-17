using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentGroupFacultyApp.Models;

namespace StudentGroupFacultyApp.Controllers
{
    [Authorize(Roles = "Admin, Faculty, Student")]
    public class StudentsController : BaseController
    {

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Where(x => x.Group.FacultyId == CurrentUser.FacultyId);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [Authorize(Roles ="Admin, Faculty")]
        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups.Where(x=>x.FacultyId==CurrentUser.FacultyId).ToList(), "GroupId", "GroupName");
            return View();
        }

        [Authorize(Roles = "Admin, Faculty")]
        // POST: Students/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,StudentLastName,StudentFirstName,StudentMiddleName,BirthDay,GroupId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups.Where(x=>x.FacultyId==CurrentUser.FacultyId).ToList(), "GroupId", "GroupName", student.GroupId);
            return View(student);
        }


        [Authorize(Roles = "Admin, Faculty")]
        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.GroupId = new SelectList(db.Groups.Where(x=>x.FacultyId==CurrentUser.FacultyId).ToList(), "GroupId", "GroupName", student.GroupId);
            return View(student);
        }

        [Authorize(Roles = "Admin, Faculty")]
        // POST: Students/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,StudentLastName,StudentFirstName,StudentMiddleName,BirthDay,GroupId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups.Where(x=>x.FacultyId==CurrentUser.FacultyId).ToList(), "GroupId", "GroupName", student.GroupId);
            return View(student);
        }

        [Authorize(Roles = "Admin, Faculty")]
        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [Authorize(Roles = "Admin, Faculty")]
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var filterStudent = db.Students.Where(x=>x.StudentId == CurrentUser.StudentId);
            if (filterStudent == null)
            {
                return HttpNotFound();
            }
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
