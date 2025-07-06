using MediatR;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.CreditPrograms.Queries
{
    public class GetAllCreditProgramsQuery: IRequest<List<CreditProgram>>
    {
    }
}
