using MediatR;
using StudentHub.Application.Students.Interfaces;
using StudentHub.Application.Students.Queries;
using StudentHub.Domain.Entities;


namespace StudentHub.Application.Students.Handlers
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<Student>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _studentRepository.GetAllStudentsAsync(cancellationToken);
        }
    }
}
