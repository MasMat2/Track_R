namespace TrackrAPI.Dtos.Catalogo
{
    public class ImagenDto
    {
        public int IdImagen { get; set; }
        public string ImagenBase64 { get; set; }
        public string TipoMime { get; set; }
        public string NombreImagen { get; set; }
    }
}