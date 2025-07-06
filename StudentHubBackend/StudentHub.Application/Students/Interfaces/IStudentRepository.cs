using StudentHub.Domain.Entities;

namespace StudentHub.Application.Students.Interfaces
{
    public interface IStudentRepository
    {
        Task<int> AddStudentAsync(Student student, CancellationToken cancellationToken);
        Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken);
        Task<Student?> GetStudentByEmailAsync(string email, CancellationToken cancellationToken);

    }
}
