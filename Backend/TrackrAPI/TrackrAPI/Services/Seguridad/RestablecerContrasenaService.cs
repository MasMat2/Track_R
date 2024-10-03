using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using MimeKit;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Sftp;
using ContentType = System.Net.Mime.ContentType;


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
        private readonly SftpService _sftpService;

        public RestablecerContrasenaService(IUsuarioRepository usuarioRepository,
            IRestablecerContrasenaRepository restablecerContrasenaRepository,
            UsuarioValidatorService usuarioValidatorService,
            CorreoHelper correoHelper,
            SimpleAES simpleAES,
            IConfiguration config,
            SftpService sftpService)
        {
            _usuarioRepository = usuarioRepository;
            _restablecerContrasenaRepository = restablecerContrasenaRepository;
            _usuarioValidatorService = usuarioValidatorService;
            _correoHelper = correoHelper;
            _simpleAES = simpleAES;
            _config = config;
            _sftpService = sftpService;
        }

        public void RestablecerContrasena(RestablecerContrasenaDto usuario)
        {
            _usuarioValidatorService.ValidarRestablecerContrasena(usuario);
            _usuarioValidatorService.ValidarCorreoNoExistente(usuario.Correo);

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

        public async void EnviarCorreo(string correoUsuario, string clave)
        {
            var usuarioCompleto = _usuarioRepository.ConsultarPorCorreo(correoUsuario);

            string correoEncriptado = _simpleAES.EncryptToString(usuarioCompleto.Correo);
            string urlFrontEnd = _config.GetSection("AppSettings:UrlFrontEnd").Value;

            //var logotipoTrackr = await DescargarLogo(urlFrontEnd + "assets/img/logo-trackr.png", "logotrackr");
            var logotipoHospital = GetLogo("oncotrackerlogo_primary.png", "logohospital", "image/png");

            //var logotipoCdis = await DescargarLogo(urlFrontEnd + "assets/img/png-Logo-01-Trackr.png", "logocdis");
            //var logotipoHospital = await DescargarLogo(urlFrontEnd + "assets/img/png-Logo-H_C_CEIC.png", "logohospital");

            //var mensaje = 
            //    $@"
            //        <div>
            //            <span><img src=""cid:logotrackr"" style='max-width:100%; height:auto;'></span>
            //            <span><img src=""cid:logohospital"" style='max-width:100%; height:auto;' align='right'></span>
            //        </div>
            //        <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
            //        <p>Da clic en el siguiente link para restablecer tu contraseña:
            //            <a href='{urlFrontEnd}#/restablecer-contrasena?id={correoEncriptado}&tkn={clave}' target='_blank'>
            //                Restablecer mi contraseña
            //            </a>
            //        </p>
            //        <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
            //    ";

            //Diseño de tablas (estándar para mayor compatibilidad con la mayoria de clientes de email)
            var mensaje =
                $@"
                    <body style=""margin: 0; padding: 0; background-color: #F4F4F4; font-family: 'Inter', sans-serif;"">
                        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color: #F4F4F4; padding: 64px;"">
                            <tr>
                                <td align=""center"">
                                    <!-- Contenedor principal -->
                                    <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""max-width: 600px; background-color: #ffffff; border-radius: 8px; padding: 64px; margin:64px; "">
                                        <tr>
                                            <td style=""padding: 16px; text-align: center;"">
                                                <!-- Card Header -->
                                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                                    <tr>
                                                        <td align=""center"">
                                                            <img src=cid:logoHospital alt=""logo Oncotracker"" style=""width: 300px; display: block; margin: 16px;"">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""padding: 32px 0;"">
                                                <!-- Card Body -->
                                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                                    <tr>
                                                        <td align=""center"" style=""padding-bottom: 32px;"">
                                                            <h3 style="" width: 504px; font-family: 'Gayathri', sans-serif; font-size: 28px; color: #292929; margin: 0;"">Restablece tu contraseña</h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align=""center"" style=""padding-bottom: 32px;"">
                                                            <p style="" width: 504px; font-size: 20px; color: #292929; margin: 0;"">Se solicitó un restablecimiento de contraseña para tu cuenta de Oncotracker. Haz click en el botón a continuación para cambiar tu contraseña.</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align=""center"">
                                                            <a href='{urlFrontEnd}#/restablecer-contrasena?id={correoEncriptado}&tkn={clave}' target=""_blank"" style=""
                                                                display: inline-block;
                                                                width: 504px;
                                                                background-color: #695e93;
                                                                color: #ffffff;
                                                                border: 1px solid #ffffff;
                                                                border-radius: 8px;
                                                                font-size: 18px;
                                                                text-decoration: none;
                                                                text-align: center;
                                                                padding: 16px;"">
                                                                Restablecer Contraseña
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""padding: 32px 0 16px 0; text-align: center; background-color: #fFFFFF;"">
                                                <!-- Card Footer -->
                                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                                    <tr>
                                                        <td align=""center"" style=""font-size: 16px; color: #292929;"">
                                                            <p style=""width: 504px; margin: 0; color: #292929"">Si tú no realizaste la solicitud de cambio de contraseña, solo ignora este mensaje.</p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""margin: 32px 0 0 0"">
                                        <tr>
                                            <td align=""center"" style=""font-size: 18px; color: #989898;"">
                                                <p style=""width: 504px; margin: 0;"">CHRISTUS  LATAM HUB CENTER OF EXCELLENCE AND INNOVATION, S.C.</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </body>
                ";

            var correo = new Correo
            {
                Receptor = usuarioCompleto.CorreoPersonal,
                Asunto = "OncoTracker: Restablecimiento de contraseña",
                Mensaje = mensaje,
                EsMensajeHtml = true,
                //Imagenes = new List<MimePart> { logotipoTrackr }
            };

             _correoHelper.Enviar(correo);
        }
        //private async Task<MimePart> DescargarLogo(string imageUrl, string contentId)
        //{
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            byte[] imageData = await httpClient.GetByteArrayAsync(imageUrl);

        //            return new MimePart("image", "png")
        //            {
        //                ContentId = contentId,
        //                Content = new MimeContent(new MemoryStream(imageData), ContentEncoding.Default),
        //                ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
        //                ContentTransferEncoding = ContentEncoding.Base64,
        //            };
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new CdisException("Ocurrió un error al enviar el correo");
        //    }
        //}

        private LinkedResource GetLogo(string imageUrl, string contentId, string mimeType)
        {
            var pathRemoteImage = Path.Combine("Archivos", "Img", imageUrl);
            var image = _sftpService.DownloadFile(pathRemoteImage);
            var bytes = Convert.FromBase64String(image);
            var stream = new MemoryStream(bytes);
            return new LinkedResource(stream)
            {
                ContentId = contentId,
                ContentType = new ContentType(mimeType)
            };
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
