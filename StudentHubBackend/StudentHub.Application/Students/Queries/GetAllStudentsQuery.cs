using MediatR;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<List<Student>> { }
}
