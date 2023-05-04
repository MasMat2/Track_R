namespace TrackrAPI.Dtos.Catalogo
{
    public class ConceptoGridDto
    {
        public int IdConcepto { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoMovimiento { get; set; }
        public string NombreCuentaContable { get; set; }
        public int? IdCuentaContable { get; set; }
        public int? IdCompania { get; set; }
        public int? IdTipoAuxiliar { get; set; }
        public string NombreUnidadSat { get; set; }
        public string NombreProductoServicioSat { get; set; }
        public string NombresTipoConcepto { get; set; }

    }
}
