namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedientePadecimientoGridDTO
    {
        public int IdExpedientePadecimiento { get; set; }
        public int IdPadecimiento { get; set; }
        public DateTime FechaDiagnostico { get; set; }
        public string nombrePadecimiento { get; set;}

        public string nombreDoctor { get; set; }
    }
}
