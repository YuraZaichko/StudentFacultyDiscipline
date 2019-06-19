﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentGroupFacultyApp.Models.ViewModels
{
    public class StudentFacultyDisciplineViewModel
    {
        public int[] StudentDisciplines { get; set; }

        public List<FacultyDiscipline> AllDisciplines { get; set; }
        
    }
}