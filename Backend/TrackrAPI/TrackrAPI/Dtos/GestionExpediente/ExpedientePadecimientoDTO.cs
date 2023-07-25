namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedientePadecimientoDTO
    {
        public int IdExpedientePadecimiento { get; set; }
        public int IdPadecimiento { get; set; }
        public DateTime FechaDiagnostico { get; set; }
        public string NombrePadecimiento { get; set; }
    }
}
