using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad;

public class LoginService
{
    private readonly JwtSettings jwtSettings;
    private readonly UsuarioValidatorService usuarioValidatorService;
    private readonly IUsuarioRepository usuarioRepository;
    private readonly UsuarioLocacionService usuarioLocacionService;

    public LoginService(
        IOptions<JwtSettings> jwtOptions,
        UsuarioValidatorService usuarioValidatorService,
        IUsuarioRepository usuarioRepository,
        UsuarioLocacionService usuarioLocacionService
        )
    {
        jwtSettings = jwtOptions.Value;
        this.usuarioValidatorService = usuarioValidatorService;
        this.usuarioRepository = usuarioRepository;
        this.usuarioLocacionService = usuarioLocacionService;
    }

    /// <summary>
    /// Autentica el usuario y genera un token JWT (JSON Web Token) para el usuario proporcionado.
    /// </summary>
    /// <param name="loginRequest">Campos</param>
    /// <returns>LoginResponse con el token, nombre y apellido</returns>
    /// <remarks>
    /// Valida que el usuario exista, con los datos proporcionados.
    /// Genera el Token JWT. Con el método GenerateToken.
    /// </remarks>
    public LoginResponse Authenticate(LoginRequest loginRequest)
    {
        Usuario usuario = usuarioValidatorService.ValidateUserExists(loginRequest);

        var locacionSeleccionada = ObtenerLocacionSeleccionada(loginRequest, usuario);

        usuario.IdHospital = locacionSeleccionada.IdLocacion;
        usuario.IdCompania = locacionSeleccionada.IdCompania;
        usuario.IdPerfil = locacionSeleccionada.IdPerfil;

        usuarioRepository.Editar(usuario);

        string token = GenerateToken(usuario);
        return new LoginResponse
        {
            Token = token,
            FirstName = usuario.Nombre,
            LastName = usuario.ApellidoPaterno ?? string.Empty
        };
    }

    private UsuarioLocacionDto ObtenerLocacionSeleccionada(LoginRequest loginRequest, Usuario usuario)
    {
        var locaciones = usuarioLocacionService
                    .ConsultarPorUsuario(usuario.IdUsuario)
                    .ToList();

        UsuarioLocacionDto? locacionSeleccionada;
        if (locaciones.Count == 0)
        {
            throw new CdisException("No cuenta con permisos de acceso");
        }
        else if (locaciones.Count == 1)
        {
            locacionSeleccionada = locaciones[0];
        }
        else if (loginRequest.IdLocacion.HasValue)
        {
            int idLocacion = loginRequest.IdLocacion.Value;
            locacionSeleccionada = locaciones.Find(permiso => permiso.IdLocacion == idLocacion);

            if (locacionSeleccionada == null)
            {
                throw new CdisException("No cuenta con permisos para acceder a la locación seleccionada");
            }
        }
        else
        {
            throw new LoginSinLocacionException(locaciones, "Debe seleccionar una locación");
        }

        return locacionSeleccionada;
    }

    /// <summary>
    /// Genera un JWT (JSON Web Token) para el usuario proporcionado.
    /// </summary>
    /// <param name="usuario">Usuario para el cual se generará el token.</param>
    /// <returns>Token JWT como cadena de texto.</returns>
    /// <remarks>
    /// Realiza los siguientes pasos:
    /// 1. Crea las credenciales de firma utilizando la clave secreta y el algoritmo HMAC-SHA256.
    /// 2. Construye un conjunto de claims para el usuario, incluyendo su identificador y nombre.
    /// 3. Crea un token de seguridad JWT con el emisor, la fecha de vencimiento, los claims y las credenciales de firma.
    /// 4. Serializa y devuelve el token de seguridad JWT como una cadena de texto.
    /// </remarks>
    private string GenerateToken(Usuario usuario)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            // TODO: 2023-05-05 -> Reivsar si se puede hacer de otra forma
            new Claim("ic", usuario.IdCompania.ToString()!)
        };

        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(jwtSettings.ExpiryHours),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
