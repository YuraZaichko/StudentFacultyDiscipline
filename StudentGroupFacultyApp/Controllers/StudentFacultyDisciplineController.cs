using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentGroupFacultyApp.Models;
using StudentGroupFacultyApp.Models.ViewModels;

namespace StudentGroupFacultyApp.Controllers
{
    [Authorize(Roles ="Admin, Student")]
    public class StudentFacultyDisciplineController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var facultyDisciplineForStudent = db.Students.FirstOrDefault
                (x => x.StudentId == CurrentUser.StudentId)
                .Group
                .Faculty
                .FacultyDisciplines;

            var studentDisciplines = db.Students.FirstOrDefault
                (x => x.StudentId == CurrentUser.StudentId)
                .FacultyDisciplines.Select(x=>x.FacultyDisciplineId).ToArray();

            var viewModel = new StudentFacultyDisciplineViewModel()
            {
                AllDisciplines = facultyDisciplineForStudent,
                StudentDisciplines = studentDisciplines
            };
           
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(int[] selectedFacultyDisciplinesIds)
        {
            if (selectedFacultyDisciplinesIds == null)
            {
                selectedFacultyDisciplinesIds = new int[0];
            }
            var facultyDisciplines = db.FacultyDisciplines.Where(x => selectedFacultyDisciplinesIds.Contains(x.FacultyDisciplineId)).ToList();
            var student = db.Students.FirstOrDefault(x => x.StudentId == CurrentUser.StudentId);
            if (student == null)
            {
                return HttpNotFound();
            }
            student.FacultyDisciplines.Clear();
            student.FacultyDisciplines.AddRange(facultyDisciplines);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}