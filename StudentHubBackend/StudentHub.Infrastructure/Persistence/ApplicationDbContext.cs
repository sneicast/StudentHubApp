using Microsoft.EntityFrameworkCore;
using StudentHub.Domain.Entities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.ClassId });
            
            modelBuilder.Entity<Student>()
               .HasIndex(s => s.Email)
               .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
