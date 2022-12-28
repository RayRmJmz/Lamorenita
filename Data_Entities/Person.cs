using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Data_Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        [Required, MaxLength(100)]
        public string SecondLastName { get; set; }
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public bool Active { get; set; } = true;
    }
}
