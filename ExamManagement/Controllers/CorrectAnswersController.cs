using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamManagement.Data;
using ExamManagement.Models;

namespace ExamManagement.Controllers
{
    public class CorrectAnswersController : Controller
    {
        private readonly ExamManagementContext _context;

        public CorrectAnswersController(ExamManagementContext context)
        {
            _context = context;
        }

        // GET: CorrectAnswers
        public async Task<IActionResult> Index()
        {
              return _context.CorrectAnswer != null ? 
                          View(await _context.CorrectAnswer.ToListAsync()) :
                          Problem("Entity set 'ExamManagementContext.CorrectAnswer'  is null.");
        }

        // GET: CorrectAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CorrectAnswer == null)
            {
                return NotFound();
            }

            var correctAnswer = await _context.CorrectAnswer
                .FirstOrDefaultAsync(m => m.CorrectAnswerID == id);
            if (correctAnswer == null)
            {
                return NotFound();
            }

            return View(correctAnswer);
        }

        // GET: CorrectAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CorrectAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorrectAnswerID,QuestionID,OptionID")] CorrectAnswer correctAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correctAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(correctAnswer);
        }

        // GET: CorrectAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CorrectAnswer == null)
            {
                return NotFound();
            }

            var correctAnswer = await _context.CorrectAnswer.FindAsync(id);
            if (correctAnswer == null)
            {
                return NotFound();
            }
            return View(correctAnswer);
        }

        // POST: CorrectAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CorrectAnswerID,QuestionID,OptionID")] CorrectAnswer correctAnswer)
        {
            if (id != correctAnswer.CorrectAnswerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correctAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorrectAnswerExists(correctAnswer.CorrectAnswerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(correctAnswer);
        }

        // GET: CorrectAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CorrectAnswer == null)
            {
                return NotFound();
            }

            var correctAnswer = await _context.CorrectAnswer
                .FirstOrDefaultAsync(m => m.CorrectAnswerID == id);
            if (correctAnswer == null)
            {
                return NotFound();
            }

            return View(correctAnswer);
        }

        // POST: CorrectAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CorrectAnswer == null)
            {
                return Problem("Entity set 'ExamManagementContext.CorrectAnswer'  is null.");
            }
            var correctAnswer = await _context.CorrectAnswer.FindAsync(id);
            if (correctAnswer != null)
            {
                _context.CorrectAnswer.Remove(correctAnswer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrectAnswerExists(int id)
        {
          return (_context.CorrectAnswer?.Any(e => e.CorrectAnswerID == id)).GetValueOrDefault();
        }
    }
}
