using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StudentGroupFacultyApp.Models;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace StudentGroupFacultyApp.Models
{
    public class Faculty
    {
        [Display(Name = "Выберите факультета")]
        public int FacultyId { get; set; }

        [Display(Name = "Название факультета") ]
        public string FacultyName { get; set; }
                
        public virtual List<Group> Groups { get; set; }

        public virtual List<FacultyDiscipline> FacultyDisciplines { get; set; }

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }
    }
}