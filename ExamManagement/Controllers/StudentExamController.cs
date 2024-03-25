using ExamManagement.Data;
using ExamManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExamManagement.Controllers
{
    public class StudentExamController : Controller
    {
        private readonly ExamManagementContext _context;

        public StudentExamController(ExamManagementContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.Exam != null ?
                        View(await _context.Exam.ToListAsync()) :
                        Problem("Entity set 'ExamManagementContext.Exam'  is null.");
        }
		public async Task<IActionResult> ExamTimesIndex()
		{
			return _context.Exam != null ?
						View(await _context.Exam.ToListAsync()) :
						Problem("Entity set 'ExamManagementContext.Exam'  is null.");
		}
		public async Task<IActionResult> DisplayExamQuestions(int? id)
        {
			var questionModels = _context.ExamQuestion
			.Where(eq => eq.ExamID == id)
			.Select(eq => new QuestionModel
			{
				QuestionId = eq.QuestionID,
				QuestionText = _context.Question
					.Where(q => q.QuestionID == eq.QuestionID)
					.Select(q => q.QuestionText)
					.FirstOrDefault(),
				Options = _context.Option
					.Where(o => o.QuestionID == eq.QuestionID)
					.ToList()
			})
			.ToList();
			var examQuestionViewModel= new ExamQuestionViewModel
			{
				questionModels = questionModels
			};

			// Pass the exam questions to the view
			return View("Test", examQuestionViewModel);
		}
        [HttpPost]
        public IActionResult SaveExamAnswers(List<int> examQuestionIds, List<int> selectedOptions)
        {
            var Examtimes = 0;
            var studentexams = _context.StudentExamQuestion;
            foreach (var studentexam in studentexams)
            {
                if (studentexam.ExamTimes > Examtimes)
                    Examtimes = studentexam.ExamTimes;
			}
            // Process and save the selected options along with their corresponding ExamQuestionIDs
            for (int i = 0; i < examQuestionIds.Count; i++)
            {
                var studentExamQuestion = new StudentExamQuestion
                {
                    StudentID = 215052268, // Replace with the actual student ID
                    ExamQuestionID = examQuestionIds[i],
                    OptionID = selectedOptions[i],
                    ExamTimes = Examtimes + 1
                };

                // Save the student exam question to the database
                _context.StudentExamQuestion.Add(studentExamQuestion);
            }
            _context.SaveChanges();

            // Redirect to a success page or display a message
            return RedirectToAction(nameof(Index));
        }
		public async Task<IActionResult> ExamTimes(int id)
        {
			// get Examtimes
			var Examtimes = 0;
			var studentexams = _context.StudentExamQuestion;
			foreach (var studentexam in studentexams)
			{
				if (studentexam.ExamTimes > Examtimes)
					Examtimes = studentexam.ExamTimes;
			}
			

			var list = new List<StudentExamTines>();
			for (int i = 0; i < Examtimes; i++)
            {
                var studentexamtime = studentexams
                    .Where(se => se.ExamTimes == i)
                    .ToList();
                var studentexamtimes = new StudentExamTines
                {
                    StudentID = 215052268, // Replace with the actual student ID
                    ExamQuestionID = studentexams
                    .Where(se => se.ExamTimes == i)
                    .Select(se => se.ExamQuestionID)
                    .ToList(),
                    ExamTimes = i,
                    ExamID=id
				};
                var questionModels = _context.ExamQuestion
             .Where(eq => eq.ExamID == id)
             .Select(eq => eq.Id).ToList();
                

				if (studentexamtimes.ExamQuestionID.Count >0 && CheckList(questionModels, studentexamtimes.ExamQuestionID))
                list.Add(studentexamtimes);
			}
            
			return View(list);
		}
		bool CheckList(List<int> danhSach1, List<int> danhSach2)
		{
			// Kiểm tra độ dài của hai danh sách
			if (danhSach1.Count != danhSach2.Count)
			{
				return false; // Nếu hai danh sách có độ dài khác nhau, chúng không thể cùng giá trị nhưng khác vị trí
			}

			// Sắp xếp các số trong hai danh sách
			danhSach1.Sort();
			danhSach2.Sort();

			// So sánh các số theo vị trí tương ứng
			for (int i = 0; i < danhSach1.Count; i++)
			{
				if (danhSach1[i] != danhSach2[i])
				{
					return false; // Nếu có ít nhất một cặp số không khớp, trả về false
				}
			}

			return true; // Nếu các số đều khớp, trả về true
		}
		public async Task<IActionResult> Review(int? id)
        {

            var questionModels = _context.ExamQuestion
            .Where(eq => eq.ExamID == id)
            .Select(eq => new QuestionViewModel2
            {
                QuestionId = eq.QuestionID,
                QuestionText = _context.Question
                    .Where(q => q.QuestionID == eq.QuestionID)
                    .Select(q => q.QuestionText)
                    .FirstOrDefault(),
                Options = _context.Option
                    .Where(o => o.QuestionID == eq.QuestionID)
                    .ToList(),
                CorrectAnswerId = _context.CorrectAnswer
                    .Where(c => c.QuestionID == eq.QuestionID)
                    .Select(c => c.QuestionID)
                    .FirstOrDefault(),
                SelectoptionId = _context.StudentExamQuestion
                    .Where(s => s.ExamQuestionID == eq.Id)
                    .Select(s=> s.OptionID)
                    .FirstOrDefault()

            })
            .ToList();
          

            return View(questionModels);
        }
    }
}
