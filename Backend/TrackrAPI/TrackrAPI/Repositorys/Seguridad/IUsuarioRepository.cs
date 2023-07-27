
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario LoginAdministrador(string usuario, string contrasena);
        public Usuario Login(string correo, string contrasena, string claveRol);
        public Usuario ConsultarPorCorreo(string correo, string claveTipoUsuario);
        public Usuario ConsultarPorCorreo(string correo);
        public Usuario ConsultarPorUsuario(string usuario);
        public Usuario Consultar(int idUsuario);
        public UsuarioDto ConsultarDto(int idUsuario);
        public UsuarioEncabezadoDto ConsultarEncabezado(int idUsuario);
        public UsuarioEncabezadoDto ConsultarEncabezado(int idUsuario, string empresa);
        public IEnumerable<UsuarioDto> ConsultarPorTipoUsuario(string claveTipoUsuario, int idCompania);
        public IEnumerable<UsuarioGridDto> ConsultarGeneral(int idCompania);
        public IEnumerable<UsuarioDto> ConsultarPorRol(string claveRol, int idCompania);
        public IEnumerable<UsuarioDto> ConsultarPorRolActivosParaSelector(int rol, int idCompania, int idHospital);
        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelector(int rol, int idCompania);
        public bool TieneRol(string claveRol, int idUsuario);
        public IEnumerable<UsuarioDto> ConsultarUsuariosParaRegistrarEntrada(int idHospital);
        public Usuario VerificarContrasena(int idUsuario, string contrasena);
        public IEnumerable<UsuarioDto> ConsultarClinicosActivos(string claveTipoUsuario, int idHospital);
        public UsuarioDto ConsultarMedico(string cedula);
        public UsuarioDto ConsultarMedico(int idUsuario);
        public IEnumerable<UsuarioGridDto> ConsultarBusquedaGridFiltro(UsuarioDto filtro);
        public Usuario ConsultarPorRFC(string rfc);
        public Usuario ConsultarPublicoEnGeneral(int idCompania);
        public Usuario ConsultarPorNotaVentaDetalle(int idNotaVentaDetalle);
        public IEnumerable<UsuarioDto> ConsultarParaPuntoVenta(int idCompania);
        public IEnumerable<UsuarioDto> ConsultarPorLocacionParaSelector(int idHospital);
        public IEnumerable<UsuarioSelectorDto> ConsultarParaSelector();
        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelectorDomicilio(int rol, int idCompania);
        public IEnumerable<Usuario> ConsultarPorCompania(int idCompania);
        public IEnumerable<Usuario> ConsultarParaReporteProductividad(int idCompania);
        public Usuario ConsultarDependencias(int idUsuario);
        public IEnumerable<UsuarioDto> ConsultarPorNombre(string filtro);
    }
}
