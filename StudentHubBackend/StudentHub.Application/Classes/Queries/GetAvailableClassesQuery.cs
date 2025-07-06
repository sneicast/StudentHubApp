

using MediatR;
using StudentHub.Application.Classes.Dtos;

namespace StudentHub.Application.Classes.Queries
{
    public class GetAvailableClassesQuery: IRequest<List<ClassDto>>
    {
    }
}
