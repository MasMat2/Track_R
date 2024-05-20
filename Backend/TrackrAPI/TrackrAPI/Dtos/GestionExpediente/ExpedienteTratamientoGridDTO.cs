namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteTratamientoGridDTO
    {
        public int IdExpedienteTratamiento { get; set; }
        public string Farmaco { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Indicaciones { get; set; } = string.Empty;
        public string Padecimiento { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string? Dias { get; set; }
        public string? Horas { get; set; }
    }
}
