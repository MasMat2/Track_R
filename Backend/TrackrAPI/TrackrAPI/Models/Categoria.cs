using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            ArticuloIdCategoriaNavigation = new HashSet<Articulo>();
            ArticuloIdSubCategoriaNavigation = new HashSet<Articulo>();
            ArticuloIdSubSubCategoriaNavigation = new HashSet<Articulo>();
            CategoriaImagen = new HashSet<CategoriaImagen>();
            InverseIdCategoriaPadreNavigation = new HashSet<Categoria>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdCategoriaPadre { get; set; }
        public string? Clave { get; set; }
        public int? IdCompania { get; set; }

        public virtual Categoria? IdCategoriaPadreNavigation { get; set; }
        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Articulo> ArticuloIdCategoriaNavigation { get; set; }
        public virtual ICollection<Articulo> ArticuloIdSubCategoriaNavigation { get; set; }
        public virtual ICollection<Articulo> ArticuloIdSubSubCategoriaNavigation { get; set; }
        public virtual ICollection<CategoriaImagen> CategoriaImagen { get; set; }
        public virtual ICollection<Categoria> InverseIdCategoriaPadreNavigation { get; set; }
    }
}
