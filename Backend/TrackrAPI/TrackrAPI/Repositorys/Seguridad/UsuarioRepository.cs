using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.Seguridad;
using System;
using TrackrAPI.Dtos.Perfil;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<UsuarioDto> ConsultarPorTipoUsuario(string claveTipoUsuario, int idCompania)
        {
            return
                context.Usuario
                .Where(u => u.IdTipoUsuarioNavigation.Clave == claveTipoUsuario
                                    && u.Habilitado == true
                                    && u.UsuarioLocacion.Any(ul => ul.IdLocacionNavigation.IdCompania == idCompania))
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    ApellidoPaterno = u.ApellidoPaterno,
                    ApellidoMaterno = u.ApellidoMaterno,
                    Correo = u.Correo,
                    CorreoPersonal = u.CorreoPersonal,
                    Calle = u.Calle,
                    Cedula = u.Cedula,
                    Ciudad = u.Ciudad,
                    CodigoPostal = u.CodigoPostal,
                    Colonia = u.Colonia,
                    Habilitado = u.Habilitado,
                    IdCompania = u.IdCompania,
                    IdDepartamento = u.IdDepartamento,
                    IdEstado = u.IdEstado,
                    IdHospital = u.IdHospital,
                    IdPerfil = u.IdPerfil,
                    IdPuntoVenta = u.IdPuntoVenta,
                    IdTipoUsuario = u.IdTipoUsuario,
                    IdTituloAcademico = u.IdTituloAcademico,
                    ImagenTipoMime = u.ImagenTipoMime,
                    NumeroExterior = u.NumeroExterior,
                    NumeroInterior = u.NumeroInterior,
                    TelefonoMovil = u.TelefonoMovil,
                    Username = u.Username,
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    IdPais = u.IdEstadoNavigation.IdPais,
                    NombrePerfil = u.IdPerfilNavigation.Nombre,
                    NumeroLicencia = u.NumeroLicencia
                })
                .ToList();
        }

        public Usuario LoginAdministrador(string usuario, string contrasena)
        {
            var usuarioConsulta =
                from u in context.Usuario
                where u.Correo == usuario
                && u.Contrasena == contrasena
                && u.Habilitado == true
                select u;
            return usuarioConsulta.FirstOrDefault();
        }

        public Usuario Login(string correo, string contrasena, string claveRol)
        {
            var usuarioConsulta =
                from u in context.Usuario
                .Include(u => u.UsuarioRol)
                    .ThenInclude(ur => ur.IdRolNavigation)
                where u.Correo == correo
                && u.Contrasena == contrasena
                // TODO: 2023-05-03 -> Agregar el filtro por rol
                // && (claveRol == null || u.UsuarioRol.Any(ur => ur.IdRolNavigation.Clave == claveRol))
                && u.Habilitado == true
                select u;
            return usuarioConsulta.FirstOrDefault();
        }

        public Usuario VerificarContrasena(int idUsuario, string contrasena)
        {
            var usuarioConsulta =
                from u in context.Usuario
                where u.IdUsuario == idUsuario
                && u.Contrasena == contrasena
                select u;
            return usuarioConsulta.FirstOrDefault();
        }

        public IEnumerable<UsuarioGridDto> ConsultarGeneral(int idCompania)
        {
            return
                context.Usuario
                .Include(u => u.UsuarioRol).ThenInclude(u => u.IdRolNavigation)
                .Include(u => u.IdCompaniaNavigation)
                .Where(u => u.UsuarioLocacion.Any(ul => ul.IdLocacionNavigation.IdCompania == idCompania) || u.IdCompania == idCompania)
                .Select(u => new UsuarioGridDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    ApellidoPaterno = u.ApellidoPaterno,
                    ApellidoMaterno = u.ApellidoMaterno,
                    Correo = u.Correo,
                    CorreoPersonal = u.CorreoPersonal,
                    Calle = u.Calle,
                    Cedula = u.Cedula,
                    Ciudad = u.Ciudad,
                    CodigoPostal = u.CodigoPostal,
                    Colonia = u.Colonia,
                    Habilitado = u.Habilitado,
                    IdCompania = u.IdCompania,
                    IdDepartamento = u.IdDepartamento,
                    IdEstado = u.IdEstado,
                    IdHospital = u.IdHospital,
                    IdPerfil = u.IdPerfil,
                    IdPuntoVenta = u.IdPuntoVenta,
                    IdTipoUsuario = u.IdTipoUsuario,
                    IdTituloAcademico = u.IdTituloAcademico,
                    ImagenTipoMime = u.ImagenTipoMime,
                    NumeroExterior = u.NumeroExterior,
                    NumeroInterior = u.NumeroInterior,
                    TelefonoMovil = u.TelefonoMovil,
                    Username = u.Username,
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    IdPais = u.IdEstadoNavigation.IdPais,
                    NombrePerfil = u.IdPerfilNavigation.Nombre,
                    NombreTipoUsuario = u.IdTipoUsuarioNavigation.Nombre,
                    Roles = u.UsuarioRol.ObtenerRoles(),
                    NombreCompania = u.IdCompaniaNavigation.Nombre,
                })
                .ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarPorRol(string claveRol, int idCompania)
        {
            return
                context.Usuario
                .Where(u => u.UsuarioRol.Any(ur => ur.IdRolNavigation.Clave == claveRol)
                    && (u.UsuarioLocacion.Any(ul => ul.IdLocacionNavigation.IdCompania == idCompania) || u.IdCompania == idCompania))
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    ApellidoPaterno = u.ApellidoPaterno,
                    ApellidoMaterno = u.ApellidoMaterno,
                    Correo = u.Correo,
                    CorreoPersonal = u.CorreoPersonal,
                    Calle = u.Calle,
                    Cedula = u.Cedula,
                    Ciudad = u.Ciudad,
                    CodigoPostal = u.CodigoPostal,
                    Colonia = u.Colonia,
                    Habilitado = u.Habilitado,
                    IdCompania = u.IdCompania,
                    IdDepartamento = u.IdDepartamento,
                    IdEstado = u.IdEstado,
                    IdHospital = u.IdHospital,
                    IdPerfil = u.IdPerfil,
                    IdPuntoVenta = u.IdPuntoVenta,
                    IdTipoUsuario = u.IdTipoUsuario,
                    IdTituloAcademico = u.IdTituloAcademico,
                    ImagenTipoMime = u.ImagenTipoMime,
                    NumeroExterior = u.NumeroExterior,
                    NumeroInterior = u.NumeroInterior,
                    TelefonoMovil = u.TelefonoMovil,
                    Username = u.Username,
                    NombreCompleto = u.ObtenerNombreCompleto() + (u.IdTipoUsuarioNavigation.Clave == GeneralConstant.ClaveTipoUsuarioMedicoExterno
                                                  ? (" (" + u.IdTipoUsuarioNavigation.Nombre + ")") : ""),
                    IdPais = u.IdEstadoNavigation.IdPais,
                    NombrePerfil = u.IdPerfilNavigation.Nombre,
                    Rfc = u.Rfc
                })
                .ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarClinicosActivos(string claveTipoUsuario, int idHospital)
        {
            return
                context.Usuario
                .Where(u => u.UsuarioRol.Any(ur => ur.IdRolNavigation.Clave == claveTipoUsuario)
                                        && u.EntradaPersonalIdUsuarioNavigation.Any(ep => ep.IdHospital == idHospital && ep.Habilitado))
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreCompleto = u.ObtenerNombreCompleto(),
                })
                .ToList();
        }

        public UsuarioDto ConsultarDto(int idUsuario)
        {
            return
                context.Usuario
                .Where(u => u.IdUsuario == idUsuario)
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    ApellidoPaterno = u.ApellidoPaterno,
                    ApellidoMaterno = u.ApellidoMaterno,
                    Correo = u.Correo,
                    CorreoPersonal = u.CorreoPersonal,
                    Calle = u.Calle,
                    Cedula = u.Cedula,
                    Ciudad = u.IdMunicipioNavigation.Nombre,
                    CodigoPostal = u.CodigoPostal,
                    Colonia = u.IdColoniaNavigation.Nombre,
                    Habilitado = u.Habilitado,
                    IdCompania = u.IdCompania,
                    IdDepartamento = u.IdDepartamento,
                    IdColonia = u.IdColonia,
                    IdMunicipio = u.IdMunicipio,
                    IdLocalidad = u.IdLocalidad,
                    IdEstado = u.IdEstado,
                    IdHospital = u.IdHospital,
                    IdPerfil = u.IdPerfil,
                    IdPuntoVenta = u.IdPuntoVenta,
                    IdTipoUsuario = u.IdTipoUsuario,
                    IdTituloAcademico = u.IdTituloAcademico,
                    ImagenTipoMime = u.ImagenTipoMime,
                    NumeroExterior = u.NumeroExterior,
                    NumeroInterior = u.NumeroInterior,
                    TelefonoMovil = u.TelefonoMovil,
                    Username = u.Username,
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    IdPais = u.IdEstadoNavigation.IdPais,
                    NombrePerfil = u.IdPerfilNavigation.Nombre,
                    Rfc = u.Rfc,
                    SueldoDiario = u.SueldoDiario,
                    IdArea = u.IdArea,
                    ClaveTipoUsuario = u.IdTipoUsuarioNavigation.Clave,
                    IdExpediente = u.Expediente.FirstOrDefault().IdExpediente,
                    IdRegimenFiscal = u.IdRegimenFiscal,
                    NumeroLicencia = u.NumeroLicencia,
                    DiasPago = u.DiasPago != null
                        ? (int)u.DiasPago
                        : 0,
                    IdListaPrecio = u.IdListaPrecio,
                    IdTipoCliente = u.IdTipoCliente,
                    IdsRol = u.UsuarioRol.Select(ur => ur.IdRol).ToList(),
                    IdMetodoPago = u.IdMetodoPago,
                    IdSatFormaPago = u.IdSatFormaPago
                })
                .FirstOrDefault();
        }

        public Usuario Consultar(int idUsuario)
        {
            var usuario = from u
                          in context.Usuario
                            .Include(u => u.IdTipoUsuarioNavigation)
                            .Include(u => u.IdEstadoNavigation)
                            .Include(u => u.IdPerfilNavigation)
                            .Include(u => u.IdTituloAcademicoNavigation)
                            .Include(u => u.UsuarioRol)
                                .ThenInclude(u => u.IdRolNavigation)
                            .Include(u => u.IdHospitalNavigation)
                            .Include(u => u.EntradaPersonalIdUsuarioNavigation)
                            .Include(u => u.IdCompaniaNavigation)
                            .Include(u => u.IdRegimenFiscalNavigation)
                          where u.IdUsuario == idUsuario
                          select u;
            return usuario.FirstOrDefault();
        }

        public UsuarioEncabezadoDto ConsultarEncabezado(int idUsuario)
        {

            return context.Usuario
                .Where(u => u.IdUsuario == idUsuario)
                .Select(u => new UsuarioEncabezadoDto {
                    Nombre = u.IdTituloAcademicoNavigation != null
                        ? u.IdTituloAcademicoNavigation.Nombre + " " + u.ObtenerNombreCompleto()
                        : u.ObtenerNombreCompleto(),
                    Imagen = "archivo/usuario/" + u.IdUsuario,
                    ImagenLogotipo = "archivo/HospitalLogotipo/" + (u.IdHospitalNavigation.HospitalLogotipo.Any()
                        ? u.IdHospitalNavigation.HospitalLogotipo.First().IdHospitalLogotipo.ToString()
                        : "0")
                })
                .FirstOrDefault();
        }

        public UsuarioEncabezadoDto ConsultarEncabezado(int idUsuario, string empresa)
        {
            return
                context.Usuario
                .Where(u => u.IdUsuario == idUsuario && u.IdCompaniaNavigation.Clave == empresa)
                .Select(u => new UsuarioEncabezadoDto
                {
                    Nombre = u.ObtenerNombreCompleto(),
                    Imagen = "archivo/usuario/" + u.IdUsuario
                })
                .FirstOrDefault();
        }

        public Usuario ConsultarPorCorreo(string correo, string claveTipoUsuario)
        {
            var usuario =
                from u in context.Usuario
                where u.Correo == correo
                && u.IdTipoUsuarioNavigation.Clave == claveTipoUsuario
                select u;
            return usuario.FirstOrDefault();
        }

        public Usuario ConsultarPorCorreo(string correo)
        {
            var usuario =
                from u in context.Usuario
                where u.Correo == correo
                select u;
            return usuario.FirstOrDefault();
        }

        public Usuario ConsultarPorUsuario(string username)
        {
            var usuario =
                from u in context.Usuario
                where u.Username == username
                select u;
            return usuario.FirstOrDefault();
        }

        public IEnumerable<UsuarioDto> ConsultarPorRolActivosParaSelector(int rol, int idCompania, int idHospital)
        {

            return context.Usuario
                      .Include(u => u.EntradaPersonalIdUsuarioNavigation)
                      .Where(e => e.UsuarioRol.Where(u => u.IdRol == rol).Count() > 0
                      && e.UsuarioLocacion.Any(ul => ul.IdLocacionNavigation.IdCompania == idCompania)
                      && e.EntradaPersonalIdUsuarioNavigation.Any(ep => ep.IdHospital == idHospital && ep.Habilitado == true))
                      .Select(e => new UsuarioDto
                      {
                          IdUsuario = e.IdUsuario,
                          NombreCompleto = e.ObtenerNombreCompleto()
                      }
                      ).ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelector(int rol, int idCompania)
        {
            return context.Usuario
                      .Include(u => u.UsuarioRol)
                      .Include(u => u.UsuarioLocacion)
                      .Where(e => e.UsuarioRol.Where(u => u.IdRol == rol).Count() > 0 &&
                            ((e.UsuarioLocacion.Where(ul => ul.IdLocacionNavigation.IdCompania == idCompania).Count() > 0) || e.IdCompania == idCompania))
                      .Select(e => new UsuarioDto
                      {
                          IdUsuario = e.IdUsuario,
                          NombreCompleto = e.ObtenerNombreCompleto(),
                          Rfc = e.Rfc
                      }
                      ).ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelectorDomicilio(int rol, int idCompania)
        {
            return context.Usuario
                      .Where(e => e.UsuarioRol.Where(u => u.IdRol == rol).Count() > 0 &&
                             e.IdCompania == idCompania)
                      .Select(e => new UsuarioDto
                      {
                          IdUsuario = e.IdUsuario,
                          NombreCompleto = e.ObtenerNombreCompleto(),
                          Rfc = e.Rfc
                      }
                      ).ToList();
        }

        public bool TieneRol(string claveRol, int idUsuario)
        {
            return context.UsuarioRol
                .Any(ur => ur.IdUsuario == idUsuario && ur.IdRolNavigation.Clave == claveRol);
        }

        public IEnumerable<UsuarioDto> ConsultarUsuariosParaRegistrarEntrada(int idHospital)
        {
            return context.Usuario
                .Where(u => u.IdHospital == idHospital && !u.EntradaPersonalIdUsuarioNavigation.Any(ep => ep.IdUsuario == u.IdUsuario && ep.Habilitado))
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreCompleto = u.ObtenerNombreCompleto()
                })
                .ToList();
        }

        public UsuarioDto ConsultarMedico(string cedula)
        {
            return
                context.Usuario
                .Where(u => u.Cedula.ToLower().Equals(cedula.ToLower()))
                .Select(u => new UsuarioDto
                {
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    Direccion = u.Direccion,
                    IdUsuario = u.IdUsuario
                })
                .ToList().FirstOrDefault();
        }
        public UsuarioDto ConsultarMedico(int idUsuario)
        {
            return
                context.Usuario
                .Where(u => u.IdUsuario == idUsuario)
                .Select(u => new UsuarioDto
                {
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    Direccion = u.Direccion,
                    IdUsuario = u.IdUsuario
                })
                .ToList().FirstOrDefault();
        }
        public IEnumerable<UsuarioGridDto> ConsultarBusquedaGridFiltro(UsuarioDto filtro)
        {
            return
                context.Usuario
                .Include(u => u.UsuarioRol).ThenInclude(u => u.IdRolNavigation)
                .Include(u => u.IdCompaniaNavigation)
                .Where(u => (u.IdTipoUsuario == filtro.IdTipoUsuario || filtro.IdTipoUsuario == 0 || filtro.IdTipoUsuario == null) &&
                            (u.IdPerfil == filtro.IdPerfil || filtro.IdPerfil == null || filtro.IdPerfil == 0) &&
                            ((u.Nombre +
                                " " + u.ApellidoPaterno +
                                " " + u.ApellidoMaterno).ToLower().Contains((filtro.Nombre ?? "").ToLower()) || string.IsNullOrEmpty(filtro.Nombre) || filtro.Nombre == null) &&
                            (u.Correo.ToLower().Contains(filtro.Correo ?? "") || string.IsNullOrEmpty(filtro.Correo) || filtro.Correo == null) &&
                            ((u.TelefonoMovil.ToLower().Contains(filtro.TelefonoMovil ?? "")) || string.IsNullOrEmpty(filtro.TelefonoMovil) || filtro.TelefonoMovil == null) &&
                            (filtro.IdsRol == null || filtro.IdsRol.Count() == 0 || u.UsuarioRol.Any(ur => filtro.IdsRol.Contains(ur.IdRol))) &&
                            (filtro.IdsCompania == null || filtro.IdsCompania.Count() == 0 || filtro.IdsCompania.Contains((int)u.IdCompania)))
                .Select(u => new UsuarioGridDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    ApellidoPaterno = u.ApellidoPaterno,
                    ApellidoMaterno = u.ApellidoMaterno,
                    Correo = u.Correo,
                    CorreoPersonal = u.CorreoPersonal,
                    Calle = u.Calle,
                    Cedula = u.Cedula,
                    Ciudad = u.Ciudad,
                    CodigoPostal = u.CodigoPostal,
                    Colonia = u.Colonia,
                    Habilitado = u.Habilitado,
                    IdCompania = u.IdCompania,
                    IdDepartamento = u.IdDepartamento,
                    IdEstado = u.IdEstado,
                    IdHospital = u.IdHospital,
                    IdPerfil = u.IdPerfil,
                    IdPuntoVenta = u.IdPuntoVenta,
                    IdTipoUsuario = u.IdTipoUsuario,
                    IdTituloAcademico = u.IdTituloAcademico,
                    ImagenTipoMime = u.ImagenTipoMime,
                    NumeroExterior = u.NumeroExterior,
                    NumeroInterior = u.NumeroInterior,
                    TelefonoMovil = u.TelefonoMovil,
                    Username = u.Username,
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    IdPais = u.IdEstadoNavigation.IdPais,
                    NombrePerfil = u.IdPerfilNavigation.Nombre,
                    NombreTipoUsuario = u.IdTipoUsuarioNavigation.Nombre,
                    Roles = u.UsuarioRol.ObtenerRoles(),
                    NombreCompania = u.IdCompaniaNavigation.Nombre,
                })
                .ToList();
        }
        public Usuario ConsultarPorRFC(string rfc)
        {
            var usuario =
                from u in context.Usuario
                where u.Rfc == rfc
                select u;
            return usuario.FirstOrDefault();
        }

        public Usuario ConsultarPublicoEnGeneral(int idCompania)
        {
            var usuario =
                from u in context.Usuario
                where u.IdCompania == idCompania && u.Rfc == GeneralConstant.RFCPublicoGeneral
                select u;
            return usuario.FirstOrDefault();
        }

        public Usuario ConsultarPorNotaVentaDetalle(int idNotaVentaDetalle)
        {
            var usuario =
                from nvd in context.NotaVentaDetalle
                where nvd.IdNotaVentaDetalle == idNotaVentaDetalle
                select nvd.IdNotaVentaNavigation.IdUsuarioClienteNavigation;
            return usuario.FirstOrDefault();
        }

        public IEnumerable<UsuarioDto> ConsultarParaPuntoVenta(int idCompania)
        {
            return context.Usuario
                      .Where(e => e.IdCompania == idCompania)
                      .Where(e => (e.UsuarioRol.Where(u => u.IdRolNavigation.Clave == GeneralConstant.ClaveRolProveedor
                      || u.IdRolNavigation.Clave == GeneralConstant.ClaveRolCliente).Count() > 0
                      && e.IdCompania == idCompania)
                      || e.Rfc == GeneralConstant.RFCPublicoGeneral)
                      .Select(e => new UsuarioDto
                      {
                          IdUsuario = e.IdUsuario,
                          NombreCompleto = e.ObtenerNombreCompleto(),
                          Rfc = e.Rfc,
                      }
                      ).ToList();
        }

        public IEnumerable<UsuarioDto> ConsultarPorLocacionParaSelector(int idHospital)
        {
            return context.Usuario
                        .Where(u => u.Habilitado == true &&
                                    u.UsuarioLocacion.Any(ul => ul.IdLocacion == idHospital))
                        .Select(u => new UsuarioDto
                        {
                            IdUsuario = u.IdUsuario,
                            NombreCompleto = u.ObtenerNombreCompleto()
                        }).ToList();
        }

        public IEnumerable<UsuarioSelectorDto> ConsultarParaSelector()
        {
            return context.Usuario
                .Select(u => new UsuarioSelectorDto
                {
                    IdUsuario = u.IdUsuario,
                    RFC = u.Rfc,
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    SelectorLabel = u.Rfc + " - " + u.ObtenerNombreCompleto()
                }).ToList();
        }

        public IEnumerable<Usuario> ConsultarPorCompania(int idCompania)
        {
            return context.Usuario
                .Where(u => u.IdHospital == idCompania);
        }

        public IEnumerable<Usuario> ConsultarParaReporteProductividad(int idCompania)
        {
            return context.Usuario
                .Include(u => u.UsuarioRol)
                .Include(u => u.Comision)
                    .ThenInclude(c => c.IdNotaVentaDetalleNavigation)
                .Include(u => u.EntradaPersonalIdUsuarioNavigation)
                .Where(u => u.IdHospital == idCompania);
        }

        public Usuario ConsultarDependencias(int idUsuario)
        {
            return context.Usuario
                .Include(u => u.OrdenCompraIdUsuarioProveedorNavigation)
                    .ThenInclude(oc => oc.IdEstatusOrdenCompraNavigation)
                .Where(u => u.IdUsuario == idUsuario)
                .FirstOrDefault();
        }

        public IEnumerable<UsuarioDto> ConsultarPorNombre(string filtro)
        {
            return context.Usuario
                .Where(u => (u.Nombre +
                                " " + u.ApellidoPaterno +
                                " " + u.ApellidoMaterno).ToLower().Contains((filtro ?? "").ToLower()) || string.IsNullOrEmpty(filtro) || filtro == null)
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreCompleto = u.ObtenerNombreCompleto(),
                    Correo = u.Correo
                })
                .ToList();
        }

        public InformacionGeneralDTO ConsultarInformacionGeneralTrackr(int idUsuario)
        {
            var usuario = context.Usuario.
                Where(u => u.IdUsuario == idUsuario)
                    .Include(u=> u.ExpedienteTrackr)
                    .Include(u => u.ExpedientePadecimiento)
                    .FirstOrDefault();

            var expediente = usuario.ExpedienteTrackr.FirstOrDefault();

            var padecimientos = expediente.ExpedientePadecimiento;

            var informacionGeneral = new InformacionGeneralDTO
            {
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                FechaNacimiento = expediente.FechaNacimiento,
                IdGenero = expediente.IdGenero,
                Peso = expediente.Peso,
                Cintura = expediente.Cintura,
                Estatura = expediente.Estatura,
                Correo = usuario.Correo,
                TelefonoMovil = usuario.TelefonoMovil,
                IdPais = 1,
                IdEstado = usuario.IdEstado,
                IdMunicipio = usuario.IdMunicipio,
                IdLocalidad = usuario.IdLocalidad,
                IdColonia = usuario.IdColonia,
                CodigoPostal = usuario.CodigoPostal,
                Calle = usuario.Calle,
                NumeroInterior = usuario.NumeroInterior,
                NumeroExterior = usuario.NumeroExterior,
                padecimientos = padecimientos.Select(p => new PadecimientoDTO
                {
                    IdPadecimiento = p.IdPadecimiento,
                    IdExpedientePadecimiento = p.IdExpedientePadecimiento,
                    NombrePadecimiento = p.IdPadecimientoNavigation.Nombre,
                    NombreDoctor = p.IdUsuarioDoctorNavigation.Nombre,
                    FechaDiagnostico = p.FechaDiagnostico
                })
            };

            return informacionGeneral;

        }
    }
}
