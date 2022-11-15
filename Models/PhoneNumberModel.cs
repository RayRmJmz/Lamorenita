using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Models
{
    public class PhoneNumberViewModel
    {
        public int Id { get; set; }
        public string? NumeroTelefono { get; set; }
        public int? ContactoId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class PhoneNumberCreateModel
    {
        [DisplayName("Número teléfono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Longitud {0} debe ser de {1} dígitos ")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "{0} debe ser númerico")]
        public string? NumeroTelefono { get; set; }
        [DisplayName("Contacto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? ContactoId { get; set; }
    }
}
