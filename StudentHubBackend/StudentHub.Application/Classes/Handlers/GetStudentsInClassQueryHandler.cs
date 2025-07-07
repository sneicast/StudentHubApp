
using MediatR;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Application.Classes.Queries;
using StudentHub.Application.Students.Dtos;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Handlers
{
    public class GetStudentsInClassQueryHandler : IRequestHandler<GetStudentsInClassQuery, List<StudentDto>>
    {
        private readonly IClassesRepository _classesRepository;

        public GetStudentsInClassQueryHandler(IClassesRepository classesRepository)
        {
            _classesRepository = classesRepository;
        }

        public async Task<List<StudentDto>> Handle(GetStudentsInClassQuery request, CancellationToken cancellationToken)
        {
           Class? detailClass = await  _classesRepository.GetStudentsInClassAsync(request.ClassId, cancellationToken);
            if(detailClass == null) {
                throw new KeyNotFoundException($"La clase con ID {request.ClassId} no ha sido encontrada.");
            }
               
            List<Enrollment> enrollments = detailClass.Enrollments.ToList();
            if (!enrollments.Any(e => e.StudentId == request.StudentId))
            {
                return new List<StudentDto>();
            }
 
            return enrollments.Select(e => new StudentDto
            {
                Id = e.Student.Id,
                Name = e.Student.Name,
                Email = e.Student.Email
            }).ToList();

        }
    }
}
