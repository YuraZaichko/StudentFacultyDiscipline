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
    [Authorize(Roles = "Admin, Faculty")]
    public class GroupsController : BaseController
    {
       
        // GET: Groups
        public ActionResult Index()
        {
            var groups = db.Groups.Where(x => x.FacultyId == (CurrentUser.FacultyId??0));

            return View(groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (CurrentUser.FacultyId != group.FacultyId)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,GroupName")] Group group)
        {
            
            if (CurrentUser.FacultyId != null)
            {
                group.FacultyId = CurrentUser.FacultyId.Value;
            }
            else
            {
                return HttpNotFound();
            }
            
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (CurrentUser.FacultyId != group.FacultyId)
            {
                return HttpNotFound();
            }
            
            return View(group);
        }

        // POST: Groups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,GroupName,FacultyId")] Group group)
        {
            var dbGroup = db.Groups.FirstOrDefault(x => x.GroupId == group.GroupId);
            if (CurrentUser.FacultyId != dbGroup.FacultyId)
            {
                return HttpNotFound();
            }
            
            if (ModelState.IsValid)
            {
                dbGroup.GroupName = group.GroupName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (CurrentUser.FacultyId != group.FacultyId)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            if (CurrentUser.FacultyId != group.FacultyId)
            {
                return HttpNotFound();
            }
            db.Groups.Remove(group);
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
