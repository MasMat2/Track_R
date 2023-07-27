using System.Collections.Generic;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IMercadoRepository : IRepository<Mercado>
    {
        IEnumerable<Mercado> ConsultarTodos();
        IEnumerable<MercadoGridDto> ConsultarParaGrid();
        Mercado Consultar(int idMercado);
        MercadoFormularioDto ConsultarFormularioDto(int idMercado);
    }
}