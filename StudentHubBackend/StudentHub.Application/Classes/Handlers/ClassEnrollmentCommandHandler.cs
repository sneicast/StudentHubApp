

using MediatR;
using StudentHub.Application.Classes.Commands;
using StudentHub.Application.Classes.Dtos;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Handlers
{
    public class ClassEnrollmentCommandHandler : IRequestHandler<ClassEnrollmentCommand, ClassEnrollmentResponseDto>
    {
        private readonly IEnrollmentsRepository _enrollmentsRepository;

        public ClassEnrollmentCommandHandler(IEnrollmentsRepository enrollmentsRepository)
        {
            _enrollmentsRepository = enrollmentsRepository;
        }
        public async Task<ClassEnrollmentResponseDto> Handle(ClassEnrollmentCommand request, CancellationToken cancellationToken)
        {
            Enrollment newEnrollment = new Enrollment
            {
                StudentId = request.StudentId,
                ClassId = request.ClassId
            };
            await _enrollmentsRepository.EnrollStudentInClassAsync(newEnrollment, cancellationToken);

            Enrollment? existingEnrollment = await _enrollmentsRepository.GetEnrollmentAsync(request.StudentId, request.ClassId, cancellationToken);
            if (existingEnrollment == null)
            {
                throw new InvalidOperationException("Enrollment failed, please try again.");
            }

            return new ClassEnrollmentResponseDto
            {
            
                ClassId = existingEnrollment.ClassId,
                ClassName = existingEnrollment.Class?.Subject?.Name ?? "Unknown",
                Credits = existingEnrollment.Class?.Subject?.Credits ?? 0,
                Message = "Registro a clase exitoso",
                ProfessorName = existingEnrollment.Class?.Professor?.Name ?? "Unknown"
            };




        }
    }
}
