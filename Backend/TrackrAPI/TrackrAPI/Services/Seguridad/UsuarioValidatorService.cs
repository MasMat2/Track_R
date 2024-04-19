using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioValidatorService
    {

        private IUsuarioRepository usuarioRepository;
        private IUsuarioRolRepository usuarioRolRepository;
        private IRolRepository rolRepository;
        private IConfirmacionCorreoRepository _confirmacionCorreoRepository;
        private readonly IExpedienteTrackrRepository _expedienteTrackrRepository;
        private readonly SimpleAES simpleAES;


        public UsuarioValidatorService(
            IUsuarioRepository usuarioRepository,
            IUsuarioRolRepository usuarioRolRepository,
            IRolRepository rolRepository,
            SimpleAES simpleAES,
            IConfirmacionCorreoRepository confirmacionCorreoRepository,
            IExpedienteTrackrRepository expedienteTrackrRepository
        )
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioRolRepository = usuarioRolRepository;
            this.rolRepository = rolRepository;
            this.simpleAES = simpleAES;
            this._confirmacionCorreoRepository = confirmacionCorreoRepository;
            _expedienteTrackrRepository = expedienteTrackrRepository;
        }

        private readonly string MensajeContrasenaRequerida = "La contraseña es requerida";
        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajePerfilRequerido = "El perfil es requerido";
        private readonly string MensajeTipoUsuarioRequerido = "El tipo de usuario es requerido";
        private readonly string MensajeUsernameRequerido = "El username es requerido";
        private readonly string MensajeCodigoPostalRequerido = "El código postal es requerido";
        private readonly string MensajeRegimenFiscalRequerido = "El régimen fiscal es requerido";
        private readonly string MensajePuntoVentaRequerido = "El punto de venta es requerido";
        private readonly string MensajeCorreoRequerido = "El correo personal es requerido";
        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeDiasPagoRequeridos = "Los días de pago son requeridos";

        private readonly string MensajeFormatoCorreo = "El formato de correo electrónico es incorrecto";
        private readonly string MensajeFormatoRfc = "El formato del RFC es incorrecto";
        private readonly string MensajeFormatoNombre = "El formato del nombre es incorrecto";
        private readonly string MensajeFormatoApellidoPaterno = "El formato del apellido paterno es incorrecto";
        private readonly string MensajeFormatoApellidoMaterno = "El formato del apellido materno es incorrecto";
        private readonly string MensajeFormatoTelefono = "El formato del teléfono móvil es incorrecto";

        private static readonly int LongitudMaximaComun = 50;
        private static readonly int LongitudMaximaCien = 100;
        private static readonly int LongitudMaximaTelefono = 15;
        private readonly string MensajeLongitudCorreo = $"La longitud máxima del correo electrónico son {LongitudMaximaCien} caracteres";
        private readonly string MensajeLongitudNombre = $"La longitud máxima del nombre son {LongitudMaximaComun} caracteres";
        private readonly string MensajeLongitudApellidoPaterno = $"La longitud máxima del apellido paterno son {LongitudMaximaComun} caracteres";
        private readonly string MensajeLongitudApellidoMaterno = $"La longitud máxima del apellido materno son {LongitudMaximaComun} caracteres";
        private readonly string MensajeLongitudTelefono = $"La longitud máxima del teléfono móvil son {LongitudMaximaTelefono} caracteres";
        private readonly string MensajeLongitudCiudad = $"La longitud máxima de la ciudad son {LongitudMaximaCien} caracteres";

        private readonly string MensajeRfcDuplicado = "El RFC ingresado ya se encuentra registrado";
        private readonly string MensajeUsuarioExistencia = "El usuario no existe";
        private readonly string MensajeCorreoExistencia = "El correo ingresado no se encuentra registrado";
        private readonly string MensajeMedicoExistencia = "El médico no existe";
        private readonly string MensajeMedicoDuplicado = "Ya existe un médio con la misma cédula";

        private readonly string MensajeDependenciaOrdenCompra = "El usuario tiene asociada al menos una orden de compra activa y no se puede ";

        public void ValidarAgregar(Usuario usuario, List<Rol> roles)
        {
            ValidarRequeridos(usuario, roles);
            ValidarLongitudes(usuario);
            ValidarFormatos(usuario);
            ValidarDuplicados(usuario);
        }

        public void ValidarEditar(Usuario usuario, List<Rol> roles)
        {
            ValidarRequeridos(usuario, roles);
            ValidarLongitudes(usuario);
            ValidarFormatos(usuario);
            ValidarDuplicados(usuario);

            // Validar si el usuario fue eliminado desde el "switch" del formulario
            Usuario usuarioDb = usuarioRepository.Consultar(usuario.IdUsuario);
            if (usuarioDb.Habilitado && !usuario.Habilitado)
            {
                ValidarEliminar(usuario.IdUsuario);
            }

            //if (roles != null)
            //{
            //    List<Rol> rolesEliminados = ObtenerRolesEliminados(usuario, roles);
            //    //ValidarDependencias(usuario.IdUsuario, rolesEliminados, true);
            //}
        }

        public void ValidarEliminar(int idUsuario)
        {
            ValidarExistencia(idUsuario);

            List<Rol> roles = usuarioRolRepository
                .ConsultarPorUsuario(idUsuario)
                .Select(usuarioRol => rolRepository.Consultar(usuarioRol.IdRol))
                .ToList();

            //ValidarDependencias(idUsuario, roles, false);
        }

        //public void ValidarDependencias(int idUsuario, List<Rol> roles, bool esEdicion)
        //{
        //    string contexto = esEdicion
        //        ? " eliminar el rol "
        //        : " desactivar el usuario";

        //    Usuario usuario = usuarioRepository.ConsultarDependencias(idUsuario);

        //    if (roles.Any(rol => rol.Clave == GeneralConstant.ClaveRolProveedor))
        //    {
        //        contexto += esEdicion ? "proveedor" : "";

        //        if (usuario.OrdenCompraIdUsuarioProveedorNavigation.Any(oc => oc.IdEstatusOrdenCompraNavigation.Clave == GeneralConstant.ClaveEstatusOrdenCompraPorSurtir))
        //        {
        //            throw new CdisException(MensajeDependenciaOrdenCompra + contexto);
        //        }
        //    }
        //}

        public void ValidarRequeridos(Usuario usuario, List<Rol> roles)
        {
            Validator.ValidarRequerido(usuario.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(usuario.IdPerfil, MensajePerfilRequerido);
            Validator.ValidarRequerido(usuario.IdTipoUsuario, MensajeTipoUsuarioRequerido);
            Validator.ValidarRequerido(usuario.Correo, MensajeUsernameRequerido);
            Validator.ValidarRequerido(usuario.CorreoPersonal, MensajeCorreoRequerido);

            if (!string.IsNullOrEmpty(usuario.Rfc))
            {
                Validator.ValidarRequerido(usuario.CodigoPostal, MensajeCodigoPostalRequerido);
                Validator.ValidarRequerido(usuario.IdRegimenFiscal, MensajeRegimenFiscalRequerido);
            }

            //MetodoPago metodoPagoCredito = metodoPagoRepository.ConsultarPorClave(GeneralConstant.ClaveMetodoPagoCredito);
            //if (usuario.IdMetodoPago != null && usuario.IdMetodoPago == metodoPagoCredito.IdMetodoPago)
            //{
            //    Validator.ValidarRequerido(usuario.DiasPago, MensajeDiasPagoRequeridos);
            //}

            // Validación dependiente de los roles. Esta validación es opcional si roles es null
            if (roles == null)
            {
                return;
            }

            if (roles.Any(rol => rol.Clave == GeneralConstant.ClaveRolVendedor))
            {
                Validator.ValidarRequerido(usuario.IdPuntoVenta, MensajePuntoVentaRequerido);
            }
        }

        public void ValidarLongitudes(Usuario usuario)
        {
            Validator.ValidarLongitudMaximaString(usuario.Correo, LongitudMaximaCien, MensajeLongitudCorreo);
            Validator.ValidarLongitudMaximaString(usuario.Nombre, LongitudMaximaComun, MensajeLongitudNombre);
            Validator.ValidarLongitudMaximaString(usuario.ApellidoPaterno, LongitudMaximaComun, MensajeLongitudApellidoPaterno);
            Validator.ValidarLongitudMaximaString(usuario.ApellidoMaterno, LongitudMaximaComun, MensajeLongitudApellidoMaterno);
            Validator.ValidarLongitudMaximaString(usuario.TelefonoMovil, LongitudMaximaTelefono, MensajeLongitudTelefono);
            Validator.ValidarLongitudMaximaString(usuario.Ciudad, LongitudMaximaCien, MensajeLongitudCiudad);

            //int diasMinimos = 0;
            //MetodoPago metodoPagoCredito = metodoPagoRepository.ConsultarPorClave(GeneralConstant.ClaveMetodoPagoCredito);
            //if (usuario.IdMetodoPago != null && usuario.IdMetodoPago == metodoPagoCredito.IdMetodoPago)
            //{
            //    diasMinimos = 1;
            //}

            //Validator.ValidarRangoEntero(usuario.DiasPago, diasMinimos, 365, $"Los días de pago deben ser entre {diasMinimos} y 365");
        }

        public void ValidarFormatos(Usuario usuario)
        {
            Validator.ValidarNombreSinNumeros(usuario.Nombre, MensajeFormatoNombre);
            Validator.ValidarNombreSinNumeros(usuario.ApellidoPaterno, MensajeFormatoApellidoPaterno);
            Validator.ValidarNombreSinNumeros(usuario.ApellidoMaterno, MensajeFormatoApellidoMaterno);
            Validator.ValidarCorreo(usuario.CorreoPersonal, MensajeFormatoCorreo);
            Validator.ValidarTelefono(usuario.TelefonoMovil, MensajeFormatoTelefono);

            if (!string.IsNullOrEmpty(usuario.Rfc))
            {
                Validator.ValidarRFC(usuario.Rfc, MensajeFormatoRfc);
            }
        }

        public void ValidarDuplicados(Usuario usuario)
        {
            ValidarUsernameDuplicado(usuario);
            ValidarRFCDuplicado(usuario);
        }

        public List<Rol> ObtenerRolesEliminados(Usuario usuario, List<Rol> nuevosRoles)
        {
            List<UsuarioRolDto> rolesDb = usuarioRolRepository.ConsultarPorUsuario(usuario.IdUsuario).ToList();

            List<UsuarioRolDto> rolesEliminados = rolesDb
                .Where(rolDb => !nuevosRoles.Any(rol => rol.IdRol == rolDb.IdRol))
                .ToList();

            return rolesEliminados.Select(rol => rolRepository.Consultar(rol.IdRol)).ToList();

        }

        public void ValidarDuplicadoMedico(UsuarioDto usuario)
        {
            var duplicado = usuarioRepository.ConsultarMedico(usuario.Cedula);

            if (duplicado != null)
            {
                throw new CdisException(MensajeMedicoDuplicado);
            }
        }

        public void ValidarUsernameDuplicado(Usuario usuario)
        {
            Usuario usuarioExistente = this.usuarioRepository.ConsultarPorCorreo(usuario.Correo);

            if (usuarioExistente != null && usuario.IdUsuario != usuarioExistente.IdUsuario && usuario.Correo != null)
            {
                throw new CdisException($@"El usuario '{usuario.Correo}' ya se encuentra registrado");
            }
        }

        public void ValidarRFCDuplicado(Usuario usuario)
        {
            Usuario usuarioExistente = this.usuarioRepository.ConsultarPorRFC(usuario.Rfc);

            if (usuarioExistente != null && usuario.IdUsuario != usuarioExistente.IdUsuario && usuario.Rfc != null && usuario.IdCompania == usuarioExistente.IdCompania)
            {
                throw new CdisException(MensajeRfcDuplicado);
            }
        }

        public void ValidarRestablecerContrasena(RestablecerContrasenaDto usuario)
        {
            Validator.ValidarRequerido(usuario.Correo, MensajeCorreoRequerido);
        }

        public void ValidarCorreoNoExistente(string correoUsuario)
        {
            Usuario usuarioExistente = this.usuarioRepository.ConsultarPorCorreo(correoUsuario);

            if (usuarioExistente == null)
            {
                throw new CdisException(MensajeCorreoExistencia);
            }
        }

        public void ValidarActualizarContrasena(RestablecerContrasenaDto usuario)
        {
            Validator.ValidarRequerido(usuario.Correo, MensajeCorreoRequerido);
            Validator.ValidarRequerido(usuario.Clave, MensajeClaveRequerida);
        }

        public void ValidarProcesarActualizacionContrasena(RestablecerContrasenaDto usuario)
        {
            ValidarActualizarContrasena(usuario);
            Validator.ValidarRequerido(usuario.ContrasenaActualizada, MensajeContrasenaRequerida);
        }

        public void ValidarConfirmarCorreo(ConfirmarCorreoDto datos)
        {
            Validator.ValidarRequerido(datos.Correo, MensajeCorreoRequerido);
            Validator.ValidarRequerido(datos.Token, MensajeClaveRequerida);

        }

        public void ValidarProcesarConfirmarCorreo(Usuario datos)
        {

            var usuario = usuarioRepository.ConsultarPorCorreo(datos.Correo);

            if (usuario != null && usuario.CorreoConfirmado == true)
            {
                throw new CdisException("El usuario ya ha confirmado su correo");
            }
            var usuarioConfirmacion = _confirmacionCorreoRepository.ConsultarPorUsuario(datos.IdUsuario);

            if (usuarioConfirmacion.FechaAlta > Utileria.ObtenerFechaActual().AddDays(1))
            {
                throw new CdisException("El límite de tiempo se ha excedido, favor de reenviar correo de confirmación");
            }

        }

        public void ValidarExistencia(int idUsuario)
        {
            Usuario usuario = usuarioRepository.Consultar(idUsuario);
            if (usuario == null)
            {
                throw new CdisException(MensajeUsuarioExistencia);
            }
        }
        public void ValidarExistenciaMedico(UsuarioDto usuario)
        {
            if (usuario == null)
            {
                throw new CdisException(MensajeMedicoExistencia);
            }
        }

        public bool ValidarUsuarioEsMedico(int idUsuario)
        {
            Usuario usuario = usuarioRepository.Consultar(idUsuario);

            if(usuario == null)
            {
                throw new CdisException(MensajeUsuarioExistencia);
            }

            if(usuario.IdPerfilNavigation.Clave != GeneralConstant.ClavePerfilMedico && usuario.IdPerfilNavigation.Clave != GeneralConstant.ClavePerfilAsistente)
            {
                return false;
            }

            return true;
        }

        public bool ValidarUsuarioEsPaciente(int idUsuario)
        {
            Usuario usuario = usuarioRepository.Consultar(idUsuario);

            if (usuario == null)
            {
                throw new CdisException(MensajeUsuarioExistencia);
            }

            if (usuario.IdPerfilNavigation.Clave != GeneralConstant.ClavePerfilPaciente)
            {
                return false;
                //throw new CdisException("El usuario no está registrado como paciente.");
            }

            return true;

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
        public Usuario ValidateUserExists(LoginRequest loginRequest , bool esMobile)
        {
            string encryptedPassword = simpleAES.EncryptToString(loginRequest.Contrasena);
            //Usuario 
            Usuario userFromRepo = usuarioRepository.Login(loginRequest.Correo, encryptedPassword, loginRequest.ClaveTipoUsuario);

           

            if (userFromRepo == null)
            {
                throw new CdisException("Usuario y/o contraseña incorrectos");
            }

            ValidarUsuarioExpediente(userFromRepo.IdUsuario , esMobile);
            return userFromRepo;
        }

        public Usuario ValidarUsuarioExpediente(int idUsuario , bool esMobile)
        {
            var expediente = _expedienteTrackrRepository.ConsultarPorUsuario(idUsuario);

            if(expediente == null && esMobile)
            {
                throw new CdisException("El usuario no cuenta con expediente médico");
            }

            return usuarioRepository.Consultar(idUsuario);
        }


    }
}
