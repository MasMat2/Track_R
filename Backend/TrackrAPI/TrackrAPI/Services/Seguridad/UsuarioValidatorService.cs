using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioValidatorService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly SimpleAES simpleAES;
        public UsuarioValidatorService(
            IUsuarioRepository usuarioRepository,
            SimpleAES simpleAES)
        {
            this.usuarioRepository = usuarioRepository;
            this.simpleAES = simpleAES;
        }

        /// <summary>
        /// Valida si existe un usuario con las credenciales proporcionadas en el LoginRequest.
        /// </summary>
        /// <param name="loginRequest">El objeto que contiene el nombre de usuario y la contraseña ingresados.</param>
        /// <returns>Un objeto Usuario si las credenciales son válidas.</returns>
        /// <exception cref="CdisException">Se lanza cuando las credenciales de usuario y/o contraseña son incorrectas.</exception>
        /// <remarks>
        /// Este método realiza los siguientes pasos:
        /// 1. Encripta la contraseña ingresada utilizando el algoritmo de cifrado simpleAES.
        /// 2. Consulta al repositorio de usuarios para encontrar un usuario con las credenciales proporcionadas (nombre de usuario y contraseña encriptada).
        /// 3. Si no se encuentra un usuario coincidente, se lanza una excepción CdisException con un mensaje de error.
        /// 4. Si se encuentra un usuario coincidente, se devuelve el objeto Usuario correspondiente.
        /// </remarks>
        public Usuario ValidateUserExists(LoginRequest loginRequest)
        {
            string encryptedPassword = simpleAES.EncryptToString(loginRequest.Contrasena);
            Usuario userFromRepo = usuarioRepository.Login(loginRequest.Correo, encryptedPassword);

            if (userFromRepo == null)
            {
                throw new CdisException("Usuario y/o contraseña incorrectos");
            }
            return userFromRepo;
        }
    }
}
