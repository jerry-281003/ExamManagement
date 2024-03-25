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
    public class ExamQuestionsController : Controller
    {
        private readonly ExamManagementContext _context;

        public ExamQuestionsController(ExamManagementContext context)
        {
            _context = context;
        }

        // GET: ExamQuestions
        public async Task<IActionResult> Index()
        {
              return _context.ExamQuestion != null ? 
                          View(await _context.ExamQuestion.ToListAsync()) :
                          Problem("Entity set 'ExamManagementContext.ExamQuestion'  is null.");
        }

        // GET: ExamQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExamQuestion == null)
            {
                return NotFound();
            }

            var examQuestion = await _context.ExamQuestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examQuestion == null)
            {
                return NotFound();
            }

            return View(examQuestion);
        }

        // GET: ExamQuestions/Create
        public IActionResult Create()
        {
            ViewData["ExamID"] = new SelectList(_context.Exam, "ExamID", "ExamID");
            ViewData["QuestionID"] = new SelectList(_context.Question, "QuestionText", "QuestionText");
            return View();
        }

        // POST: ExamQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExamID,QuestionID")] ExamQuestion examQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamID"] = new SelectList(_context.Exam, "ExamID", "ExamID", examQuestion.ExamID);
            ViewData["QuestionID"] = new SelectList(_context.Question, "QuestionID", "QuestionID", examQuestion.QuestionID);
            return View(examQuestion);
        }

        // GET: ExamQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExamQuestion == null)
            {
                return NotFound();
            }

            var examQuestion = await _context.ExamQuestion.FindAsync(id);
            if (examQuestion == null)
            {
                return NotFound();
            }
            return View(examQuestion);
        }
        
        
        // POST: ExamQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExamID,QuestionID")] ExamQuestion examQuestion)
        {
            if (id != examQuestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamQuestionExists(examQuestion.Id))
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
            return View(examQuestion);
        }

        // GET: ExamQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExamQuestion == null)
            {
                return NotFound();
            }

            var examQuestion = await _context.ExamQuestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examQuestion == null)
            {
                return NotFound();
            }

            return View(examQuestion);
        }

        // POST: ExamQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExamQuestion == null)
            {
                return Problem("Entity set 'ExamManagementContext.ExamQuestion'  is null.");
            }
            var examQuestion = await _context.ExamQuestion.FindAsync(id);
            if (examQuestion != null)
            {
                _context.ExamQuestion.Remove(examQuestion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamQuestionExists(int id)
        {
          return (_context.ExamQuestion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
