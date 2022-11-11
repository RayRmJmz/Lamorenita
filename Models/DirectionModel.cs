using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Calle { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Colonia { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Municipio { get; set; }
    }
}
