using MediatR;
using StudentHub.Application.Classes.Dtos;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Application.Classes.Queries;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.Classes.Handlers
{
    public class GetAvailableClassesQueryHandler: IRequestHandler<GetAvailableClassesQuery, List<ClassDto>>
    {
        private readonly IClassesRepository _classesRepository;
        public GetAvailableClassesQueryHandler(IClassesRepository classesRepository)
        {
            _classesRepository = classesRepository;
        }
        public async Task<List<ClassDto>> Handle(GetAvailableClassesQuery request, CancellationToken cancellationToken)
        {
            var result = await _classesRepository.GetAllClassesAsync(cancellationToken);
            return result.Select(c => new ClassDto
            {
                Id = c.Id,
                SubjectName = c.Subject?.Name,
                ProfessorName = c.Professor?.Name,

            }).ToList();
        }
    }
}
