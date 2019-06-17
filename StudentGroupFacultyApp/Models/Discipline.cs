using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentGroupFacultyApp.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }

        public string DisciplineName { get; set; }

        public virtual List<FacultyDiscipline> FacultyDisciplines { get; set; }
    }
}