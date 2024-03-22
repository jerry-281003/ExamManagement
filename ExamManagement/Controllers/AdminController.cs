using ExamManagement.Data;
using ExamManagement.Migrations;
using ExamManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace ExamManagement.Controllers
{
    public class AdminController : Controller
    {
		private readonly UserManager<IdentityUser> _userManager;
        private readonly ExamManagementContext _context;


        public AdminController(UserManager<IdentityUser> userManager, ExamManagementContext context)
		{
			_userManager = userManager;
            _context = context;
			
		}
		public IActionResult Index()
        {
            return View();
        }
		
		public async Task<IActionResult> Students()
		{
            var students = _context.ApplicationUser;
            return View(students);
        }


	}
}
