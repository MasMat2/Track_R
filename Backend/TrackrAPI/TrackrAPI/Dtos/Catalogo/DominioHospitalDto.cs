namespace TrackrAPI.Dtos.Catalogo;

public class DominioHospitalDto
{
    public int? IdDominioHospital {  get; set; }
    public int IdDominio {  get; set; }
    public int IdHospital { get; set; }
    public int? LongitudMinima { get; set; }
    public int? LongitudMaxima { get; set; }
    public decimal? ValorMinimo { get; set; }
    public decimal? ValorMaximo { get; set; }
    public DateTime? FechaMinima { get; set; }
    public DateTime? FechaMaxima { get; set; }
    public bool ? PermiteFueraDeRango { get; set; }
    public string UnidadMedida { get; set; }
}

