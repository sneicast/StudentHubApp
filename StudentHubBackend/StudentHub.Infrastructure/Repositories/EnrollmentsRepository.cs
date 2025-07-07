
using Microsoft.EntityFrameworkCore;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Domain.Entities;
using StudentHub.Infrastructure.Persistence;

namespace StudentHub.Infrastructure.Repositories
{
    public class EnrollmentsRepository : IEnrollmentsRepository
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task EnrollStudentInClassAsync(Enrollment enrollment, CancellationToken cancellationToken)
        {
           _context.Enrollments.Add(enrollment);
           return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Enrollment?> GetEnrollmentAsync(int studentId, int classId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Class)
                .ThenInclude(c => c.Subject)
                .Include(e => e.Class)
                .ThenInclude(s => s.Professor)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ClassId == classId, cancellationToken);

        }

        public Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
        {
            return _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Class)
                .ThenInclude(c => c.Subject)
                .Include(e => e.Class)
                .ThenInclude(s => s.Professor)
                .Where(e => e.StudentId == studentId)
                .ToListAsync(cancellationToken);
        }

        public Task RemoveEnrollmentAsync(int studentId, int classId, CancellationToken cancellationToken)
        {
            _context.Enrollments.Remove(new Enrollment { StudentId = studentId, ClassId = classId });
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
