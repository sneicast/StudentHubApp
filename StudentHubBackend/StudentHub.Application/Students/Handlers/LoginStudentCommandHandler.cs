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

        public Task<LoginStudentResultDto> Handle(LoginStudentCommand request, CancellationToken cancellationToken)
        {
            
            var student = _studentRepository.GetStudentByEmailAsync(request.Email, cancellationToken)
                .GetAwaiter().GetResult();
            if (student == null || !BCrypt.Net.BCrypt.Verify(request.Password, student.Password_hash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            return Task.FromResult(new LoginStudentResultDto
            {
                AccessToken = _jwtTokenService.GenerateToken(student),
            });
        }

    }
}
