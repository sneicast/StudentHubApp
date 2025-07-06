using StudentHub.Application.Students.Interfaces;
using StudentHub.Domain.Entities;
using StudentHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace StudentHub.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync(cancellationToken);
            return student.Id;
        }

        public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Students.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Student?> GetStudentByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Email == email, cancellationToken);
        }
    }
}
