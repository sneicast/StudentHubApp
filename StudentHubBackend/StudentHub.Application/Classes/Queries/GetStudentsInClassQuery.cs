
using MediatR;
using StudentHub.Application.Students.Dtos;

namespace StudentHub.Application.Classes.Queries
{
    public class GetStudentsInClassQuery : IRequest<List<StudentDto>>
    {
        public int ClassId { get; set; } = default!;
        public int StudentId { get; set; } = default!;
    }
}
