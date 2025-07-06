using System.ComponentModel.DataAnnotations;

namespace StudentHub.Domain.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        public int Credits { get; set; }

        public ICollection<Class> Classes { get; set; } = new List<Class>();

    }
}
