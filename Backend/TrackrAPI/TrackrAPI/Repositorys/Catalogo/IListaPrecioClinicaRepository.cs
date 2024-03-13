using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.CanalDistribucion;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IListaPrecioClinicaRepository : IRepository<ListaPrecioClinica>
    {
        ListaPrecioClinica Consultar(int idListaPrecioClinica);
        IEnumerable<ListaPrecioClinicaDto> ConsultarPorListaPrecio(int idListaPrecio);
        IEnumerable<PrecioListaGeneralDto> ConsultarParaCanal(int idCompania);
    }
}
