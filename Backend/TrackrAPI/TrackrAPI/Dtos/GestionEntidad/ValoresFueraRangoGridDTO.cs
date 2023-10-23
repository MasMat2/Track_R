namespace TrackrAPI.Dtos.GestionEntidad
{
    // Hace referencia a una EntidadEstructuraTablaValor
    public class ValoresFueraRangoGridDTO
    {
        public string NombrePadecimiento { get; set; }
        public string Variable { get; set; }
        public string Parametro { get; set; }
        public DateTime? FechaHora { get; set; }
        public string ValorRegistrado { get; set; }
        public string ValorReferencia { get; set; }

    }
}
