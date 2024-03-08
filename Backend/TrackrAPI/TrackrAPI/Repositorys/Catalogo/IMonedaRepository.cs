using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IMonedaRepository : IRepository<Moneda>
    {
        Moneda ConsultarPorId(int idMoneda);
        Moneda ConsultarPorClave(string clave);
        Moneda ConsultarPorNombre(string nombre);
        MonedaDto ConsultarDto(int idMoneda);
        IEnumerable<MonedaGridDto> ConsultarTodosParaGrid(int idCompania);
        IEnumerable<MonedaSelectorDto> ConsultarParaSelector();
    }
}
