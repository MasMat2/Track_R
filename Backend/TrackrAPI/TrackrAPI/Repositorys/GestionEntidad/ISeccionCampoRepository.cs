using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface ISeccionCampoRepository : IRepository<SeccionCampo>
    {
        IEnumerable<SeccionCampoDto> ConsultarPorSeccion(int idSeccion);
        SeccionCampo Consultar(int idSeccionCampo);
        SeccionCampo Consultar(string descripcion);
        SeccionCampo ConsultarPorClave(string clave);
        SeccionCampo ConsultarDuplicado(int orden, string clave, int idSeccion);
    }
}
