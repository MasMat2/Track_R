using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class RecetaDetalle
    {
        public int IdRecetaDetalle { get; set; }
        public int IdPresentacion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Dosis { get; set; }
        public decimal Frecuencia { get; set; }
        public decimal Duracion { get; set; }
        public string Indicaciones { get; set; } = null!;
        public int IdReceta { get; set; }

        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual Receta IdRecetaNavigation { get; set; } = null!;
    }
}
