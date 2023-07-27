using TrackrAPI.Dtos.Seguridad;

namespace TrackrAPI.Helpers;

public class LoginSinLocacionException : Exception
{
    public List<UsuarioLocacionDto> Locaciones { get; set; }

    public LoginSinLocacionException(List<UsuarioLocacionDto> locaciones, string mensaje) : base(mensaje)
    {
        Locaciones = locaciones;
    }

    public LoginSinLocacionException()
    {
        Locaciones = new();
    }

    public LoginSinLocacionException(string? message) : base(message)
    {
        Locaciones = new();
    }

    public LoginSinLocacionException(string? message, Exception? innerException) : base(message, innerException)
    {
        Locaciones = new();
    }
}
