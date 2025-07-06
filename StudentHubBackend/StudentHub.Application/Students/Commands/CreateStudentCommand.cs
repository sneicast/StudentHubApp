using MediatR;

namespace StudentHub.Application.Students.Commands
{
    public class CreateStudentCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public string Surnames { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int CreditProgramId { get; set; }
    }
}
