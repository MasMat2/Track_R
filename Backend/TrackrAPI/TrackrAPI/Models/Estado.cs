using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Almacen = new HashSet<Almacen>();
            Compania = new HashSet<Compania>();
            Domicilio = new HashSet<Domicilio>();
            FacturaIdEstadoEmisorNavigation = new HashSet<Factura>();
            FacturaIdEstadoReceptorNavigation = new HashSet<Factura>();
            Hospital = new HashSet<Hospital>();
            Localidad = new HashSet<Localidad>();
            Municipio = new HashSet<Municipio>();
            Proveedor = new HashSet<Proveedor>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public string? Clave { get; set; }

        public virtual Pais IdPaisNavigation { get; set; } = null!;
        public virtual ICollection<Almacen> Almacen { get; set; }
        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<Factura> FacturaIdEstadoEmisorNavigation { get; set; }
        public virtual ICollection<Factura> FacturaIdEstadoReceptorNavigation { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<Localidad> Localidad { get; set; }
        public virtual ICollection<Municipio> Municipio { get; set; }
        public virtual ICollection<Proveedor> Proveedor { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
