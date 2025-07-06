using System.ComponentModel.DataAnnotations;

namespace StudentHub.Domain.Entities
{
    public class CreditProgram
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int TotalCredits { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
