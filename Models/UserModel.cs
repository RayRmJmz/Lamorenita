using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set;}

    }

    public class UserCreateModel
    {
        [DisplayName("Primre apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Longitud {0} debe ser  máximo {1} y mínimo {2}")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
        [DisplayName("Primre apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Longitud {0} debe ser  máximo {1} y mínimo {2}")]
        public string Nombre { get; set; }
        [DisplayName("Primer apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Longitud {0} debe ser  máximo {1} y mínimo {2}")]
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
    }
}
