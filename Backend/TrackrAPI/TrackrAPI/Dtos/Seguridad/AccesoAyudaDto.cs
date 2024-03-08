namespace TrackrAPI.Dtos.Seguridad
{
    public class AccesoAyudaDto
    {
        public int IdAccesoAyuda { get; set; }
        public int IdAcceso { get; set; }
        public string EtiquetaCampo { get; set; }
        public string DescripcionAyuda { get; set; }
        public byte[] Imagen { get; set; }
        public string TipoMime { get; set; }
        public int? Orden { get; set; }
        public string NombreArchivo { get; set; }
        public int? IdAyudaSeccion { get; set; }
        public string NombreAyudaSeccion { get; set; }
    }
}
