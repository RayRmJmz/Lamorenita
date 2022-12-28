namespace Lamorenita.Data_Entities
{
    public class ProductTypeEntity : Catalogue
    {
        public virtual ICollection<ProductEntity>? Product { get; set; }
    }
}
