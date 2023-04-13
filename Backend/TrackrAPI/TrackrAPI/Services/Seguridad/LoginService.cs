using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class LoginService
    {
        private readonly JwtSettings jwtSettings;
        private readonly UsuarioValidatorService usuarioValidatorService;

        public LoginService(
            IOptions<JwtSettings> jwtOptions,
            UsuarioValidatorService usuarioValidatorService
            )
        {
            jwtSettings = jwtOptions.Value;
            this.usuarioValidatorService = usuarioValidatorService;
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
        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            Usuario usuario = usuarioValidatorService.ValidateUserExists(loginRequest);
            string token = GenerateToken(usuario);
            return await Task.FromResult(new LoginResponse
            {
                Token = token,
                FirstName = usuario.Nombre,
                LastName = usuario.ApellidoPaterno
            });
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
                new Claim(ClaimTypes.Name, usuario.Nombre)
            };

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                expires: DateTime.Now.AddHours(jwtSettings.ExpiryHours),
                claims : claims,
                signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
