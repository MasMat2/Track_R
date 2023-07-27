using System;

namespace TrackrAPI.Dtos.Inventario
{
    public class AlmacenGridDto
    {
        public int IdAlmacen { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string TelefonoUno { get; set; }
        public string TelefonoDos { get; set; }
        public string UrlMapa { get; set; }
        public int IdEstatusAlmacen { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int IdEstado { get; set; }
        public int IdCompania { get; set; }
        public string Direccion { get; set; }
        public string ResponsableNombre { get; set; }
        public int? IdCuentaContable { get; set; }
        public string NombreCuentaContable { get; set; }
    }
}
