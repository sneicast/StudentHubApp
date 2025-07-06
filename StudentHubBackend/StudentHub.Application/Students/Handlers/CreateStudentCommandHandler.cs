using MediatR;
using StudentHub.Application.Students.Commands;
using StudentHub.Application.Students.Interfaces;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.Students.Handlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Name = request.Name,
                Surnames = request.Surnames,
                Email = request.Email,
                Password_hash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreditProgramId = request.CreditProgramId,
            };

            return await _studentRepository.AddStudentAsync(student, cancellationToken);
        }
    }
}
