using System.ComponentModel.DataAnnotations;

namespace StudentHub.Domain.Entities
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
