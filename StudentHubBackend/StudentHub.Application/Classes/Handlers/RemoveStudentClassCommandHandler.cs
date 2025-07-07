
using MediatR;
using StudentHub.Application.Classes.Commands;
using StudentHub.Application.Classes.Interfaces;

namespace StudentHub.Application.Classes.Handlers
{
    public class RemoveStudentClassCommandHandler : IRequestHandler<RemoveStudentClassCommand, Unit>
    {
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        public RemoveStudentClassCommandHandler(IEnrollmentsRepository enrollmentsRepository)
        {
            _enrollmentsRepository = enrollmentsRepository;
        }
        public async Task<Unit> Handle(RemoveStudentClassCommand request, CancellationToken cancellationToken)
        {
            await _enrollmentsRepository.RemoveEnrollmentAsync(request.StudentId, request.ClassId, cancellationToken);
            return Unit.Value;
        }
    }
}
