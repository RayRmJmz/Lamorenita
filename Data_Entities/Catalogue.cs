using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Data_Entities
{
    public class Catalogue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public bool Active { get; set; } = true;
    }
}
