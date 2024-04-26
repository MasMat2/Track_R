namespace TrackrAPI.Dtos.GestionExpediente
{
    public class AgregarExpedientePadecimientoDTO
    {
        public int IdPadecimiento { get; set; }
        public int IdUsuarioDoctor { get; set; }
        public DateTime FechaDiagnostico { get; set; }
    }
}
