namespace ExamManagement.Models
{
    public class StudentExamQuestion
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public int ExamQuestionID { get; set; }
        public int OptionID { get; set; }
        public int ExamTimes { get; set; }
	}
    public class ReviewData
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Option> Options { get; set; }
        public int StudentAnswerId { get; set; }
        public int CorrectAnswerId { get; set; }
    }

    public class StudentExamTines
    {
		public List<int> ExamQuestionID { get; set; }
		public int StudentID { get; set; }
        public int ExamTimes { get; set; }
        public int ExamID { get; set; }
	}


}
