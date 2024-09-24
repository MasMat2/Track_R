using MimeKit;
using MimeTypes;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.GestionExpediente;
using TrackrAPI.Services.Sftp;
using ContentDisposition = MimeKit.ContentDisposition;
using ContentType = System.Net.Mime.ContentType;

namespace TrackrAPI.Services.Seguridad
{
    public class ConfirmacionCorreoService
    {
        private readonly IConfirmacionCorreoRepository _confirmacionCorreoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly SimpleAES _simpleAES;
        private readonly IConfiguration _config;
        private readonly CorreoHelper _correoHelper;
        private readonly UsuarioValidatorService _usuarioValidatorService;
        private readonly UsuarioRolService _usuarioRolService;
        private readonly UsuarioLocacionService _usuarioLocacionService;
        private readonly ExpedienteTrackrService _expedienteTrackrService;
        private readonly IPerfilRepository _perfilRepository;
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;
        private readonly SftpService _sftpService;

        public ConfirmacionCorreoService(
            IUsuarioRepository usuarioRepository,
            IConfirmacionCorreoRepository confirmacionCorreoRepository,
            SimpleAES simpleAES,
            IConfiguration config,
            CorreoHelper correoHelper,
            UsuarioValidatorService usuarioValidatorService,
            UsuarioRolService usuarioRolService,
            UsuarioLocacionService usuarioLocacionService,
            ExpedienteTrackrService expedienteTrackrService,
            IPerfilRepository perfilRepository,
            ITipoUsuarioRepository tipoUsuarioRepository,
            SftpService sftpService
        ) {
            this._confirmacionCorreoRepository = confirmacionCorreoRepository;
            this._usuarioRepository = usuarioRepository;
            this._simpleAES = simpleAES;
            this._config = config;
            this._correoHelper = correoHelper;
            this._usuarioValidatorService = usuarioValidatorService;
            this._usuarioRolService = usuarioRolService;
            this._usuarioLocacionService = usuarioLocacionService;
            this._expedienteTrackrService = expedienteTrackrService;
            this._perfilRepository = perfilRepository;
            this._tipoUsuarioRepository = tipoUsuarioRepository;
            _sftpService = sftpService;
        
        }

        public void ConfirmarCorreo(string correoUsuario, int idUsuario)
        {
            _usuarioValidatorService.ValidarCorreoNoExistente(correoUsuario);

            string clave = GenerarClaveConfirmacion();
            var usuarioCompleto = _usuarioRepository.Consultar(idUsuario);

            var confirmacionCorreo = new ConfirmacionCorreo
            {
                IdUsuario = usuarioCompleto.IdUsuario,
                Clave = _simpleAES.EncryptToString(clave),
                FechaAlta = Utileria.ObtenerFechaActual()
            };

            Agregar(confirmacionCorreo);

            EnviarCorreo(usuarioCompleto.Correo, clave , idUsuario);

        }
        public bool ValidarConfirmarCorreo(ConfirmarCorreoDto datosConfirmacionDto)
        {
            _usuarioValidatorService.ValidarExistencia(datosConfirmacionDto.IdUsuario);
            Usuario usuario = _usuarioRepository.Consultar(datosConfirmacionDto.IdUsuario);



            if (usuario != null && ValidarClaveConfirmacion(usuario.IdUsuario, datosConfirmacionDto.Token))
            {
                ProcesarConfirmarCorreo(usuario);
                return true;
            }

            return false;

        }

