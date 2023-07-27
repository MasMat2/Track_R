namespace TrackrAPI.Dtos.Seguridad;

public class LoginResponse
{
    public string Token { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<UsuarioLocacionDto> Permisos { get; set; }
}
