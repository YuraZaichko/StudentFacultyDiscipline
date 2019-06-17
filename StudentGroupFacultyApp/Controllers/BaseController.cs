using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StudentGroupFacultyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentGroupFacultyApp.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationUser CurrentUser { get; set; }
        public string[] CurrentUserRoles { get; set; }
        protected ApplicationDbContext db = new ApplicationDbContext();
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if(User!=null && User.Identity!=null && !string.IsNullOrEmpty(User.Identity.Name))
            {
                CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (CurrentUser != null)
                {                    
                    ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                            .GetUserManager<ApplicationUserManager>();                    
                    CurrentUserRoles = userManager.GetRoles(CurrentUser.Id).ToArray();
                    ViewBag.CurrentUser = CurrentUser;
                    ViewBag.CurrentUserRoles = CurrentUserRoles;
                }
            }

        }
    }
}