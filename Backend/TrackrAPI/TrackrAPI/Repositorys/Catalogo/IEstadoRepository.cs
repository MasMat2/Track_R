using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IEstadoRepository : IRepository<Estado>
    {
        IEnumerable<Estado> ConsultarParaGrid();
        IEnumerable<Estado> ConsultarPorPais(int idPais);
        Estado? Consultar(int idEstado);
        Estado? Consultar(string nombre, int idPais);
        Estado? ConsultarDependencias(int idEstado);
    }
}
