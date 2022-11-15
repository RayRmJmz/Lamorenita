using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Models
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set; }
    }

    public class StoreCreateModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Descripcion { get; set; }

    }
}
