using MimeTypes;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Seguridad;
using System.Text;
using System.Transactions;
using TrackrAPI.Services.Inventario;
using TrackrAPI.Repositorys.Inventario;
using System.Collections.Generic;
using CanalDistAPI.Dtos.Seguridad;
using TrackrAPI.Dtos.Perfil;
using TrackrAPI.Repositorys.GestionExpediente;
using DocumentFormat.OpenXml.Office.CustomXsn;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioService
    {
        private IUsuarioRepository usuarioRepository;
        private IExpedienteTrackrRepository expedienteTrackrRepository;
        private IWebHostEnvironment hostingEnvironment;
        private ITipoUsuarioRepository tipoUsuarioRepository;
        private UsuarioValidatorService usuarioValidatorService;
        private CorreoHelper correoHelper;
        private SimpleAES simpleAES;
        private CarritoService carritoService;
        private UsuarioLocacionService usuarioLocacionService;
        private IEstadoRepository estadoRepository;
        private IDomicilioRepository domicilioRepository;
        private DomicilioValidatorService domicilioValidatorService;
        private IColoniaRepository coloniaRepository;
        private UsuarioRolService usuarioRolService;
        private RolService rolService;
        private BitacoraMovimientoUsuarioService bitacoraMovimientoUsuarioService;

        public UsuarioService(IUsuarioRepository usuarioRepository,
            IWebHostEnvironment hostingEnvironment,
            ITipoUsuarioRepository tipoUsuarioRepository,
            UsuarioValidatorService usuarioValidatorService,
            CorreoHelper correoHelper,
            SimpleAES simpleAES,
            CarritoService carritoService,
            IEstadoRepository estadoRepository,
            IPerfilRepository perfilRepository,
            UsuarioLocacionService usuarioLocacionService,
            IDomicilioRepository domicilioRepository,
            DomicilioValidatorService domicilioValidatorService,
            IColoniaRepository coloniaRepository,
            UsuarioRolService usuarioRolService,
            RolService rolService,
            BitacoraMovimientoUsuarioService bitacoraMovimientoUsuarioService)
        {
            this.usuarioRepository = usuarioRepository;
            this.simpleAES = simpleAES;
            this.usuarioValidatorService = usuarioValidatorService;
            this.hostingEnvironment = hostingEnvironment;
            this.tipoUsuarioRepository = tipoUsuarioRepository;
            this.correoHelper = correoHelper;
            this.estadoRepository = estadoRepository;
            this.carritoService = carritoService;
            this.usuarioLocacionService = usuarioLocacionService;
            this.domicilioRepository = domicilioRepository;
            this.domicilioValidatorService = domicilioValidatorService;
            this.coloniaRepository = coloniaRepository;
            this.usuarioRolService = usuarioRolService;
            this.rolService = rolService;
            this.bitacoraMovimientoUsuarioService = bitacoraMovimientoUsuarioService;
        }

        public Usuario Consultar(int idUsuario)
        {
            var usuario = usuarioRepository.Consultar(idUsuario);

            if (usuario == null)
            {
                throw new CdisException("El usuario no existe");
            }

            return usuario;
        }

        public UsuarioDto ConsultarDto(int idUsuario)
        {
            return usuarioRepository.ConsultarDto(idUsuario);
        }

        public Usuario ConsultarPorNotaVentaDetalle(int idNotaVentaDetalle)
        {
            return usuarioRepository.ConsultarPorNotaVentaDetalle(idNotaVentaDetalle);
        }

        public Usuario ConsultarPublicoEnGeneral(int idCompania)
        {
            return usuarioRepository.ConsultarPublicoEnGeneral(idCompania);
        }

        public Usuario Login(string usuario, string contrasena, string claveRol)
        {
            string ce = simpleAES.EncryptToString(contrasena);
            return usuarioRepository.Login(usuario, ce, claveRol);
        }

        public Usuario LoginAdministrador(string usuario, string contrasena)
        {
            string ce = simpleAES.EncryptToString(contrasena);
            return usuarioRepository.LoginAdministrador(usuario, ce);
        }

        public Usuario VerificarContrasena(int idUsuario, string contrasena)
        {
            string ce = simpleAES.EncryptToString(contrasena);
            return usuarioRepository.VerificarContrasena(idUsuario, ce);
        }

        public Usuario ConsultarPorCorreo(string correo, string claveTipoUsuario)
        {
            return usuarioRepository.ConsultarPorCorreo(correo, claveTipoUsuario);
        }

        public Usuario ConsultarPorUsuario(string usuario)
        {
            return usuarioRepository.ConsultarPorUsuario(usuario);
        }
        public Usuario ConsultarPorRFC(string rfc)
        {
            return usuarioRepository.ConsultarPorRFC(rfc);
        }

        public IEnumerable<UsuarioDto> ConsultarPorRol(string claveRol, int idCompania)
        {
            return usuarioRepository.ConsultarPorRol(claveRol, idCompania);
        }
        public IEnumerable<UsuarioDto> ConsultarParaPuntoVenta(int idCompania)
        {
            return usuarioRepository.ConsultarParaPuntoVenta(idCompania);
        }

        public IEnumerable<UsuarioDto> ConsultarClinicosActivos(string claveTipoUsuario, Usuario usuario)
        {
            return usuarioRepository.ConsultarClinicosActivos(claveTipoUsuario, (int)usuario.IdHospital);
        }

        public Usuario UsuarioEncabezado(int idUsuario)
        {
            var usuario = usuarioRepository.Consultar(idUsuario);

            if (usuario == null)
            {
                throw new CdisException("El usuario no existe");
            }

            return usuario;
        }

        public UsuarioEncabezadoDto ConsultarEncabezado(int idUsuario)
        {
            return usuarioRepository.ConsultarEncabezado(idUsuario);
        }

        public UsuarioEncabezadoDto ConsultarEncabezadoPedidoEnLinea(int idUsuario, string empresa, string token)
        {
            if (idUsuario <= 0)
            {
                var total = carritoService.ConsultarAgregadoPorToken(token);
                var encabezadoDto = new UsuarioEncabezadoDto();
                // encabezadoDto.Logotipo = empresaComercial.Logotipo;
                encabezadoDto.CantidadCarrito = total;
                return encabezadoDto;
            }

            return usuarioRepository.ConsultarEncabezado(idUsuario);
        }

        public IEnumerable<UsuarioDto> ConsultarPorTipoUsuario(string claveTipoUsurio, int idCompania)
        {
            return usuarioRepository.ConsultarPorTipoUsuario(claveTipoUsurio, idCompania);
        }

        public int AgregarCliente(Usuario usuario)
        {
            var aux = usuarioRepository.Agregar(usuario);
            return aux.IdUsuario;
        }

        public void Eliminar(int idUsuario)
        {
            Usuario usuario = usuarioRepository.Consultar(idUsuario);
            usuarioValidatorService.ValidarEliminar(idUsuario);
            usuario.Habilitado = false;
            bitacoraMovimientoUsuarioService.Agregar(GeneralConstant.TipoMovimientoUsuarioEliminacion, "Eliminación del usuario " + usuario.ObtenerNombreCompleto(), null);
            usuarioRepository.Editar(usuario);
        }

        public int Agregar(UsuarioDto usuarioDto, int idLocacion)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                Usuario usuario = MapearUsuario(usuarioDto);

                string contrasenaEncriptada;
                if (usuarioDto.Contrasena == null || usuarioDto.Contrasena == "")
                {
                    string contraseniaAutogenerada = GenerarContraseniaAleatoria();

                    contrasenaEncriptada = simpleAES.EncryptToString(contraseniaAutogenerada);
                    EnviarCorreoConContrasena(usuario, contraseniaAutogenerada);
                }
                else
                {
                    contrasenaEncriptada = simpleAES.EncryptToString(usuarioDto.Contrasena);
                }

                usuario.Habilitado = true;
                usuario.Contrasena = contrasenaEncriptada;

                if (usuarioDto.AdministradorCompania != true && (usuarioDto.IdsRol == null || usuarioDto.IdsRol.Count == 0))
                {
                    throw new CdisException("Se debe seleccionar al menos un rol");
                }

                List<Rol> roles = usuarioDto.IdsRol?.Select(idRol => rolService.Consultar(idRol)).ToList();

                usuarioValidatorService.ValidarAgregar(usuario, roles);
                int idUsuario = usuarioRepository.Agregar(usuario).IdUsuario;

                // Actualizar los roles del usuario
                List<UsuarioRol> usuarioRols = usuarioDto.IdsRol?
                    .Select(idRol =>
                    {
                        return new UsuarioRol()
                        {
                            IdRol = idRol,
                            IdUsuario = idUsuario
                        };
                    })
                    .ToList();

                if (usuarioRols != null)
                {
                    usuarioRolService.Guardar(usuarioRols);
                }

                // El domicilio sólo se agrega cuando se llenan todos los datos de domicilio
                bool esCliente = roles != null && roles.Any(rol => rol.Clave == GeneralConstant.ClaveRolCliente);
                if (esCliente && usuario.TieneDomicilioCompleto())
                {
                    Domicilio domicilioNuevo = ObtenerDomicilioUsuario(usuario);

                    domicilioValidatorService.ValidarAgregar(domicilioNuevo, false);
                    domicilioRepository.Agregar(domicilioNuevo);
                }

                // Guardar la imagen
                if (usuarioDto.ImagenBase64 != null)
                {
                    string nombreArchivo = $"{usuario.IdUsuario}{MimeTypeMap.GetExtension(usuarioDto.ImagenTipoMime)}";
                    string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Usuario", nombreArchivo);
                    usuarioDto.ImagenBase64 = usuarioDto.ImagenBase64.Substring(usuarioDto.ImagenBase64.LastIndexOf(',') + 1);
                    File.WriteAllBytes(path, Convert.FromBase64String(usuarioDto.ImagenBase64));
                }

                // Guardar el perfil
                if (usuarioDto.IdPerfil > 0)
                {
                    UsuarioLocacion permiso = new()
                    {
                        IdPerfil = (int)usuarioDto.IdPerfil,
                        IdUsuario = idUsuario,
                        IdLocacion = idLocacion
                    };
                    usuarioLocacionService.Agregar(permiso);
                }

                scope.Complete();

                return idUsuario;
            }
        }

        public void EditarAdministrador(UsuarioDto usuarioDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                Usuario usuarioActual = usuarioRepository.Consultar(usuarioDto.IdUsuario);
                Usuario usuario = MapearUsuario(usuarioDto);

                usuario.Contrasena = usuarioDto.ContrasenaActualizada == null
                    ? usuarioActual.Contrasena
                    : simpleAES.EncryptToString(usuarioDto.ContrasenaActualizada);

                if (usuarioDto.IdsRol == null || usuarioDto.IdsRol.Count == 0)
                {
                    throw new CdisException("Se debe seleccionar al menos un rol");
                }

                List<Rol> roles = usuarioDto.IdsRol.Select(idRol => rolService.Consultar(idRol)).ToList();

                // Guardar la imagen
                if (usuarioDto.ImagenBase64 != null)
                {
                    string nombreArchivo = $"{usuario.IdUsuario}{MimeTypeMap.GetExtension(usuarioDto.ImagenTipoMime)}";
                    string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Usuario", nombreArchivo);
                    usuarioDto.ImagenBase64 = usuarioDto.ImagenBase64.Substring(usuarioDto.ImagenBase64.LastIndexOf(',') + 1);
                    usuario.ImagenTipoMime = usuarioDto.ImagenTipoMime;
                    File.WriteAllBytes(path, Convert.FromBase64String(usuarioDto.ImagenBase64));
                }

                // El domicilio sólo se actualiza cuando se llenan todos los datos de domicilio
                bool esCliente = roles.Any(rol => rol.Clave == GeneralConstant.ClaveRolCliente);
                if (esCliente && usuario.TieneDomicilioCompleto())
                {
                    ActualizarDomicilioUsuario(usuario);
                }

                usuarioValidatorService.ValidarEditar(usuario, roles);
                usuarioRepository.Editar(usuario);

                // Actualizar los roles del usuario
                List<UsuarioRol> usuarioRols = usuarioDto.IdsRol
                    .Select(idRol =>
                    {
                        return new UsuarioRol()
                        {
                            IdRol = idRol,
                            IdUsuario = usuarioDto.IdUsuario
                        };
                    })
                    .ToList();

                usuarioRolService.Guardar(usuarioRols);

                scope.Complete();
            }
        }

        public void EditarLocacionAdministrador(Usuario usuario)
        {
            var usuarioConsultado = usuarioRepository.Consultar(usuario.IdUsuario);

            usuarioConsultado.IdHospital = usuario.IdHospital;
            usuarioConsultado.IdCompania = usuario.IdCompania;
            usuarioConsultado.IdPerfil = usuario.IdPerfil;
            usuarioRepository.Editar(usuarioConsultado);
        }

        public void Editar(Usuario usuario)
        {
            List<Rol> roles = usuarioRolService.ConsultarPorUsuario(usuario.IdUsuario)
                .Select(usuarioRol => rolService.Consultar(usuarioRol.IdRol))
                .ToList();

            usuarioValidatorService.ValidarEditar(usuario, roles);
            bitacoraMovimientoUsuarioService.Agregar(GeneralConstant.TipoMovimientoUsuarioEdicion, "Edición del usuario " + usuario.ObtenerNombreCompleto(), null);
            usuarioRepository.Editar(usuario);
        }

        public IEnumerable<UsuarioGridDto> ConsultarGeneral(int idCompania)
        {
            return usuarioRepository.ConsultarGeneral(idCompania);
        }

        public IEnumerable<UsuarioGridDto> ConsultarBusquedaGridFiltro(UsuarioDto filtro)
        {
            return usuarioRepository.ConsultarBusquedaGridFiltro(filtro);
        }

        public IEnumerable<UsuarioDto> ConsultarPorRolActivosParaSelector(List<int> roles, int idCompania, int idHospital)
        {
            List<UsuarioDto> usuarios = new List<UsuarioDto>();
            foreach (var rol in roles)
            {
                var usersTempo = usuarioRepository.ConsultarPorRolActivosParaSelector(rol, idCompania, idHospital);
                usuarios.AddRange(usersTempo);
            }
            return usuarios.GroupBy(p => p.IdUsuario)
                            .Select(g => g.First())
                            .ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelector(List<int> roles, int idCompania)
        {
            List<UsuarioDto> usuarios = new List<UsuarioDto>();
            foreach (var rol in roles)
            {
                var usersTempo = usuarioRepository.ConsultarPorRolCompaniaParaSelector(rol, idCompania);
                usuarios.AddRange(usersTempo);
            }
            return usuarios.GroupBy(p => p.IdUsuario)
                            .Select(g => g.First())
                            .ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelectorDomicilio(List<int> roles, int idCompania)
        {
            List<UsuarioDto> usuarios = new List<UsuarioDto>();
            foreach (var rol in roles)
            {
                var usersTempo = usuarioRepository.ConsultarPorRolCompaniaParaSelectorDomicilio(rol, idCompania);
                usuarios.AddRange(usersTempo);
            }
            return usuarios.GroupBy(p => p.IdUsuario)
                            .Select(g => g.First())
                            .ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarUsuariosParaRegistrarEntrada(int idHospital)
        {
            return usuarioRepository.ConsultarUsuariosParaRegistrarEntrada(idHospital);
        }
        public UsuarioDto ConsultarMedico(string cedula)
        {
            var medico = usuarioRepository.ConsultarMedico(cedula);
            usuarioValidatorService.ValidarExistenciaMedico(medico);
            return medico;
        }
        public UsuarioDto ConsultarMedico(int idUsuario)
        {
            return usuarioRepository.ConsultarMedico(idUsuario);
        }

        public int AgregarMedico(UsuarioDto usuarioDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var usuario = new Usuario();

                usuario.Nombre = usuarioDto.Nombre;
                usuario.Cedula = usuarioDto.Cedula;
                usuario.Direccion = usuarioDto.Direccion;

                usuario.Habilitado = true;
                usuario.IdTipoUsuario = tipoUsuarioRepository.ConsultarDto(GeneralConstant.ClaveTipoUsuarioMedicoExterno).IdTipoUsuario;

                usuarioValidatorService.ValidarDuplicadoMedico(usuarioDto);
                var idUsuario = usuarioRepository.Agregar(usuario).IdUsuario;
                scope.Complete();

                return idUsuario;
            }
        }

        public IEnumerable<UsuarioDto> ConsultarPorLocacionParaSelector(int idHospital)
        {
            return usuarioRepository.ConsultarPorLocacionParaSelector(idHospital);
        }

        public IEnumerable<UsuarioSelectorDto> ConsultarParaSelector()
        {
            return usuarioRepository.ConsultarParaSelector();
        }

        private string GenerarContraseniaAleatoria()
        {
            int length = 8;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private void EnviarCorreoConContrasena(Usuario usuario, string constrasenaSinEncriptar)
        {
            try
            {
                var correo = new Correo
                {
                    Receptor = usuario.CorreoPersonal,
                    Asunto = "Registro de Usuario",
                    Mensaje = $@"
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>
                    <p>Ya puedes acceder al sistema con el correo <b>{usuario.Correo}</b> y la contraseña <b>{constrasenaSinEncriptar}</b></p>
                    <p>Te deseamos un excelente día</p>
                    <hr style='border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;'>",
                    EsMensajeHtml = true
                };

                correoHelper.Enviar(correo);
            }
            catch (Exception) { }
        }

        private string ObtenerImagenBase64(string rutaImagen)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(rutaImagen);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        private Domicilio ObtenerDomicilioUsuario(Usuario usuario)
        {
            // Sólo se puede crear un domicilio si los datos de domicilio del usuario están completos
            if (!usuario.TieneDomicilioCompleto())
                return null;

            int idPais = estadoRepository.Consultar((int)usuario.IdEstado).IdPais;
            return new()
            {
                IdPais = idPais,
                IdEstado = (int)usuario.IdEstado,
                IdMunicipio = (int)usuario.IdMunicipio,
                IdLocalidad = (int)usuario.IdLocalidad,
                IdColonia = (int)usuario.IdColonia,
                CodigoPostal = usuario.CodigoPostal,
                Calle = usuario.Calle,
                NumeroExterior = usuario.NumeroExterior,
                NumeroInterior = usuario.NumeroInterior,
                IdUsuario = usuario.IdUsuario,
                IdCompania = usuario.IdCompania
            };
        }

        private Usuario MapearUsuario(UsuarioDto usuarioDto)
        {
            var estado = usuarioDto.IdEstado > 0
                ? estadoRepository.Consultar((int)usuarioDto.IdEstado).Nombre
                : "";

            string direccion = (usuarioDto.Calle ?? "") + (!String.IsNullOrEmpty(usuarioDto.NumeroExterior) ? (", #" + usuarioDto.NumeroExterior) : "")
                + (!String.IsNullOrEmpty(usuarioDto.Colonia) ? (", Col. " + usuarioDto.Colonia) : "")
                + (!String.IsNullOrEmpty(usuarioDto.CodigoPostal) ? (", C.P. " + usuarioDto.CodigoPostal + ", ") : "")
                + estado;

            return new Usuario
            {
                IdUsuario = usuarioDto.IdUsuario,
                Nombre = usuarioDto.Nombre,
                ApellidoPaterno = usuarioDto.ApellidoPaterno,
                ApellidoMaterno = usuarioDto.ApellidoMaterno,
                Correo = usuarioDto.Correo,
                CorreoPersonal = usuarioDto.CorreoPersonal.ToLower(),
                Cedula = usuarioDto.Cedula,
                Habilitado = usuarioDto.Habilitado,
                Rfc = String.IsNullOrWhiteSpace(usuarioDto.Rfc) ? null : usuarioDto.Rfc.ToUpper(),
                SueldoDiario = usuarioDto.SueldoDiario,

                Calle = usuarioDto.Calle,
                NumeroInterior = usuarioDto.NumeroInterior,
                NumeroExterior = usuarioDto.NumeroExterior,
                Colonia = usuarioDto.Colonia,
                Ciudad = usuarioDto.Ciudad,
                CodigoPostal = usuarioDto.CodigoPostal,
                TelefonoMovil = usuarioDto.TelefonoMovil,

                Direccion = direccion,

                IdTituloAcademico = usuarioDto.IdTituloAcademico,
                IdCompania = usuarioDto.IdCompania,
                IdHospital = usuarioDto.IdHospital,
                IdEstado = usuarioDto.IdEstado,
                IdMunicipio = usuarioDto.IdMunicipio,
                IdColonia = usuarioDto.IdColonia,
                IdLocalidad = usuarioDto.IdLocalidad,
                IdPerfil = usuarioDto.IdPerfil,
                IdPuntoVenta = usuarioDto.IdPuntoVenta,
                IdArea = usuarioDto.IdArea,
                IdTipoUsuario = usuarioDto.IdTipoUsuario,
                IdRegimenFiscal = usuarioDto.IdRegimenFiscal,
                NumeroLicencia = usuarioDto.NumeroLicencia,
                DiasPago = usuarioDto.DiasPago,
                IdListaPrecio = usuarioDto.IdListaPrecio,

                IdMetodoPago = usuarioDto.IdMetodoPago,
                IdTipoCliente = usuarioDto.IdTipoCliente,
                IdSatFormaPago = usuarioDto.IdSatFormaPago
            };
        }

        private void ActualizarDomicilioUsuario(Usuario usuario)
        {
            Usuario usuarioOriginal = usuarioRepository.Consultar(usuario.IdUsuario);

            Domicilio domicilioOriginal = ObtenerDomicilioUsuario(usuarioOriginal);
            Domicilio domicilioNuevo = ObtenerDomicilioUsuario(usuario);

            // Se busca el domicilio asociado al usurio correspondiente con el registrado en
            // el formulario de usuario.
            int? idDomicilioUsuario = null;
            if (domicilioOriginal != null)
            {
                idDomicilioUsuario = domicilioRepository
                    .ConsultarPorUsuario(usuario.IdUsuario)
                    .Where(domicilio => domicilio.IsEqualTo(domicilioOriginal))
                    .FirstOrDefault()?.IdDomicilio;
            }

            // Agregar
            if (idDomicilioUsuario == null)
            {
                domicilioValidatorService.ValidarAgregar(domicilioNuevo, false);
                domicilioRepository.Agregar(domicilioNuevo);
            }
            // Sólo se edita si hay cambios
            else if (!domicilioNuevo.IsEqualTo(domicilioOriginal))
            {
                domicilioNuevo.IdDomicilio = (int)idDomicilioUsuario;

                domicilioValidatorService.ValidarEditar(domicilioNuevo, false);
                domicilioRepository.Editar(domicilioNuevo);
            }
        }

        public IEnumerable<UsuarioDto> ConsultarPorNombre(string filtro)
        {
            return usuarioRepository.ConsultarPorNombre(filtro);
        }

        public InformacionGeneralDTO ConsultarInformacionGeneralTrackr(int idUsuario)
        {
            return usuarioRepository.ConsultarInformacionGeneralTrackr(idUsuario);
        }

        public void ActualizarInformacionGeneralTrackr(InformacionGeneralDTO informacion, int idUsuario)
        {
            using TransactionScope scope = new TransactionScope();

            var usuario = usuarioRepository.Consultar(idUsuario);
            var expediente = expedienteTrackrRepository.ConsultarPorUsuario(idUsuario);

            
            usuario.Nombre = informacion.Nombre;
            usuario.ApellidoPaterno = informacion.ApellidoPaterno;
            usuario.ApellidoMaterno = informacion.ApellidoMaterno;
            expediente.FechaNacimiento = informacion.FechaNacimiento;
            expediente.IdGenero = informacion.IdGenero;
            expediente.Peso = informacion.Peso;
            expediente.Cintura = informacion.Cintura;
            expediente.Estatura = informacion.Estatura;
            usuario.Correo = informacion.Correo;
            usuario.TelefonoMovil = informacion.TelefonoMovil;
            usuario.IdEstado = informacion.IdEstado;
            usuario.IdMunicipio = informacion.IdMunicipio;
            usuario.IdLocalidad = informacion.IdLocalidad;
            usuario.IdColonia = informacion.IdColonia;
            usuario.CodigoPostal = informacion.CodigoPostal;
            usuario.Calle = informacion.Calle;
            usuario.NumeroInterior = informacion.NumeroInterior;
            usuario.NumeroExterior = informacion.NumeroExterior;
            

            usuarioRepository.Editar(usuario);
            expedienteTrackrRepository.Editar(expediente);

            scope.Complete();
        }
    }
}