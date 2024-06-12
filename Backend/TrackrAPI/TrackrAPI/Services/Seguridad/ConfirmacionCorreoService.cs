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
            ITipoUsuarioRepository tipoUsuarioRepository
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
        
        }

        public void ConfirmarCorreo(string correoUsuario)
        {
            _usuarioValidatorService.ValidarCorreoNoExistente(correoUsuario);

            string clave = GenerarClaveConfirmacion();
            var usuarioCompleto = _usuarioRepository.ConsultarPorCorreo(correoUsuario);

            var confirmacionCorreo = new ConfirmacionCorreo
            {
                IdUsuario = usuarioCompleto.IdUsuario,
                Clave = _simpleAES.EncryptToString(clave),
                FechaAlta = Utileria.ObtenerFechaActual()
            };

            Agregar(confirmacionCorreo);

            EnviarCorreo(usuarioCompleto.Correo, clave);

        }
        public bool ValidarConfirmarCorreo(ConfirmarCorreoDto datosConfirmacionDto)
        {
            _usuarioValidatorService.ValidarConfirmarCorreo(datosConfirmacionDto);
            Usuario usuario = _usuarioRepository.ConsultarPorUsername(_simpleAES.DecryptString(datosConfirmacionDto.Correo));



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

        public async void EnviarCorreo(string correoUsuario, string clave){

            var usuarioCompleto = _usuarioRepository.ConsultarPorCorreo(correoUsuario);

            string correoEncriptado = _simpleAES.EncryptToString(usuarioCompleto.CorreoPersonal);

            string urlFrontEnd = _config.GetSection("AppSettings:UrlFrontEnd").Value;

            var logotipoTrackr = GetLogo("png-Logo-01-Trackr.png", "logotrackr" , "image/png");
            var logotipoHospital = GetLogo("png-Logo-H_C_CEIC.png", "logohospital" , "image/png");

            var mensaje =
                $@"
                    <div>
                        <span><img src=cid:logotrackr style='max-width:50%; height:auto;'></span>
                        <span><img src=cid:logoHospital style='max-width:50%; height:auto;' align='right'></span>
                    </div>
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
                    <p>Da clic en el siguiente link para confirmar tu correo:
                        <a href='{urlFrontEnd}#/confirmar-correo?id={correoEncriptado}&tkn={clave}' target='_blank'>
                            Confirmar mi correo
                        </a>
                    </p>
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
                ";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, null, MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(logotipoTrackr);
            htmlView.LinkedResources.Add(logotipoHospital);



            var correo = new Correo()
            {
                Receptor = usuarioCompleto.CorreoPersonal,
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

        private LinkedResource GetLogo(string imageUrl, string contentId , string mimeType)
        {
            var pathRemoteImage = Path.Combine("Archivos" , "Img" , imageUrl);
            var image = _sftpService.DownloadFileAsBase64(pathRemoteImage);
            var bytes = Convert.FromBase64String(image);
            var stream = new MemoryStream(bytes);
            return new LinkedResource(stream){
                ContentId = contentId,
                ContentType = new ContentType(mimeType)
            };
        }







    }
}
