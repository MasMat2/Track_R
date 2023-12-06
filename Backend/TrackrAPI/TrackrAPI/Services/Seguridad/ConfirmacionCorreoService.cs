﻿using MimeKit;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.GestionExpediente;

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

        public ConfirmacionCorreoService(
            IUsuarioRepository usuarioRepository,
            IConfirmacionCorreoRepository confirmacionCorreoRepository,
            SimpleAES simpleAES,
            IConfiguration config,
            CorreoHelper correoHelper,
            UsuarioValidatorService usuarioValidatorService,
            UsuarioRolService usuarioRolService,
            UsuarioLocacionService usuarioLocacionService,
            ExpedienteTrackrService expedienteTrackrService
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
            Usuario usuario = _usuarioRepository.ConsultarPorCorreo(_simpleAES.DecryptString(datosConfirmacionDto.Correo));



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
                usuario.CorreoConfirmado = true;
                usuario.IdPerfil = 2236;
                usuario.IdCompania = 177;
                usuario.IdTipoUsuario = 1003;
                usuario.IdHospital = 169;
                _usuarioRepository.Editar(usuario);


                var locacionesUsuario = _usuarioLocacionService.ConsultarPorUsuario(usuario.IdUsuario);
                var locacionPredeterminada = locacionesUsuario.Where(u => u.IdLocacion == 174).FirstOrDefault();
                var locacionMuguerza = locacionesUsuario.Where(u => u.IdLocacion == 169).FirstOrDefault();

                if (locacionPredeterminada != null)
                {
                    _usuarioLocacionService.Eliminar(locacionPredeterminada.IdUsuarioLocacion);
                }

                if (locacionMuguerza == null)
                {
                    var usuarioLocacion = new UsuarioLocacion()
                    {
                        IdLocacion = 169,
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

            string correoEncriptado = _simpleAES.EncryptToString(usuarioCompleto.Correo);

            string urlFrontEnd = _config.GetSection("AppSettings:UrlFrontEnd").Value;

            var logotipoCdis = await DescargarLogo(urlFrontEnd + "assets/img/logo-trackr.png", "logo");
            //var logotipoHospital = await DescargarLogo(urlFrontEnd + "assets/img/png-Logo-H_C_CEIC.png", "logohospital");

            var mensaje =
                $@"
                    <div>
                        <span><img src=""cid:logo"" style='max-width:100%; height:auto;'></span>
                        <span><img src=""cid:logohospital"" style='max-width:100%; height:auto;' align='right'></span>
                    </div>
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
                    <p>Da clic en el siguiente link para confirmar tu correo:
                        <a href='{urlFrontEnd}#/confirmar-correo?id={correoEncriptado}&tkn={clave}' target='_blank'>
                            Confirmar mi correo
                        </a>
                    </p>
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
                ";

            var correo = new Correo()
            {
                Receptor = usuarioCompleto.CorreoPersonal,
                Asunto = "ATISC: Confirmación de correo",
                Mensaje = mensaje,
                EsMensajeHtml = true,
                Imagenes = new List<MimePart> { logotipoCdis }
            };

            await _correoHelper.Enviar(correo);
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







    }
}
