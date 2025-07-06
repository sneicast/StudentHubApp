
using MediatR;
using StudentHub.Application.Classes.Dtos;

namespace StudentHub.Application.Classes.Commands
{
    public class ClassEnrollmentCommand : IRequest<ClassEnrollmentResponseDto>
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
    }
}
