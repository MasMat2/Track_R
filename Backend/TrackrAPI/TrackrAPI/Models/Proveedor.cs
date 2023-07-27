using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public string NumeroExterior { get; set; } = null!;
        public string? NumeroInterior { get; set; }
        public string Colonia { get; set; } = null!;
        public string Localidad { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string? UrlMapa { get; set; }
        public string TelefonoUno { get; set; } = null!;
        public string TelefonoDos { get; set; } = null!;
        public string Contacto { get; set; } = null!;
        public int IdEstado { get; set; }
        public int IdTipoProveedor { get; set; }
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual TipoProveedor IdTipoProveedorNavigation { get; set; } = null!;
    }
}
