using StudentHub.Domain.Entities;

namespace StudentHub.Application.Students.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Student student);
    }
}
