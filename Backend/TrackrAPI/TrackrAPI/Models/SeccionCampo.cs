using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SeccionCampo
    {
        public SeccionCampo()
        {
            EntidadEstructuraTablaValor = new HashSet<EntidadEstructuraTablaValor>();
        }

        public int IdSeccionCampo { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdDominio { get; set; }
        public int IdSeccion { get; set; }
        public bool Requerido { get; set; }
        public int Orden { get; set; }
        public int TamanoColumna { get; set; }
        public bool? Deshabilitado { get; set; }
        public string? Grupo { get; set; }
        public int? Fila { get; set; }
        public int? IdIcono { get; set; }
        public bool? MostrarDashboard { get; set; }

        public virtual Dominio IdDominioNavigation { get; set; } = null!;
        public virtual Icono? IdIconoNavigation { get; set; }
        public virtual Seccion IdSeccionNavigation { get; set; } = null!;
        public virtual ICollection<EntidadEstructuraTablaValor> EntidadEstructuraTablaValor { get; set; }
    }
}
