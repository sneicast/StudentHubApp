using MediatR;
using StudentHub.Application.Students.Commands;
using StudentHub.Application.Students.Dtos;
using StudentHub.Application.Students.Interfaces;



namespace StudentHub.Application.Students.Handlers
{
    internal class LoginStudentCommandHandler : IRequestHandler<LoginStudentCommand, LoginStudentResultDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IJwtTokenService _jwtTokenService;
        public LoginStudentCommandHandler(IStudentRepository studentRepository, IJwtTokenService jwtTokenService)
        {
            _studentRepository = studentRepository;
            _jwtTokenService = jwtTokenService;

        }

        public async Task<LoginStudentResultDto> Handle(LoginStudentCommand request, CancellationToken cancellationToken)
        {
            
            var student = await _studentRepository.GetStudentByEmailAsync(request.Email, cancellationToken);
            if (student == null || !BCrypt.Net.BCrypt.Verify(request.Password, student.Password_hash))
            {
                throw new UnauthorizedAccessException("Correo o contraseña Invalidos");
            }
            return new LoginStudentResultDto
            {
                AccessToken = _jwtTokenService.GenerateToken(student),
                FullName = $"{student.Name} {student.Surnames}",
                Email = student.Email,
                CreditProgramId = student.CreditProgramId ?? 0,
                CreditProgramName = student.CreditProgram?.Name ?? "",
                TotalCredits = student.CreditProgram?.TotalCredits ?? 0


            };
        }

    }
}
