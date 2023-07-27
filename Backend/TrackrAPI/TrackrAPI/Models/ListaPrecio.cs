using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ListaPrecio
    {
        public ListaPrecio()
        {
            HospitalIdListaPrecioDefaultNavigation = new HashSet<Hospital>();
            HospitalIdListaPrecioLineaNavigation = new HashSet<Hospital>();
            ListaPrecioClinica = new HashSet<ListaPrecioClinica>();
            ListaPrecioDetalle = new HashSet<ListaPrecioDetalle>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdListaPrecio { get; set; }
        public string Clave { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public string Nombre { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public bool TodasClinicas { get; set; }
        public int? IdMoneda { get; set; }

        public virtual Moneda? IdMonedaNavigation { get; set; }
        public virtual ICollection<Hospital> HospitalIdListaPrecioDefaultNavigation { get; set; }
        public virtual ICollection<Hospital> HospitalIdListaPrecioLineaNavigation { get; set; }
        public virtual ICollection<ListaPrecioClinica> ListaPrecioClinica { get; set; }
        public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalle { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
