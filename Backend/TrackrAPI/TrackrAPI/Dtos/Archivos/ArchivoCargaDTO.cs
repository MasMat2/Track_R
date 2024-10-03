namespace TrackrAPI.Dtos.Archivos
{
    public class ArchivoCarga
    {
        public string ArchivoBase64 { get; set; } = string.Empty;
        public string ArchivoNombre { get; set; } = string.Empty;
        public string ArchivoTipoMime { get; set; } = string.Empty;
    }
}