namespace TrackrAPI.Dtos.Seguridad
{
    public class AccesoAyudaSeccionDto
    {
        public int? IdAyudaSeccion { get; set; }
        public string NombreAyudaSeccion { get; set; }
        public IEnumerable<AccesoAyudaDto> AccesoAyudas { get; set; }
    }
}
