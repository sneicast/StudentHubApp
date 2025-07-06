

using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Interfaces
{
    public interface IClassesRepository
    {
        Task<List<Class>> GetAllClassesAsync(CancellationToken cancellationToken);

    }
}
