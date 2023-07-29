using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using System.Collections.Generic;
using TrackrAPI.Dtos.Seguridad;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IPerfilRepository : IRepository<Perfil>
    {
        Perfil ConsultarAdministradorPorTipoCompania(int idTipoCompania, int idCompania);
        public Perfil ConsultarPorClave(string clave, int idCompania);
        Perfil Consultar(int idPerfil);
        PerfilDto ConsultarDto(int idPerfil);
        Perfil ConsultarPorNombre(int idCompania, string nombre);
        Perfil ConsultarDependencia(int idPerfil);
        IEnumerable<PerfilDto> ConsultarGeneral(int idCompania);
        IEnumerable<Perfil> ConsultarPorCompaniaBase(int idCompania);
        IEnumerable<Perfil> ConsultarPorTipoCompania(int idTipoCompania);
        public IEnumerable<PerfilDto> ConsultarTodosParaSelector(int idCompania);
        Perfil ConsultarUltimoAgregado(bool esCompaniaBase, int idCompania);
    }
}
