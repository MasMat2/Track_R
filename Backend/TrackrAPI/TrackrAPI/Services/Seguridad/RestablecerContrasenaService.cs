using System.Security.Cryptography;
using System.Text;
using MimeKit;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class RestablecerContrasenaService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRestablecerContrasenaRepository _restablecerContrasenaRepository;
        private readonly UsuarioValidatorService _usuarioValidatorService;
        private readonly CorreoHelper _correoHelper;
        private readonly SimpleAES _simpleAES;
        private readonly IConfiguration _config;

        public RestablecerContrasenaService(IUsuarioRepository usuarioRepository,
            IRestablecerContrasenaRepository restablecerContrasenaRepository,
            UsuarioValidatorService usuarioValidatorService,
            CorreoHelper correoHelper,
            SimpleAES simpleAES,
            IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _restablecerContrasenaRepository = restablecerContrasenaRepository;
            _usuarioValidatorService = usuarioValidatorService;
            _correoHelper = correoHelper;
            _simpleAES = simpleAES;
            _config = config;
        }

        public void RestablecerContrasena(RestablecerContrasenaDto usuario)
        {
            _usuarioValidatorService.ValidarRestablecerContrasena(usuario);
            _usuarioValidatorService.ValidarCorreoNoExistente(usuario);

            string clave = GenerarClaveRestablecimiento();
            var usuarioCompleto = _usuarioRepository.ConsultarPorCorreo(usuario.Correo);

            var modeloRestablecer = new RestablecerContrasena
            {
                IdUsuario = usuarioCompleto.IdUsuario,
                Clave = _simpleAES.EncryptToString(clave),
                FechaAlta = Utileria.ObtenerFechaActual()
            };

            Agregar(modeloRestablecer);

            EnviarCorreo(usuarioCompleto.Correo, clave);
        }

        public bool ValidarActualizarContrasena(RestablecerContrasenaDto usuarioDto)
        {
            _usuarioValidatorService.ValidarActualizarContrasena(usuarioDto);
            Usuario usuario = _usuarioRepository.ConsultarPorCorreo(_simpleAES.DecryptString(usuarioDto.Correo));

            if (usuario != null && ValidarClaveRestablecimiento(usuario.IdUsuario, usuarioDto.Clave))
            {
                return true;
            }

            return false;
        }

        public void ProcesarActualizacionContrasena(RestablecerContrasenaDto usuarioDto)
        {
            _usuarioValidatorService.ValidarProcesarActualizacionContrasena(usuarioDto);

            var usuario = _usuarioRepository.ConsultarPorCorreo(_simpleAES.DecryptString(usuarioDto.Correo));

            if (usuario != null && ValidarClaveRestablecimiento(usuario.IdUsuario, usuarioDto.Clave))
            {
                usuario.Contrasena = _simpleAES.EncryptToString(usuarioDto.ContrasenaActualizada);
                _usuarioRepository.Editar(usuario);
                return;
            }

            throw new CdisException("Ha ocurrido un error al intentar actualizar la contraseña. El tiempo de validación se ha agotado.");
        }

        public async Task EnviarCorreo(string correoUsuario, string clave)
        {
            var usuarioCompleto = _usuarioRepository.ConsultarPorCorreo(correoUsuario);

            string correoEncriptado = _simpleAES.EncryptToString(usuarioCompleto.Correo);
            string urlFrontEnd = _config.GetSection("AppSettings:UrlFrontEnd").Value;

            var logotipoCdis = await DescargarLogo(urlFrontEnd + "assets/img/png-Logo-01-Trackr.png", "logo");
            var logotipoHospital = await DescargarLogo(urlFrontEnd + "assets/img/png-Logo-H_C_CEIC.png", "logohospital");

            var mensaje = $@"
        <div>
            <span><img src=""cid:logo"" style='max-width:100%; height:auto;'></span>
            <span><img src=""cid:logohospital"" style='max-width:100%; height:auto;' align='right'></span>
        </div>
        <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
        <p>Da clic en el siguiente link para restablecer tu contraseña:
            <a href='{urlFrontEnd}#/acceso/restablecer-contrasena?id={correoEncriptado}&tkn={clave}' target='_blank'>
                Restablecer mi contraseña
            </a>
        </p>
        <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
    ";

            var correo = new Correo
            {
                Receptor = usuarioCompleto.CorreoPersonal,
                Asunto = "ATISC: Restablecimiento de contraseña",
                Mensaje = mensaje,
                EsMensajeHtml = true,
                Imagenes = new List<MimePart> { logotipoCdis, logotipoHospital }
            };

            await _correoHelper.Enviar(correo);
        }
        private async Task<MimePart> DescargarLogo(string imageUrl, string contentId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    byte[] imageData = await httpClient.GetByteArrayAsync(imageUrl);

                    return new MimePart("image", "png")
                    {
                        ContentId = contentId,
                        Content = new MimeContent(new MemoryStream(imageData), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
                        ContentTransferEncoding = ContentEncoding.Base64,
                    };
                }
            }
            catch (Exception)
            {
                throw new CdisException("Ocurrió un error al enviar el correo");
            }
        }


        private string GenerarClaveRestablecimiento()
        {
            string MascaraCodigo = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int LongitudClave = 16;
            char[] chars = MascaraCodigo.ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[LongitudClave];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(LongitudClave);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }

        private RestablecerContrasena Agregar(RestablecerContrasena restablecerContrasena)
        {
            return _restablecerContrasenaRepository.Agregar(restablecerContrasena);
        }

        private bool ValidarClaveRestablecimiento(int idUsuario, string clave)
        {
            string claveEncriptada = _simpleAES.EncryptToString(clave);
            RestablecerContrasena resultado = _restablecerContrasenaRepository.ConsultarPorUsuario(idUsuario);
            if (resultado != null && resultado.Clave.Equals(claveEncriptada))
            {
                DateTime fechaActual = Utileria.ObtenerFechaActual();
                TimeSpan timeSpan = fechaActual.Subtract(resultado.FechaAlta);

                // Clave valida por 120 minutos
                if (timeSpan.TotalMinutes <= 120)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
