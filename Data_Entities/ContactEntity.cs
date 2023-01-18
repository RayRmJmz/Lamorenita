namespace Lamorenita.Data_Entities
{
    public class ContactEntity : Person
    {
        public virtual IList<PhoneNumberEntity>? PhoneNumber { get; set; }
        public virtual ICollection<ContactDirectionEntity>? ContactDirection { get; set; }
        public virtual ICollection<StoreManagerEntity>? StoreManager { get; set; }
    }
}
