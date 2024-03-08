using TrackrAPI.Models;
using TrackrAPI.Dtos.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IDominioDetalleRepository : IRepository<DominioDetalle>
    {
        public IEnumerable<DominioDetalleGridDto> ConsultarPorDominio(int idDominio, int idCompania);

        public DominioDetalleDto ConsultarDto(int idDominioDetalle);

        public DominioDetalle Consultar(int idDominioDetalle);
        public DominioDetalle Consultar(string valor, int idDominio);
    }
}
