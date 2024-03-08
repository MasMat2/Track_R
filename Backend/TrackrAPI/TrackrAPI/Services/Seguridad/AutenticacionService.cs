using Microsoft.AspNetCore.Http;
using TrackrAPI.Helpers;
using System.Security.Claims;

namespace TrackrAPI.Services.Seguridad
{
    public class AutenticacionService
    {
        private IHttpContextAccessor httpContextAccessor;
        public AutenticacionService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        ///  Utiliza HttpAccessor para obtener el Id del Usuario en Sesión.
        ///  Verifica si el usuario actual tiene un identificador de nombre en sus reclamaciones.
        ///  Si no lo tiene, se lanza una excepción "UnathorizedException" que indica que el acceso no está autorizado.
        ///  Si el usuario tiene un identificador de nombre, se devuelve su valor como un entero después de analizarlo
        ///  con "int.Parse()". El identificador de nombre se obtiene del objeto "httpContextAccessor.HttpContext.User"
        ///  utilizando el método "FindFirst()" y la constante "ClaimTypes.NameIdentifier".
        /// </summary>
        /// <returns>Id Usuario en Sesión</returns>
        /// <exception cref="UnathorizedException"></exception>
        public int ObtenerIdUsuarioSesion()
        {

            if (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                throw new UnathorizedException("Acceso no autorizado");
            }

            return int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
