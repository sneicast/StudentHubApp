using Microsoft.EntityFrameworkCore;
using StudentHub.Application.CreditPrograms.Interfaces;
using StudentHub.Domain.Entities;
using StudentHub.Infrastructure.Persistence;

namespace StudentHub.Infrastructure.Repositories
{
    public class CreditProgramRepository : ICreditProgramRepository
    {
        private readonly ApplicationDbContext _context;
        public CreditProgramRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<CreditProgram>> GetAllCreditProgramsAsync(CancellationToken cancellationToken)
        {
           return await _context.CreditPrograms
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
