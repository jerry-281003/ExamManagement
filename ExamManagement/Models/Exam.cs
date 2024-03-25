﻿
using System.Collections.Generic;
using System.IO;

namespace ExamManagement.Models
{
    public class Exam
    {
        
        public int ExamID { get; set; }
        public string CourseName { get; set; }
        public DateTime Date { get; set; }
        public string ExamName { get; set; }
        public int Duration { get; set; }
    }

    
}
