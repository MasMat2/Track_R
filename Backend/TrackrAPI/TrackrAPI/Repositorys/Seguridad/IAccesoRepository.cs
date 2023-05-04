using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IAccesoRepository : IRepository<Acceso>
    {
        AccesoDto ConsultarDto(int idAcceso);
        Acceso Consultar(int idAcceso);
        Acceso ConsultarDependencia(int idAcceso);
        Acceso ConsultarPorClave(string clave);
        IEnumerable<AccesoGridDto> ConsultarGeneral();
        IEnumerable<AccesoMenuDto> ConsultarHijosPorUsuario(int idUsuario, int idAccesoPadre);
        IEnumerable<AccesoMenuDto> ConsultarPadrePorUsuario(int idPerfil);
        IEnumerable<AccesoMenuDto> ConsultarHijos(int idAccesoPadre);
        IEnumerable<AccesoMenuDto> ConsultarPadre(int idUsuarioSesion);
        IEnumerable<Acceso> ConsultarHijosPorPerfil(int idPerfil, int idAccesoPadre);
        IEnumerable<AccesoDto> ConsultarPorPerfil(int idPerfil);
        IEnumerable<AccesoMenuDto> ConsultarMenuPorAccesoPadre(string claveAccesoPadre, int idUsuario);
        IEnumerable<AccesoMenuDto> ConsultarTodos();
        IEnumerable<AccesoMenuDto> ConsultarTodosPorUsuario(int idUsuarioSesion);
        IEnumerable<AccesoMenuDto> ConsultarTodosPorPerfil(int idPerfil);
        bool TieneAcceso(int idUsuario, string codigoAcceso);
        IEnumerable<AccesoGridDto> ConsultarPorRolAcceso(int idRolAcceso);
        IEnumerable<AccesoGridDto> ConsultarParaReporteArbol(string claveAccesoRol);
    }
}
