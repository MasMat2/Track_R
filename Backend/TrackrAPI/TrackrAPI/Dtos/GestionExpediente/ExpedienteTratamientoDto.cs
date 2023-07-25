namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteTratamientoDto
    {
        public int IdExpedienteTratamiento { get; set; }
        public string Farmaco { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Indicaciones { get; set; }
        public int IdPadecimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
