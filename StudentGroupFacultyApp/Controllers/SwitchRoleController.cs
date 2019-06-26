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
            if (selectedFaculty != null)
            {                
                var applicationUserFaculty = db.Users.FirstOrDefault(x => x.Id==CurrentUser.Id);
                if (applicationUserFaculty == null)
                {
                    return new HttpStatusCodeResult(404, "Ошибка с выбором факультета!!!");
                }

                applicationUserFaculty.FacultyId = selectedFaculty;
                applicationUserFaculty.StudentId= null;
                db.SaveChanges();
               
            }

            if (selectedStudent!=null)
            {
                var applicationUserStudent = db.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);
                if (applicationUserStudent==null)
                {
                    return new HttpStatusCodeResult(404, "Ошибка с выбором студента!!!"); 
                }

                applicationUserStudent.StudentId = selectedStudent;
                applicationUserStudent.FacultyId = null;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}