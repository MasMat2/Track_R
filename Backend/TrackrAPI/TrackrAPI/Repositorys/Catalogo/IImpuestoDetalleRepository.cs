using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IImpuestoDetalleRepository : IRepository<ImpuestoDetalle>
    {
        public IEnumerable<ImpuestoDetalleGridDto> ConsultarTodosPorImpuestoParaGrid(int idImpuesto);
        public IEnumerable<ImpuestoDetalle> ConsultarPorImpuesto(int idImpuesto);
        public ImpuestoDetalleDto ConsultarDto(int idImpuestoDetalle);
        public ImpuestoDetalle Consultar(int idImpuestoDetalle);
        public ImpuestoDetalle ConsultarDependencias(int idImpuestoDetalle);
    }
}
