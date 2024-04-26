namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedientePadecimientoDTO
    {
        public int IdExpedientePadecimiento { get; set; }
        public int IdPadecimiento { get; set; }
        public string clavePadecimiento { get; set; }
        public int IdUsuarioDoctor { get; set; }
        public string NombreDoctor { get; set; }
        public string ApellidosDoctor { get; set; }
        public string TituloDoctor { get; set; }
        public string NombrePadecimiento { get; set; }
        public DateTime FechaDiagnostico { get; set; }
        public int IdExpediente { get; set; }
        public bool? EsAntecedente { get; set; }
    }
}
