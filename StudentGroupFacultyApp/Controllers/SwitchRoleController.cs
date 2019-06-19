using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentGroupFacultyApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SwitchRoleController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
    }
}