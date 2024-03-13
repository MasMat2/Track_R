using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CategoriaImagen
    {
        public int IdCategoriaImagen { get; set; }
        public int IdCategoria { get; set; }
        public byte[] Imagen { get; set; } = null!;
        public string TipoMime { get; set; } = null!;
        public string? NombreArchivo { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
    }
}
