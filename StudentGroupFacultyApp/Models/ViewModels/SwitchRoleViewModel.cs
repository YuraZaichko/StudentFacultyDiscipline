using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentGroupFacultyApp.Models.ViewModels
{
    public class SwitchRoleViewModel
    {
        public List<Faculty> AllFaculty { get; set; }
        public List<Student> Students { get; set; }
    }
}