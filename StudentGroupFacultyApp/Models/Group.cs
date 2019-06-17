using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StudentGroupFacultyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentGroupFacultyApp.Models
{
    public class Group
    {
        [Display(Name = "Выберите группу")]
        public int GroupId { get; set; }

        [Display(Name = "Название группы")]
        public string GroupName { get; set; }

        public virtual List<Student> Students { get; set; }

        [Display(Name = "Выберите факультет")]
        public int FacultyId { get; set; }

        [Display(Name = "Выберите факультет")]
        public virtual Faculty Faculty { get; set; }
    }
}