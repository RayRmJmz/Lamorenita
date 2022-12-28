using System.ComponentModel.DataAnnotations.Schema;

namespace Lamorenita.Data_Entities
{
    public class ProductEntity : Catalogue
    {
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int? ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductTypeEntity? ProductType { get; set; }
    }
}
