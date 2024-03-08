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
        IEnumerable<AccesoDto> ConsultarPorPerfil(int idPerfil);
        bool TieneAcceso(int idUsuario, string codigoAcceso);
        IEnumerable<AccesoGridDto> ConsultarParaReporteArbol(string claveAccesoRol);
        IEnumerable<AccesoMenuDto> ConsultarPorPerfilParaMenu(int idPerfil);

    }
}
