using System.ComponentModel.DataAnnotations;

namespace StudentHub.Domain.Entities
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = null!;

        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
