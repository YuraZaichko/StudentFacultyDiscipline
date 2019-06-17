using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StudentGroupFacultyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentGroupFacultyApp.Models
{
    public class Student
    {

        public int StudentId { get; set; }

        [Display(Name = "Фамилия")]
        public string StudentLastName { get; set; }

        [Display(Name = "Имя")]
        public string StudentFirstName { get; set; }

        [Display(Name = "Отчество")]
        public string StudentMiddleName { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Выберите группу")]
        public int GroupId { get; set; }

        [Display(Name = "Выберите группу")]
        public virtual Group Group { get; set; }

        public virtual List<FacultyDiscipline> FacultyDisciplines { get; set; }

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }
    }
}