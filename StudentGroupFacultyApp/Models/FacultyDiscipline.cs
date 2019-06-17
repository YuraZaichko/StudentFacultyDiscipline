using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentGroupFacultyApp.Models
{
    public class FacultyDiscipline
    {
        public int FacultyDisciplineId { get; set; }

        public int FacultyId { get; set; }

        public int DisciplineId  { get; set; }

        public virtual List<Student> Students { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual Discipline Discipline { get; set; }
    } 
}