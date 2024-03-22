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
    public class OptionsController : Controller
    {
        private readonly ExamManagementContext _context;

        public OptionsController(ExamManagementContext context)
        {
            _context = context;
        }

        // GET: Options
        public async Task<IActionResult> Index()
        {
              return _context.Option != null ? 
                          View(await _context.Option.ToListAsync()) :
                          Problem("Entity set 'ExamManagementContext.Option'  is null.");
        }

        // GET: Options/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Option == null)
            {
                return NotFound();
            }

            var option = await _context.Option
                .FirstOrDefaultAsync(m => m.OptionID == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // GET: Options/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OptionID,QuestionID,OptionText")] Option option)
        {
            if (ModelState.IsValid)
            {
                _context.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(option);
        }

        // GET: Options/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Option == null)
            {
                return NotFound();
            }

            var option = await _context.Option.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }
            return View(option);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OptionID,QuestionID,OptionText")] Option option)
        {
            if (id != option.OptionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(option);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionExists(option.OptionID))
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
            return View(option);
        }

        // GET: Options/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Option == null)
            {
                return NotFound();
            }

            var option = await _context.Option
                .FirstOrDefaultAsync(m => m.OptionID == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Option == null)
            {
                return Problem("Entity set 'ExamManagementContext.Option'  is null.");
            }
            var option = await _context.Option.FindAsync(id);
            if (option != null)
            {
                _context.Option.Remove(option);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionExists(int id)
        {
          return (_context.Option?.Any(e => e.OptionID == id)).GetValueOrDefault();
        }
    }
}
