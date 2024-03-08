namespace TrackrAPI.Dtos.Catalogo
{
    public class HospitalLogotipoDto : ImagenDto
    {
        public int IdHospitalLogotipo { get; set; }
        public int IdHospital { get; set; }
        public string Src { get; set; }
    }
}
