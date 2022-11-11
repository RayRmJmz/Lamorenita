namespace Lamorenita.Models
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set; }
    }

    public class StoreCreateModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
}
