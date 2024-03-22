﻿using System.ComponentModel.DataAnnotations;

namespace ExamManagement.Models
{
    public class ExamQuestion
    {
        
        public int Id { get; set; }
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
    }
	public class CreateExamQuestionsViewModel
	{
		[Required]
		public string CourseName { get; set; }

		[Required(ErrorMessage = "Please enter the number of questions.")]
		[Range(1, int.MaxValue, ErrorMessage = "Number of questions must be greater than 0.")]
		public int NumberOfQuestions { get; set; }
		public int ExamID { get; internal set; }
	}
}
