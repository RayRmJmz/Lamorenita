﻿namespace Lamorenita.Models
{
    public class ProductTypeViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set; }

    }

    public class ProductTypeCreateModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }

}
