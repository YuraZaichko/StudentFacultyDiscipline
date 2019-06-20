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
        public ActionResult Index(int? selectedFaculty/*, int? selectedStudent*/)
        {
            if (selectedFaculty != null/* && selectedStudent==null*/)
            {
                //selectedStudent = 0;
                
                var applicationUser = db.Users.FirstOrDefault(x => x.Id==CurrentUser.Id);
                if (applicationUser == null)
                {
                    return new HttpStatusCodeResult(404, "Ошибка!!!");
                }

                applicationUser.FacultyId = selectedFaculty;
                db.SaveChanges();
               
            }



            //if(selectedFaculty==null && selectedStudent != null)
            //{
            //    selectedFaculty = 0;
            //}

            return RedirectToAction("Index", "Home");
        }
    }
}