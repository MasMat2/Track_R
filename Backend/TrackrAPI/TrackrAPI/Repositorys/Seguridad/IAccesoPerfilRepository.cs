using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IAccesoPerfilRepository : IRepository<AccesoPerfil>
    {
        IEnumerable<AccesoPerfil> ConsultarPorPerfil(int idPerfil);
        AccesoPerfil Consultar(int idPerfil, int idAcceso);
    }
}
