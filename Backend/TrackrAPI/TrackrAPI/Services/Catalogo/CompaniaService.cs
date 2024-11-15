using Microsoft.AspNetCore.Hosting;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Inventario;
using TrackrAPI.Services.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class CompaniaService
    {
        private readonly IAccesoPerfilRepository accesoPerfilRepository;
        private readonly IAccesoRepository accesoRepository;
        private readonly ICompaniaRepository companiaRepository;
        private readonly IHospitalRepository hospitalRepository;
        private readonly IPerfilRepository perfilRepository;
        private readonly IRolRepository rolRepository;

        private readonly CompaniaContactoService companiaContactoService;
        private readonly CompaniaValidatorService companiaValidatorService;
        private readonly CorreoHelper correoHelper;
        private readonly GiroComercialService giroComercialService;
        private readonly HospitalService hospitalService;
        private readonly UsuarioRolService usuarioRolService;
        private readonly UsuarioService usuarioService;

        private readonly IWebHostEnvironment env;

        public CompaniaService(
            IAccesoPerfilRepository accesoPerfilRepository,
            IAccesoRepository accesoRepository,
            ICompaniaRepository companiaRepository,
            IHospitalRepository hospitalRepository,
            IPerfilRepository perfilRepository,
            IRolRepository rolRepository,

            CompaniaContactoService companiaContactoService,
            CompaniaValidatorService companiaValidatorService,
            CorreoHelper correoHelper,
            GiroComercialService giroComercialService,
            HospitalService hospitalService,
            UsuarioRolService usuarioRolService,
            UsuarioService usuarioService,

            IWebHostEnvironment env)
        {
            this.accesoPerfilRepository = accesoPerfilRepository;
            this.accesoRepository = accesoRepository;
            this.companiaRepository = companiaRepository;
            this.hospitalRepository = hospitalRepository;
            this.perfilRepository = perfilRepository;
            this.rolRepository = rolRepository;

            this.companiaContactoService = companiaContactoService;
            this.companiaValidatorService = companiaValidatorService;
            this.correoHelper = correoHelper;
            this.giroComercialService = giroComercialService;
            this.hospitalService = hospitalService;
            this.usuarioRolService = usuarioRolService;
            this.usuarioService = usuarioService;

            this.env = env;
        }

        public IEnumerable<CompaniaDto> ConsultarPorUsuarioPermiso(int idUsuario)
        {
            return companiaRepository.ConsultarPorUsuarioPermiso(idUsuario);
        }

        public IEnumerable<CompaniaDto> ConsultarGeneral()
        {
            return companiaRepository.ConsultarGeneral();
        }

        public IEnumerable<CompaniaSelectorDto> ConsultarTodosParaSelector()
        {
            return companiaRepository.ConsultarTodosParaSelector();
        }

        public IEnumerable<CompaniaDto> ConsultarTodosParaGrid(CompaniaFiltroDto filtro, string claveCompania)
        {
            return companiaRepository.ConsultarTodosParaGrid(filtro, claveCompania);
        }

        public CompaniaDto ConsultarDto(int idCompania)
        {
            return companiaRepository.ConsultarDto(idCompania);
        }

        public CompaniaDto ConsultarPorIdentificadorUrl(string empresa)
        {
            return companiaRepository.ConsultarPorIdentificadorUrl(empresa);
        }

        public async Task<int> Agregar(CompaniaDto companiaDto)
        {
            Compania compania = CrearCompaniaDtoModel(companiaDto);
            //AgrupadorCuentaContable agrupadorAtisc = agrupadorCuentaContableRepository.ConsultarPorClave(GeneralConstant.ClaveAgrupadorCuentaAtisc);

            compania.Clave = GenerarClave();
            compania.AfectacionContable = false;
            compania.Timbrado = false;
            compania.UsoAlmacen = false;
            //compania.IdAgrupadorCuentaContable = agrupadorAtisc.IdAgrupadorCuentaContable;

            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            companiaValidatorService.ValidarAgregar(compania);
            int idCompania = companiaRepository.Agregar(compania).IdCompania;

            if (companiaDto.CompaniaContacto != null)
            {
                CompaniaContacto companiaContacto = companiaDto.CompaniaContacto;
                companiaContacto.IdCompania = idCompania;
                companiaContactoService.Agregar(companiaContacto);
            }

            AgregarPerfilesDefault(compania);
            int idLocacion = AgregarLocacionDefault(compania);
            int idUsuario = await AgregarUsuarioDefault(compania, idLocacion, companiaDto.contrasenaUsuario);
            //int idAlmacen = AgregarAlmacenDefault(compania, idUsuario, idLocacion);
            //int idPuntoVenta = AgregarPuntoDeVentaDefault(idAlmacen, idUsuario);
            AgregarRolesDefaultAdministrador(idUsuario);
            //AgregarCuentasContables(compania);
            //AgregarConceptos(compania);
            //AgregarJerarquiasContabilidad(compania);
            //AgregarImpuestosDefault(compania.IdCompania);
            AgregarClientePublicoGeneral(compania, idLocacion);
            //AgregarFlujoDefault(idCompania);

            // if (companiaDto.CompaniaContacto != null)
            // {
            //     EnviarCorreo(compania, companiaDto.CompaniaContacto);
            // }

            scope.Complete();

            return idCompania;
        }

        private void AgregarClientePublicoGeneral(Compania compania, int idLocacion)
        {
            var usuario = new Usuario();

            usuario.Nombre = "Público";
            usuario.ApellidoPaterno = "en General";
            usuario.Habilitado = true;
            usuario.IdTipoUsuario = 1;
            usuario.IdCompania = compania.IdCompania;
            usuario.Rfc = GeneralConstant.RFCPublicoGeneral;
            usuario.IdHospital = idLocacion;

            int idUsuario = usuarioService.AgregarCliente(usuario);

            var rolCliente = new UsuarioRol();
            //var conceptoCuentaPorCobrar = conceptoRepository.ConsultarPorClave(GeneralConstant.ClaveConceptoCuentaPorCobrar, (int)compania.IdCompania);

            rolCliente.IdUsuario = idUsuario;
            rolCliente.IdRol = rolRepository.ConsultarPorClave(GeneralConstant.ClaveRolCliente).IdRol;

            //if (compania.AfectacionContable == true)
            //{
            //    rolCliente.IdConcepto = conceptoCuentaPorCobrar != null ? conceptoCuentaPorCobrar.IdConcepto : null;
            //    rolCliente.IdCuentaContable = conceptoCuentaPorCobrar != null ? conceptoCuentaPorCobrar.IdCuentaContable : null;
            //}

            usuarioRolService.Agregar(rolCliente);
        }

        private int AgregarLocacionDefault(Compania compania)
        {
            var locacion = new Hospital()
            {
                Nombre = "Hospital " + compania.Nombre,
                Calle = compania.Calle,
                NumeroExterior = compania.NumeroExterior,
                NumeroInterior = compania.NumeroInterior,
                Colonia = compania.Colonia,
                CodigoPostal = compania.CodigoPostal,
                Correo = compania.Correo,
                Telefono = compania.Telefono,
                IdEstado = compania.IdEstado,
                Rfc = compania.Rfc,
                IdRegimenFiscal = compania.IdRegimenFiscal,
                IdLada = compania.IdLada,
                PortalWeb = compania.PortalWeb,
                IdCompania = compania.IdCompania,
                IdMunicipio = compania.IdMunicipio,
                Cuenta = "000",
                Clabe = "000",
                Predeterminada = true
            };

            return hospitalService.Agregar(locacion);
        }

        private async Task<int> AgregarUsuarioDefault(Compania compania, int idLocacion, string contrasena)
        {
            Perfil perfilAdministrador = perfilRepository.ConsultarAdministradorPorTipoCompania((int)compania.IdTipoCompania, compania.IdCompania);

            if (perfilAdministrador == null)
            {
                throw new CdisException("El tipo de compañía seleccionado no tiene un perfil administrador");
            }

            var usuario = new UsuarioDto()
            {
                Nombre = "Administrador",
                ApellidoPaterno = compania.Nombre,
                Correo = compania.Correo,
                CorreoPersonal = compania.Correo,
                IdCompania = compania.IdCompania,
                IdHospital = idLocacion,
                IdEstado = compania.IdEstado,
                TelefonoMovil = compania.Telefono,
                Habilitado = true,
                IdTipoUsuario = 1,
                IdPerfil = perfilAdministrador != null ? perfilAdministrador.IdPerfil : null,
                Contrasena = contrasena,
                AdministradorCompania = true
            };

            return await usuarioService.Agregar(usuario, idLocacion);
        }

        //private int AgregarAlmacenDefault(Compania compania, int idUsuario, int idLocacion)
        //{
        //    //Almacén de producción
        //    var almacenProduccion = new Almacen();

        //    almacenProduccion.IdCompania = compania.IdCompania;
        //    almacenProduccion.Numero = "001";
        //    almacenProduccion.Nombre = "Almacén General";
        //    almacenProduccion.Descripcion = "Almacén General";
        //    almacenProduccion.Calle = compania.Calle;
        //    almacenProduccion.NumeroExterior = compania.NumeroExterior;
        //    almacenProduccion.NumeroInterior = compania.NumeroInterior;
        //    almacenProduccion.Colonia = compania.Colonia;
        //    almacenProduccion.Localidad = "Sin Especificar";
        //    almacenProduccion.CodigoPostal = compania.CodigoPostal;
        //    almacenProduccion.TelefonoUno = compania.Telefono;
        //    almacenProduccion.TelefonoDos = compania.Telefono;
        //    almacenProduccion.IdEstatusAlmacen = 1;
        //    almacenProduccion.IdUsuarioResponsable = idUsuario;
        //    almacenProduccion.IdEstado = (int)compania.IdEstado;
        //    almacenProduccion.FechaAlta = Utileria.ObtenerFechaActual();

        //    int idAlmacenProduccion = almacenService.Agregar(almacenProduccion);

        //    var accesoAlmacenProduccion = new UsuarioAlmacen();

        //    accesoAlmacenProduccion.IdUsuario = idUsuario;
        //    accesoAlmacenProduccion.IdAlmacen = idAlmacenProduccion;

        //    usuarioAlmacenService.Agregar(accesoAlmacenProduccion);

        //    //Almacén de producto caduco
        //    var almacenCaduco = new Almacen();

        //    almacenCaduco.IdCompania = compania.IdCompania;
        //    almacenCaduco.Numero = "002";
        //    almacenCaduco.Nombre = "Almacén Caduco";
        //    almacenCaduco.Descripcion = "Almacén Caduco";
        //    almacenCaduco.Calle = compania.Calle;
        //    almacenCaduco.NumeroExterior = compania.NumeroExterior;
        //    almacenCaduco.NumeroInterior = compania.NumeroInterior;
        //    almacenCaduco.Colonia = compania.Colonia;
        //    almacenCaduco.Localidad = "Sin Especificar";
        //    almacenCaduco.CodigoPostal = compania.CodigoPostal;
        //    almacenCaduco.TelefonoUno = compania.Telefono;
        //    almacenCaduco.TelefonoDos = compania.Telefono;
        //    almacenCaduco.IdEstatusAlmacen = 1;
        //    almacenCaduco.IdUsuarioResponsable = idUsuario;
        //    almacenCaduco.IdEstado = (int)compania.IdEstado;
        //    almacenCaduco.FechaAlta = Utileria.ObtenerFechaActual();

        //    int idAlmacenCaduco = almacenService.Agregar(almacenCaduco);

        //    var accesoAlmacenCaduco = new UsuarioAlmacen();

        //    accesoAlmacenCaduco.IdUsuario = idUsuario;
        //    accesoAlmacenCaduco.IdAlmacen = idAlmacenCaduco;

        //    usuarioAlmacenService.Agregar(accesoAlmacenCaduco);

        //    //Se asigna en almacén default a las áreas de almacen de la locación
        //    var locacion = hospitalRepository.Consultar(idLocacion);
        //    locacion.IdAlmacenProduccion = idAlmacenProduccion;
        //    locacion.IdAlmacenCaduco = idAlmacenCaduco;
        //    hospitalService.Editar(locacion);

        //    return idAlmacenProduccion;
        //}

        //private int AgregarPuntoDeVentaDefault(int idAlmacen, int idUsuario)
        //{
        //    var puntoVenta = new PuntoVenta();

        //    puntoVenta.Clave = "001";
        //    puntoVenta.Nombre = "Punto de Venta Principal";
        //    puntoVenta.Descripcion = "Punto de Venta Principal";
        //    puntoVenta.IdAlmacen = idAlmacen;
        //    puntoVenta.IdTipoPuntoVenta = 2;

        //    int idPuntoVenta = puntoVentaService.Agregar(puntoVenta);

        //    var usuario = usuarioService.Consultar(idUsuario);
        //    usuario.IdPuntoVenta = idPuntoVenta;
        //    usuarioService.Editar(usuario);

        //    // Se agrega una ubicacion de venta al punto de venta

        //    var ubicacionVenta = new UbicacionVenta();

        //    ubicacionVenta.IdPuntoVenta = idPuntoVenta;
        //    ubicacionVenta.Nombre = "Mostrador";
        //    ubicacionVenta.Clave = "001";

        //    ubicacionVentaRepository.Agregar(ubicacionVenta);

        //    // Se le pone la ubicacion de venta default al punto de venta

        //    puntoVenta.IdUbicacionVenta = ubicacionVenta.IdUbicacionVenta;
        //    puntoVentaService.Editar(puntoVenta);

        //    return idPuntoVenta;
        //}

        private void AgregarRolesDefaultAdministrador(int idUsuario)
        {
            var rolVendedor = new UsuarioRol();
            rolVendedor.IdUsuario = idUsuario;
            rolVendedor.IdRol = rolRepository.ConsultarPorClave(GeneralConstant.ClaveRolMedico).IdRol;

            usuarioRolService.Agregar(rolVendedor);

            var rolCajero = new UsuarioRol();
            rolCajero.IdUsuario = idUsuario;
            rolCajero.IdRol = rolRepository.ConsultarPorClave(GeneralConstant.ClaveRolCajero).IdRol;

            usuarioRolService.Agregar(rolCajero);

            var rolGestorFlujo = new UsuarioRol();
            rolGestorFlujo.IdUsuario = idUsuario;
            rolGestorFlujo.IdRol = rolRepository.ConsultarPorClave("015").IdRol;

            usuarioRolService.Agregar(rolGestorFlujo);
        }

        //private void AgregarCuentasContables(Compania compania)
        //{
        //    if (compania.IdAgrupadorCuentaContable > 0)
        //    {
        //        var cuentasAcopiar = cuentaContableRepository.ConsultarPorCompaniaBaseYAgrupador((int)compania.IdAgrupadorCuentaContable);

        //        foreach(var cuentaAcopiar in cuentasAcopiar)
        //        {
        //            cuentaAcopiar.IdCuentaContable = 0;
        //            cuentaAcopiar.IdCompania = compania.IdCompania;
        //            cuentaContableRepository.Agregar(cuentaAcopiar);
        //        }
        //    }
        //}

        //private void AgregarConceptos(Compania compania)
        //{
        //    var conceptosAcopiar = conceptoRepository.ConsultarPorCompaniaBase();

        //    foreach (var conceptoAcopiar in conceptosAcopiar)
        //    {
        //        conceptoAcopiar.IdConcepto = 0;
        //        conceptoAcopiar.IdCompania = compania.IdCompania;

        //        if (conceptoAcopiar.IdCuentaContable > 0)
        //        {
        //            string numero = conceptoAcopiar.IdCuentaContableNavigation.Numero;
        //            var cuentaContable = cuentaContableRepository.ConsultarPorNumero(compania.IdCompania, numero);

        //            if (cuentaContable != null)
        //            {
        //                conceptoAcopiar.IdCuentaContable = cuentaContable.IdCuentaContable;
        //            }
        //            else
        //            {
        //                conceptoAcopiar.IdCuentaContable = null;
        //            }

        //        }

        //        conceptoRepository.Agregar(conceptoAcopiar);
        //    }
        //}

        //private void AgregarJerarquiasContabilidad(Compania compania)
        //{
        //    List<TipoAuxiliar> auxiliaryTypeList = tipoAuxiliarRepository.ConsultarTodos().ToList();

        //    // Se agregan las jerarquias estandar
        //    foreach (TipoAuxiliar auxiliaryType in auxiliaryTypeList)
        //    {
        //        Jerarquia hierarchy = new Jerarquia();
        //        hierarchy.IdCompania = compania.IdCompania;
        //        hierarchy.Estandar = true;
        //        hierarchy.Nombre = "Estándar";
        //        hierarchy.InvertirSigno = false;
        //        hierarchy.IdTipoAuxiliar = auxiliaryType.IdTipoAuxiliar;

        //        if (auxiliaryType.Clave == GeneralConstant.TypeAuxiliaryCodeAccount)
        //        {
        //            var jerarquias = jerarquiaService.GetByAccountGroupingDefault((int)compania.IdAgrupadorCuentaContable);

        //            foreach(var jerarquia in jerarquias)
        //            {
        //                hierarchy = new Jerarquia();
        //                hierarchy = jerarquia;
        //                hierarchy.IdCompania = compania.IdCompania;

        //                jerarquiaService.AddByHierarchyBase(hierarchy, jerarquia.IdJerarquia);
        //            }

        //            continue;
        //        }

        //        jerarquiaService.Agregar(hierarchy);
        //    }

        //    // Se agrega la configuracion del mes contable actual
        //    var configurationMonth = new Configuracion();
        //    configurationMonth.Descripcion = GeneralConstant.ConfiguracionDescripcionMesContableActual;
        //    configurationMonth.Clave = GeneralConstant.ConfiguracionMesContableActual;
        //    configurationMonth.IdCompania = compania.IdCompania;
        //    configurationMonth.Valor = DateTime.Now.Month.ToString();
        //    configuracionRepository.Agregar(configurationMonth);

        //    // Se agrega la configuracion del anio contable actual
        //    var configurationYear = new Configuracion();
        //    configurationYear.Descripcion = GeneralConstant.ConfiguracionDescripcionAnioContableActual;
        //    configurationYear.Clave = GeneralConstant.ConfiguracionAnioContableActual;
        //    configurationYear.IdCompania = compania.IdCompania;
        //    configurationYear.Valor = DateTime.Now.Year.ToString();
        //    configuracionRepository.Agregar(configurationYear);
        //}

        //public void AgregarImpuestosDefault(int idCompania)
        //{
        //    // Se agrega configuracion de impuesto IVA 16%
        //    var impuestoIva = new Impuesto();
        //    impuestoIva.PorcentajeNeto = 16;
        //    impuestoIva.Clave = "001";
        //    impuestoIva.Nombre = "IVA 16%";
        //    impuestoIva.IdCompania = idCompania;

        //    impuestoRepository.Agregar(impuestoIva);

        //    var detalleIva16 = new ImpuestoDetalle();
        //    detalleIva16.Descripcion = "IVA";
        //    detalleIva16.IdImpuesto = impuestoIva.IdImpuesto;
        //    detalleIva16.IdTipoImpuesto = 2;
        //    detalleIva16.Valor = 16;

        //    impuestoDetalleRepository.Agregar(detalleIva16);

        //    // Sin Impuestos
        //    var sinImpuesto = new Impuesto();
        //    sinImpuesto.PorcentajeNeto = 0;
        //    sinImpuesto.Clave = "002";
        //    sinImpuesto.Nombre = "Sin Impuestos";
        //    sinImpuesto.IdCompania = idCompania;

        //    impuestoRepository.Agregar(sinImpuesto);
        //}

        private void AgregarPerfilesDefault(Compania compania)
        {
            var companiaBase = companiaRepository.ConsultarPorClave(GeneralConstant.ClaveCompaniaBase);

            //Se consultas los perfiles de la compañía base, de acuerdo al tipo de compañía
            var perfiles = perfilRepository.ConsultarPorCompaniaBase((int)companiaBase.IdCompania).ToList();

            //Se copia el perfil "Sin Acceso" de la compañía base, por default
            var perfilSinAcceso = perfilRepository.ConsultarPorClave(GeneralConstant.ClavePerfilSinAcceso, companiaBase.IdCompania);
            if (perfilSinAcceso != null)
            {
                perfiles.Add(perfilSinAcceso);
            }

            foreach (var perfil in perfiles)
            {
                var accesos = accesoRepository.ConsultarPorPerfil(perfil.IdPerfil);

                perfil.IdPerfil = 0;
                perfil.IdCompania = compania.IdCompania;
                perfil.IdTipoCompania = compania.IdTipoCompania;

                perfilRepository.Agregar(perfil);

                foreach (var acceso in accesos)
                {
                    var accesoPerfil = new AccesoPerfil();
                    accesoPerfil.IdPerfil = perfil.IdPerfil;
                    accesoPerfil.IdAcceso = acceso.IdAcceso;

                    accesoPerfilRepository.Agregar(accesoPerfil);
                }
            }
        }

        //private void AgregarFlujoDefault(int idCompania)
        //{
        //    var rolGestorFlujos = rolRepository.ConsultarPorClave(GeneralConstant.ClaveRolGestorFlujos);
        //    var tipoFlujoDefault = tipoFlujoRepository.ConsultarPorClave(GeneralConstant.ClaveTipoFlujoDefault);

        //    Flujo flujoDefault = new()
        //    {
        //        Clave = "001",
        //        Nombre = "Estándar",
        //        IdCompania = idCompania,
        //        EsDefault = true,
        //        IdTipoFlujo = tipoFlujoDefault?.IdTipoFlujo,
        //        IdRol = rolGestorFlujos?.IdRol
        //    };

        //    flujoService.Agregar(flujoDefault);
        //}

        private Compania CrearCompaniaDtoModel(CompaniaDto companiaDto)
        {
            Compania compania = new Compania
            {
                IdCompania = companiaDto.IdCompania,
                Clave = companiaDto.Clave,
                Nombre = companiaDto.Nombre,
                Correo = companiaDto.Correo,
                PortalWeb = companiaDto.PortalWeb,
                Calle = companiaDto.Calle,
                NumeroExterior = companiaDto.NumeroExterior,
                NumeroInterior = companiaDto.NumeroInterior,
                Colonia = companiaDto.Colonia,
                CodigoPostal = companiaDto.CodigoPostal,
                Telefono = companiaDto.Telefono,
                Ciudad = companiaDto.Ciudad,
                IdEstado = companiaDto.IdEstado,
                IdMunicipio = companiaDto.IdMunicipio,
                Rfc = companiaDto.Rfc,
                IdLada = companiaDto.IdLada,
                IdRegimenFiscal = companiaDto.IdRegimenFiscal,
                IdMoneda = companiaDto.IdMoneda,
                IdAgrupadorCuentaContable = companiaDto.IdAgrupadorCuentaContable,
                IdTipoCompania = companiaDto.IdTipoCompania,
                Timbrado = companiaDto.Timbrado,
                IdGiroComercial = companiaDto.IdGiroComercial
            };
            return compania;
        }

        public void Editar(Compania companiaFormulario)
        {
            //var compania = companiaRepository.Consultar(companiaFormulario.IdCompania);

            //if (compania.IdGiroComercial != companiaFormulario.IdGiroComercial && compania.MercadoCompania.Any())
            //{
            //    var mercadoCompania = compania.MercadoCompania.FirstOrDefault();
            //    var mercado = mercadoRepository.Consultar(mercadoCompania.IdMercado);

            //    throw new CdisException($"Para cambiar el giro comercial de la compañía, primero se debe eliminar del mercado {mercado.Nombre}");
            //}

            companiaValidatorService.ValidarEditar(companiaFormulario);
            companiaRepository.Editar(companiaFormulario);
        }

        public void Eliminar(int idCompania)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var compania = companiaRepository.Consultar(idCompania);

                companiaRepository.Eliminar(compania);

                scope.Complete();
            }
        }

        private string GenerarClave()
        {
            var ultimaCompania = companiaRepository.ConsultarUltimaAgregada();

            if (ultimaCompania == null)
            {
                return "001";
            }

            int ultimoConsecutivo = Int32.Parse(ultimaCompania.Clave);
            int consecutivo = ultimoConsecutivo + 1;

            return consecutivo.ToString("D3");
        }

        private void EnviarCorreo(Compania compania, CompaniaContacto companiaContacto)
        {
            Compania companiaBase = companiaRepository.ConsultarPorClave(GeneralConstant.ClaveCompaniaBase);

            GiroComercial giroComercial = giroComercialService.Consultar((int)compania.IdGiroComercial);

            try
            {
                var correo = new Correo
                {
                    Receptor = companiaBase.Correo,
                    Asunto = "Registro de Compañía ATI (Login - Cliente)",
                    Mensaje = $@"
                        <hr style='border: none; border-bottom: 1px #2e75b6 solid; margin: 20px 0;'>
                        <b>Notificación de creación de compañía en el sistema ATI:</b>
                        <table>
                            <tr>
                                <td style='padding-right: 12px;'>Compañía</td>
                                <td>{compania.Nombre} - {compania.Clave}</td>
                            </tr>
                            <tr>
                                <td style='padding-right: 12px;'>Giro Comercial</td>
                                <td>{giroComercial.Nombre}</td>
                            </tr>
                            <tr>
                                <td style='padding-right: 12px;'>Contacto</td>
                                <td>{companiaContacto.Nombre} {companiaContacto.ApellidoPaterno} {companiaContacto.ApellidoMaterno}</td>
                            </tr>
                            <tr>
                                <td style='padding-right: 12px;'>Correo Electrónico</td>
                                <td>{companiaContacto.Correo}</td>
                            </tr>
                            <tr>
                                <td style='padding-right: 12px;'>Teléfono</td>
                                <td>{companiaContacto.TelefonoMovil}</td>
                            </tr>
                        </table>",
                    EsMensajeHtml = true
                };

                correoHelper.Enviar(correo);
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex, env);
            }
        }
    }
}
