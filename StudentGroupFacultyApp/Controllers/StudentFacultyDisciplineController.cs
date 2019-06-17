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
        public ActionResult Index()
        {            
            var facultyDisciplineForStudent = db.Students.FirstOrDefault(
                x => x.StudentId == CurrentUser.StudentId)
                .Group
                .Faculty
                .FacultyDisciplines.ToList();

            var studentDisciplines = db.Students.FirstOrDefault(
                x => x.StudentId == CurrentUser.StudentId)
                .FacultyDisciplines.ToList();

            var viewModel = new StudentFacultyDisciplineViewModel()
            {
                AllDisciplines = facultyDisciplineForStudent,
                StudentDisciplines = studentDisciplines
            };

            return View(viewModel);
        }        
    }
}