﻿using System.ComponentModel.DataAnnotations;

namespace ExamManagement.Models
{
    public class CorrectAnswer
    {
       
        public int CorrectAnswerID { get; set; }
        public int QuestionID { get; set; }
        public int OptionID { get; set; }
    }
}
