

using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Interfaces
{
    public interface IClassesRepository
    {
        Task<List<Class>> GetAllClassesAsync(CancellationToken cancellationToken);

        Task<Class?> GetClassByIdAsync(int classId, CancellationToken cancellationToken);

        Task<Class?> GetStudentsInClassAsync(int classId, CancellationToken cancellationToken);
    }
}
