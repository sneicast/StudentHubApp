
namespace StudentHub.Application.Students.Dtos
{
    public class LoginStudentResultDto
    {
        public string AccessToken { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }
}
