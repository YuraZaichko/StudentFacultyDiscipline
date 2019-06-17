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
    [Authorize(Roles ="Admin, Faculty")]
    public class FacultyDisciplinesController : BaseController
    {

        // GET: FacultyDisciplines
        public ActionResult Index()
        {
            var facultyDisciplines = db.FacultyDisciplines.Where(x => x.FacultyId == (CurrentUser.FacultyId ?? 0));
            
            return View(facultyDisciplines.ToList());
        }

        // GET: FacultyDisciplines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDiscipline facultyDiscipline = db.FacultyDisciplines.Find(id);
            if (facultyDiscipline == null)
            {
                return HttpNotFound();
            }
            if (CurrentUser.FacultyId != facultyDiscipline.FacultyId)
            {
                return HttpNotFound();
            }
                return View(facultyDiscipline);
        }

        // GET: FacultyDisciplines/Create
        public ActionResult Create()
        {
            var selectedDisciplines = db.FacultyDisciplines.Where(x => x.FacultyId == CurrentUser.FacultyId).Select(x => x.DisciplineId)
                .Distinct().ToArray();

            var notSelectedDisciplines = db.Disciplines.Where(x => !selectedDisciplines.Contains(x.DisciplineId)).ToList();

            ViewBag.DisciplineId = new SelectList(notSelectedDisciplines, "DisciplineId", "DisciplineName");
            
            return View();
        }

        // POST: FacultyDisciplines/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyDisciplineId,FacultyId,DisciplineId")] FacultyDiscipline facultyDiscipline)
        {


            var isDisciplineExists = db.FacultyDisciplines.Any(x => x.DisciplineId == facultyDiscipline.DisciplineId && CurrentUser.FacultyId == x.FacultyId);

            if (isDisciplineExists)
            {
                return HttpNotFound();
            }


            if (CurrentUser.FacultyId != null)
            {
                facultyDiscipline.FacultyId = CurrentUser.FacultyId.Value;
            }
            else
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {                
                db.FacultyDisciplines.Add(facultyDiscipline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var selectedDisciplines = db.FacultyDisciplines.Where(x => x.FacultyId == CurrentUser.FacultyId).Select(x => x.DisciplineId)
                .Distinct().ToArray();

            var notSelectedDisciplines = db.Disciplines.Where(x => !selectedDisciplines.Contains(x.DisciplineId)).ToList();


            ViewBag.DisciplineId = new SelectList(notSelectedDisciplines, "DisciplineId", "DisciplineName", facultyDiscipline.DisciplineId);
            return View(facultyDiscipline);
        }

        // GET: FacultyDisciplines/Edit/5
        public ActionResult Edit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDiscipline facultyDiscipline = db.FacultyDisciplines.Find(id);
            if (facultyDiscipline == null)
            {
                return HttpNotFound();
            }
            if (CurrentUser.FacultyId != facultyDiscipline.FacultyId)
            {
                return HttpNotFound();
            }
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "DisciplineName", facultyDiscipline.DisciplineId);
            return View(facultyDiscipline);
        }

        // POST: FacultyDisciplines/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyDisciplineId,DisciplineId")] FacultyDiscipline facultyDiscipline)
        {
            
            var dbFacultyDiscipline = db.FacultyDisciplines.FirstOrDefault(x => x.FacultyDisciplineId == facultyDiscipline.FacultyDisciplineId);
            if(dbFacultyDiscipline==null)
            {
                return HttpNotFound();
            }
            if (CurrentUser.FacultyId != dbFacultyDiscipline.FacultyId)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                dbFacultyDiscipline.DisciplineId = facultyDiscipline.DisciplineId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "DisciplineName", facultyDiscipline.DisciplineId);
            return View(facultyDiscipline);
        }

        // GET: FacultyDisciplines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDiscipline facultyDiscipline = db.FacultyDisciplines.Find(id);
            if (facultyDiscipline == null)
            {
                return HttpNotFound();
            }
            if (CurrentUser.FacultyId != facultyDiscipline.FacultyId)
            {
                return HttpNotFound();
            }
            return View(facultyDiscipline);
        }

        // POST: FacultyDisciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacultyDiscipline facultyDiscipline = db.FacultyDisciplines.Find(id);
            if(CurrentUser.FacultyId!= facultyDiscipline.FacultyId || CurrentUser.FacultyId == null)
            {
                return HttpNotFound();
            } 
            db.FacultyDisciplines.Remove(facultyDiscipline);
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
