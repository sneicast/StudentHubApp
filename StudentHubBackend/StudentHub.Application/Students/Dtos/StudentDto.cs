
namespace StudentHub.Application.Students.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surnames { get; set; } = default!;
        public string Email { get; set; } = default!;

    }
}
