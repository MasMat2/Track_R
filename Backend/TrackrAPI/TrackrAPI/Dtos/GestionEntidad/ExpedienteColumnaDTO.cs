namespace TrackrAPI.Dtos.GestionEntidad
{
    // Hace referencia a un SeccionCampo
    public class ExpedienteColumnaDTO
    {
        public string NombreCampo { get; set; }
        public string Parametro { get; set; }
        public string Clave { get; set;}
        public string Variable { get; set;}
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get;set; }
    }
}
