using Microsoft.Extensions.Configuration;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System;
using System.Security.Cryptography;
using System.Text;

namespace TrackrAPI.Services.Seguridad
{
    public class RestablecerContrasenaService
    {
        private IUsuarioRepository usuarioRepository;
        private IRestablecerContrasenaRepository restablecerContrasenaRepository;
        private UsuarioValidatorService usuarioValidatorService;
        private CorreoHelper correoHelper;
        private SimpleAES simpleAES;
        private IConfiguration config;

        public RestablecerContrasenaService(IUsuarioRepository usuarioRepository,
            IRestablecerContrasenaRepository restablecerContrasenaRepository,
            UsuarioValidatorService usuarioValidatorService,
            CorreoHelper correoHelper,
            SimpleAES simpleAES,
            IConfiguration config)
        {
            this.usuarioRepository = usuarioRepository;
            this.restablecerContrasenaRepository = restablecerContrasenaRepository;
            this.usuarioValidatorService = usuarioValidatorService;
            this.correoHelper = correoHelper;
            this.simpleAES = simpleAES;
            this.config = config;
        }

        public void RestablecerContrasena(Usuario usuario)
        {
            usuarioValidatorService.ValidarRestablecerContrasena(usuario);
            usuarioValidatorService.ValidarCorreoNoExistente(usuario);

            string clave = GenerarClaveRestablecimiento();
            var usuarioCompleto = usuarioRepository.ConsultarPorCorreo(usuario.Correo);

            var modeloRestablecer = new RestablecerContrasena
            {
                IdUsuario = usuarioCompleto.IdUsuario,
                Clave = simpleAES.EncryptToString(clave),
                FechaAlta = Utileria.ObtenerFechaActual()
            };

            Agregar(modeloRestablecer);

            EnviarCorreo(usuarioCompleto.Correo, clave);
        }

        public bool ValidarActualizarContrasena(UsuarioDto usuarioDto)
        {
            usuarioValidatorService.ValidarActualizarContrasena(usuarioDto);
            Usuario usuario = usuarioRepository.ConsultarPorCorreo(simpleAES.DecryptString(usuarioDto.Correo));

            if (usuario != null && ValidarClaveRestablecimiento(usuario.IdUsuario, usuarioDto.Clave))
            {
                return true;
            }

            return false;
        }

        public void ProcesarActualizacionContrasena(UsuarioDto usuarioDto)
        {
            usuarioValidatorService.ValidarProcesarActualizacionContrasena(usuarioDto);

            var usuario = usuarioRepository.ConsultarPorCorreo(simpleAES.DecryptString(usuarioDto.Correo));

            if (usuario != null && ValidarClaveRestablecimiento(usuario.IdUsuario, usuarioDto.Clave))
            {
                usuario.Contrasena = simpleAES.EncryptToString(usuarioDto.ContrasenaActualizada);
                usuarioRepository.Editar(usuario);
                return;
            }

            throw new CdisException("Ha ocurrido un error al intentar actualizar la contraseña. El tiempo de validación se ha agotado.");
        }

        private void EnviarCorreo(string correoUsuario, string clave)
        {
            var usuarioCompleto = usuarioRepository.ConsultarPorCorreo(correoUsuario);

            string correoEncriptado = simpleAES.EncryptToString(usuarioCompleto.Correo);
            string urlFrontEnd = config.GetSection("AppSettings:UrlFrontEnd").Value;

            string logotipo = config.GetSection("AppSettings:UrlFrontEnd").Value + "assets/img/logotipo.png";
            string logotipo2 = config.GetSection("AppSettings:UrlFrontEnd").Value + "assets/img/atencion-express.png";

            var correo = new Correo
            {
                Receptor = usuarioCompleto.CorreoPersonal,
                Asunto = "ATISC: Restablecimiento de contraseña",
                Mensaje =
               @$"<div>
                        <span><img src='{logotipo}' style='width: 200px; height: 45px;'></span>
                        <span><img src='{logotipo2}'  style='width: 130px; height: 48px;' align='right'></span>
                    </div>
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
                    <p>Da clic en el siguiente link para restablecer tu contraseña: <a href='{urlFrontEnd}#/restablecer-contrasena?id={correoEncriptado}&tkn={clave}' target='_blank'>  Restablecer mi contraseña </a></p>
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>",
                EsMensajeHtml = true
            };

            correoHelper.Enviar(correo);
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
            return restablecerContrasenaRepository.Agregar(restablecerContrasena);
        }

        private bool ValidarClaveRestablecimiento(int idUsuario, string clave)
        {
            string claveEncriptada = simpleAES.EncryptToString(clave);
            RestablecerContrasena resultado = restablecerContrasenaRepository.ConsultarPorUsuario(idUsuario);
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
