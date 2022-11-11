namespace Lamorenita.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set; }
    }

    public class ContactCreateModel
    {
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }

    }
}