        private int ProcesarConfirmarCorreo(Usuario usuario)
        {
            _usuarioValidatorService.ValidarProcesarConfirmarCorreo(usuario);

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
            
                int idPerfil = _perfilRepository.ConsultarPorClave(GeneralConstant.ClavePerfilPaciente , GeneralConstant.IdCompaniaMuguerza).IdPerfil;
                int idTipoUsuario = _tipoUsuarioRepository.ConsultarDto(GeneralConstant.ClaveTipoUsuarioPaciente).IdTipoUsuario;

                usuario.CorreoConfirmado = true;
                usuario.IdPerfil = idPerfil;
                usuario.IdCompania = GeneralConstant.IdCompaniaMuguerza;
                usuario.IdTipoUsuario = idTipoUsuario;
                usuario.IdHospital = GeneralConstant.IdHospitalMuguerza;
                _usuarioRepository.Editar(usuario);


                var locacionesUsuario = _usuarioLocacionService.ConsultarPorUsuario(usuario.IdUsuario);
                var locacionPredeterminada = locacionesUsuario.Where(u => u.IdLocacion == GeneralConstant.IdHospitalPredeterminado).FirstOrDefault();
                var locacionMuguerza = locacionesUsuario.Where(u => u.IdLocacion == GeneralConstant.IdHospitalMuguerza).FirstOrDefault();

                if (locacionPredeterminada != null)
                {
                    _usuarioLocacionService.Eliminar(locacionPredeterminada.IdUsuarioLocacion);
                }

                if (locacionMuguerza == null)
                {
                    var usuarioLocacion = new UsuarioLocacion()
                    {
                        IdLocacion = GeneralConstant.IdHospitalMuguerza,
                        IdUsuario = usuario.IdUsuario,
                        IdPerfil = (int)usuario.IdPerfil,
                    };
                    _usuarioLocacionService.Agregar(usuarioLocacion);
                }
                
                scope.Complete();

                return usuario.IdUsuario;

            }
        }

        private ConfirmacionCorreo Agregar(ConfirmacionCorreo confirmacionCorreo)
        {
            return this._confirmacionCorreoRepository.Agregar(confirmacionCorreo);

        }

        public async void EnviarCorreo(string correoUsuario, string clave, int idUsuario){

            string urlFrontEnd = _config.GetSection("AppSettings:UrlFrontEnd").Value;

            var logotipoHospital = GetLogo("oncotrackerlogo_primary.png", "logohospital" , "image/png");

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
                                                            <h3 style="" width: 504px; font-family: 'Gayathri', sans-serif; font-size: 28px; color: #292929; margin: 0;"">Verifica tu correo electrónico</h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align=""center"" style=""padding-bottom: 32px;"">
                                                            <p style="" width: 504px; font-size: 20px; color: #292929; margin: 0;"">Por favor confirma que quieres utilizar este correo electrónico para tu cuenta de Oncotracker.</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align=""center"">
                                                            <a href='{urlFrontEnd}#/confirmar-correo?id={idUsuario}&tkn={clave}' target=""_blank"" style=""
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
                                                                Confirmar mi correo
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
                                                            <p style=""width: 504px; margin: 0; color: #292929"">O pega este enlace en tu navegador: <a href='{urlFrontEnd}#/confirmar-correo?id={idUsuario}&tkn={clave}' target=""_blank"" style=""color: #695e93;"">{urlFrontEnd}#/confirmar-correo?id={idUsuario}&tkn={clave}</a></p>
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

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, null, MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(logotipoHospital);



            var correo = new Correo()
            {
                Receptor = correoUsuario,
                Asunto = "OncoTracker: Confirmación de correo",
                Mensaje = mensaje,
                EsMensajeHtml = true
            };

            _correoHelper.Enviar(correo, htmlView);
        }


        private string GenerarClaveConfirmacion()
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

        private bool ValidarClaveConfirmacion(int idUsuario, string clave)
        {
            string claveEncriptada = _simpleAES.EncryptToString(clave);
            ConfirmacionCorreo resultado = _confirmacionCorreoRepository.ConsultarPorUsuario(idUsuario);
            if (resultado != null && resultado.Clave.Equals(claveEncriptada))
            {
                DateTime fechaActual = Utileria.ObtenerFechaActual();
                TimeSpan timeSpan = fechaActual.Subtract(resultado.FechaAlta);

                // Clave valida por 1 día
                if (timeSpan.TotalDays <= 1)
                {
                    return true;
                }
            }

            return false;
        }

        private LinkedResource GetLogo(string imageUrl, string contentId , string mimeType)
        {
            var pathRemoteImage = Path.Combine("Archivos" , "Img" , imageUrl);
            var image = _sftpService.DownloadFile(pathRemoteImage);
            var bytes = Convert.FromBase64String(image);
            var stream = new MemoryStream(bytes);
            return new LinkedResource(stream){
                ContentId = contentId,
                ContentType = new ContentType(mimeType)
            };
        }

    }
}
