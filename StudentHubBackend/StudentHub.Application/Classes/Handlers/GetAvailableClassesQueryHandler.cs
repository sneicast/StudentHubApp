using MediatR;
using StudentHub.Application.Classes.Dtos;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Application.Classes.Queries;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Handlers
{
    public class GetAvailableClassesQueryHandler : IRequestHandler<GetAvailableClassesQuery, List<ClassDto>>
    {
        private readonly IClassesRepository _classesRepository;
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        public GetAvailableClassesQueryHandler(IClassesRepository classesRepository, IEnrollmentsRepository enrollmentsRepository)
        {
            _classesRepository = classesRepository;
            _enrollmentsRepository = enrollmentsRepository;
        }
        public async Task<List<ClassDto>> Handle(GetAvailableClassesQuery request, CancellationToken cancellationToken)
        {
            List<Enrollment> studentClasses = await _enrollmentsRepository.GetEnrollmentsByStudentIdAsync(request.StudentId, cancellationToken);
            List<Class> classes = await _classesRepository.GetAllClassesAsync(cancellationToken);
      
            classes = classes.Where(c => !studentClasses.Any(e => e.ClassId == c.Id || e.Class.ProfessorId == c.ProfessorId)).ToList();

            return classes.Select(c => new ClassDto
            {
                Id = c.Id,
                SubjectName = c.Subject?.Name ?? string.Empty, 
                ProfessorName = c.Professor?.Name ?? string.Empty, 
                Credits = c.Subject?.Credits ?? 0 
            }).ToList();
        }
    }
}
