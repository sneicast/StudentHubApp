using Microsoft.EntityFrameworkCore;
using StudentHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Professor> Professors => Set <Professor>();
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<CreditProgram> CreditPrograms => Set<CreditProgram>();
    }
}
