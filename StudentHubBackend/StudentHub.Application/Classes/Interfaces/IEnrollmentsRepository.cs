using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Interfaces
{
    public interface IEnrollmentsRepository
    {
        Task EnrollStudentInClassAsync(Enrollment enrollment, CancellationToken cancellationToken);
        Task<Enrollment?> GetEnrollmentAsync(int studentId, int classId, CancellationToken cancellationToken);

        Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId, CancellationToken cancellationToken);

        Task RemoveEnrollmentAsync(int studentId, int classId, CancellationToken cancellationToken);
    }
}
