namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteConsumoMedicamentoGridDto
    {
        public int IdTomaTratamiento { get; set; }
        public string Farmaco { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Indicaciones { get; set; } = string.Empty;
        public string Padecimiento { get; set; } = string.Empty;
        public string? FechaTomada{ get; set; }
        public string? FechaEstablecida { get; set; }
    }
}
