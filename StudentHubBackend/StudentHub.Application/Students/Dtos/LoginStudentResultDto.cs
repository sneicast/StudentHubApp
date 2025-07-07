
namespace StudentHub.Application.Students.Dtos
{
    public class LoginStudentResultDto
    {
        public string AccessToken { get; set; } = default!;
        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;
        public int CreditProgramId { get; set; }
        public string CreditProgramName { get; set; } = default!;
        public int TotalCredits { get; set; } = default!;

    }
}
