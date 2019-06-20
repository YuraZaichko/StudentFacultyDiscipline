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

        //[HttpPost]
        //public ActionResult Action(int? selectedFaculty, int? selectedStudent)
        //{
        //    if (selectedFaculty==null)
        //    {

        //    }

        //    return View();
        //}
    }
}