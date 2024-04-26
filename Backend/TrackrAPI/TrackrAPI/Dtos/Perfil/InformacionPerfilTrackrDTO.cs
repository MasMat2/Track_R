
using TrackrAPI.Dtos.Archivos;

namespace TrackrAPI.Dtos.Perfil
{
    public class InformacionPerfilTrackrDTO
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public ArchivoDTO ImagenBase64 { get; set; }
    }
}
