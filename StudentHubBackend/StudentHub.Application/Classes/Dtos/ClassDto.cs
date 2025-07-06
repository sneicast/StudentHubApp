

namespace StudentHub.Application.Classes.Dtos
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = default!;
        public string ProfessorName { get; set; } = default!;
        public int Credits { get; set; }
    }
}
