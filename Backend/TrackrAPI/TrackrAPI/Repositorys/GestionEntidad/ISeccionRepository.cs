using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface ISeccionRepository : IRepository<Seccion>
    {
        IEnumerable<SeccionGridDto> ConsultarGeneral();
        IEnumerable<SeccionDto> consultarTodosParaSelector();
        Seccion Consultar(int idSeccion);
        Seccion Consultar(string nombre, string clave);
        Seccion ConsultarConDependencias(int idSeccion);
        Seccion ConsultarPorNombre(string nombre);
        Seccion ConsultarPorClave(string clave);
        string ConsultarUltimaClave();
    }
}
