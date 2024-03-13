namespace TrackrAPI.Dtos.Catalogo
{
    public class CompaniaLogotipoDto : ImagenDto
    {
        public int IdCompaniaLogotipo { get; set; }
        public int IdCompania { get; set; }
        public string Src { get; set; }
    }
}