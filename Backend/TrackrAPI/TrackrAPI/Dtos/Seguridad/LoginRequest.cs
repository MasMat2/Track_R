namespace TrackrAPI.Dtos.Seguridad;

public class LoginRequest
{
    public string Correo { get; set; }
    public string Contrasena { get; set; }
    public string ClaveTipoUsuario { get; set; }
    public int? IdLocacion { get; set; }
}
