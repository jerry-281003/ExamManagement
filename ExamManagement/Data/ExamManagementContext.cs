using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExamManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExamManagement.Data
{
    public class ExamManagementContext : IdentityDbContext
	{
        public ExamManagementContext (DbContextOptions<ExamManagementContext> options)
            : base(options)
        {
        }

        public DbSet<ExamManagement.Models.CorrectAnswer> CorrectAnswer { get; set; } = default!;

        public DbSet<ExamManagement.Models.Course> Course { get; set; } = default!;

        public DbSet<ExamManagement.Models.ExamQuestion> ExamQuestion { get; set; } = default!;

        public DbSet<ExamManagement.Models.Option> Option { get; set; } = default!;

        public DbSet<ExamManagement.Models.Question> Question { get; set; } = default!;

        public DbSet<ExamManagement.Models.Exam> Exam { get; set; } = default!;
		public DbSet<ExamManagement.Models.ApplicationUser> ApplicationUser { get; set; } = default!;
	}
}
