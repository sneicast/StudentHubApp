

using MediatR;
using StudentHub.Application.CreditPrograms.Interfaces;
using StudentHub.Application.CreditPrograms.Queries;
using StudentHub.Domain.Entities;

namespace StudentHub.Application.CreditPrograms.Handlers
{
    internal class GetAllCreditProgramsQueryHandler : IRequestHandler<GetAllCreditProgramsQuery, List<CreditProgram>>
    {
        private readonly ICreditProgramRepository _creditProgramRepository;
        public GetAllCreditProgramsQueryHandler(ICreditProgramRepository creditProgramRepository)
        {
            _creditProgramRepository = creditProgramRepository;
        }
        public async Task<List<CreditProgram>> Handle(GetAllCreditProgramsQuery request, CancellationToken cancellationToken)
        {
           return await _creditProgramRepository.GetAllCreditProgramsAsync(cancellationToken);
        }
    }
}
