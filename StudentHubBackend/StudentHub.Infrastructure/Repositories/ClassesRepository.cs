using Microsoft.EntityFrameworkCore;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Domain.Entities;
using StudentHub.Infrastructure.Persistence;

namespace StudentHub.Infrastructure.Repositories
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly ApplicationDbContext _context;
        public ClassesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Class>> GetAllClassesAsync(CancellationToken cancellationToken)
        {
            return await _context.Classes
                .AsNoTracking()
                .Include(c => c.Subject)
                .Include(c => c.Professor)
                .ToListAsync(cancellationToken);
        }
    }
}
