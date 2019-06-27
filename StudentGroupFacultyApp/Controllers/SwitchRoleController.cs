using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudentGroupFacultyApp.Models;
using StudentGroupFacultyApp.Models.ViewModels;

namespace StudentGroupFacultyApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SwitchRoleController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var allFaculty = db.Faculties.ToList();
            var allStudent = db.Students.ToList();

            var viewModel = new SwitchRoleViewModel()
            {
                AllFaculty = allFaculty,
                Students = allStudent
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(int? selectedFaculty, int? selectedStudent)
        {
            if (selectedFaculty==null && selectedStudent == null)
            {
                return new HttpStatusCodeResult(404, "page not found!");
            }

            var applicationUser = db.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);
            applicationUser.FacultyId = null;
            applicationUser.StudentId = null;
            db.SaveChanges();

            //заходим под студентом
            if (selectedFaculty == null && selectedStudent != null || selectedFaculty != null && selectedStudent!=null)
            {
                var applicationUserStudent = db.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);
                applicationUser.StudentId = selectedStudent;
                db.SaveChanges();
            }

            //заходим под факультетом
            if (selectedFaculty!=null && selectedStudent==null)
            {
                var applicationUserFaculty = db.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);
                applicationUserFaculty.FacultyId = selectedFaculty;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetStudentsByFaculty(int selectFaculty)
        {
            var students = db.Students.Where(x => x.Group.FacultyId == selectFaculty).Select(x => new
            {
                StudentId = x.StudentId,
                FullName=x.StudentLastName+" "+x.StudentFirstName+" "+x.StudentMiddleName
            }).ToList();

            return Json(students,JsonRequestBehavior.AllowGet);
        }
    }
}