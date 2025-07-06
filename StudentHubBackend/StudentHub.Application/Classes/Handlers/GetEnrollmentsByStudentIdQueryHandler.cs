using MediatR;
using StudentHub.Application.Classes.Dtos;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Application.Classes.Queries;

namespace StudentHub.Application.Classes.Handlers
{
    public class GetEnrollmentsByStudentIdQueryHandler : IRequestHandler<GetEnrollmentsByStudentIdQuery, List<ClassEnrollmentResponseDto>>
    {
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        public GetEnrollmentsByStudentIdQueryHandler(IEnrollmentsRepository enrollmentsRepository)
        {
            _enrollmentsRepository = enrollmentsRepository;
        }

        public Task<List<ClassEnrollmentResponseDto>> Handle(GetEnrollmentsByStudentIdQuery request, CancellationToken cancellationToken)
        {
            
           return _enrollmentsRepository.GetEnrollmentsByStudentIdAsync(request.StudentId, cancellationToken)
                .ContinueWith(task =>
                {
                    var enrollments = task.Result;
                    return enrollments.Select(e => new ClassEnrollmentResponseDto
                    {
                        ClassId = e.ClassId,
                        ClassName = e.Class?.Subject?.Name ?? "Unknown",
                        Credits = e.Class?.Subject?.Credits ?? 0,
                        Message = "Estas Registrado a esta clase",
                        ProfessorName = e.Class?.Professor?.Name ?? "Unknown"
                    }).ToList();
                }, cancellationToken);
        }
    }
}
