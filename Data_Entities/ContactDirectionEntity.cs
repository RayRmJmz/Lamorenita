using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lamorenita.Data_Entities
{
    public class ContactDirectionEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual ContactEntity? Contact { get; set; }
        public int? DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public virtual DirectionEntity? Direction { get; set; }
    }
}
