namespace Lamorenita.Models
{
    public class DirectionViewModel
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set; }
    }

    public class DirectionCreateModel
    { 
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
    }
}
