namespace TrackrAPI.Dtos.GestionEntidad
{
    // Hace referencia a un SeccionCampo
    public class ExpedienteColumnaDTO
    {
        public string NombreCampo { get; set; }
        public string Parametro { get; set; }
        public int IdSeccionVariable { get; set;}
        public string ClaveSeccion { get; set; }
        public string Variable { get; set;}
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public bool? MostrarDashboard { get; set; }
        public string? UnidadMedida { get; set; }
    }
}
