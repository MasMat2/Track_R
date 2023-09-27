using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IEspecialidadRepository : IRepository<Especialidad>
    {
        Especialidad? Consultar(int idEspecialidad);
        IEnumerable<Especialidad> Consultar();
        Especialidad? ConsultarPorNombre(string nombre);
    }
}