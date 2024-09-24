namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteTratamientoPerfilDto
    {
        public int IdExpedienteTratamiento { get; set; }
        public string Farmaco { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Padecimiento { get; set; }
        public string? ImagenBase64 { get; set; }
        public string? TipoMime { get; set; }
    }
}
