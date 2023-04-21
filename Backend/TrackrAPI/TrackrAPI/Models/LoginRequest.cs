namespace TrackrAPI.Models
{
    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string ClaveTipoUsuario { get; set; }
    }
}
