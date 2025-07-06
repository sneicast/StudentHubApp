using MediatR;
using StudentHub.Application.Students.Dtos;

namespace StudentHub.Application.Students.Commands
{
    public class LoginStudentCommand : IRequest<LoginStudentResultDto>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    
    }
}
