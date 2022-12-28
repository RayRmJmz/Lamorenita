using System.ComponentModel.DataAnnotations.Schema;

namespace Lamorenita.Data_Entities
{
    public class StoreEntity : Catalogue
    {
        public virtual ICollection<StoreDirectionEntity>? StoreDirection { get; set; }
        public virtual ICollection<StoreManagerEntity>? StoreManager { get; set; }

    }
}
