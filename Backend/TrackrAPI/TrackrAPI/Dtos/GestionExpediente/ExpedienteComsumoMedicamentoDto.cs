namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteConsumoMedicamentoDto
    {
        public int IdTomaTratamiento { get; set; }
        public int IdTratamientoRecordatorio { get; set; }
        public int IdExpedienteTratamiento { get; set; }
        public string Farmaco { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Indicaciones { get; set; } = string.Empty;
        public string Padecimiento { get; set; } = string.Empty;
        public DateTime FechaToma { get; set; }
        public DateTime FechaEnvio { get; set; }
    }
}
