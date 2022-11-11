using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lamorenita.Data_Entities
{
    public class PhoneNumberEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public int? ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual ContactEntity? Contact { get; set; }
    }
}
