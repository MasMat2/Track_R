namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedientePadecimientoDTO
    {
        public int IdExpedientePadecimiento { get; set; }
        public int IdPadecimiento { get; set; }
        public string NombreDoctor { get; set; }
        public string NombrePadecimiento { get; set; }
        public DateTime FechaDiagnostico { get; set; }
    }
}
