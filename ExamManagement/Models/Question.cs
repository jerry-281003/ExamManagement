using System.ComponentModel.DataAnnotations.Schema;

namespace ExamManagement.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int CourseID { get; set; }
    }
	public class QuestionViewModel
	{
		public int QuestionId { get; set; }
		public string QuestionText { get; set; }
		public List<Option> Options { get; set; }
		public int CorrectAnswerId { get; set; }
	}
}
