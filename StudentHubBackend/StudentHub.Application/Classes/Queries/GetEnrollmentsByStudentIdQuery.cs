
using MediatR;
using StudentHub.Application.Classes.Dtos;

namespace StudentHub.Application.Classes.Queries
{
    public class GetEnrollmentsByStudentIdQuery: IRequest<List<ClassEnrollmentResponseDto>>
    {
        public int StudentId { get; set; }

    }
}
