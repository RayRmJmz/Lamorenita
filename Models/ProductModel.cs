using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lamorenita.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoProductoId { get; set; }
        public string TipoProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set; }
        

    }

    public class ProductCreateModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Minimo 3 y Maximo 30 letras")]
        public string Nombre { get; set; }

        [DisplayName("Tipo de producto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? TipoProductoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Precio { get; set; }

        public int Existencia { get; set; }

    }
}
