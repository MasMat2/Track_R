namespace TrackrAPI.Dtos.Catalogo
{
    public class DominioDto
    {
        public int IdDominio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoDato { get; set; } = null!;
        public string TipoCampo { get; set; } = null!;
        public int? LongitudMinima { get; set; }
        public int? LongitudMaxima { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public DateTime? FechaMinima { get; set; }
        public DateTime? FechaMaxima { get; set; }
        public bool PermiteFueraDeRango { get; set; }
    }
}
