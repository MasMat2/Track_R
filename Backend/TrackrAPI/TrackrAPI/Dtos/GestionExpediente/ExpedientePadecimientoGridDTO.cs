namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedientePadecimientoGridDTO
    {
        public int IdExpedientePadecimiento { get; set; }
        public int IdPadecimiento { get; set; }
        public DateTime FechaDiagnostico { get; set; }
        public string NombrePadecimiento { get; set;}

        public string NombreDoctor { get; set; }
    }
}
