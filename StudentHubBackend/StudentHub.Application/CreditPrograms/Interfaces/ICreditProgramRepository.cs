using StudentHub.Domain.Entities;

namespace StudentHub.Application.CreditPrograms.Interfaces
{
    public interface ICreditProgramRepository
    {
          Task<List<CreditProgram>> GetAllCreditProgramsAsync(CancellationToken cancellationToken);
          }
}
