using System.ComponentModel.DataAnnotations;

namespace StudentHub.Domain.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
    }
}
