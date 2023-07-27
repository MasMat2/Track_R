using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class DominioDetalleRepository : Repository<DominioDetalle>, IDominioDetalleRepository
    {
        public DominioDetalleRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<DominioDetalleGridDto> ConsultarPorDominio(int idDominio, int idCompania)
        {
            return context.DominioDetalle
                .Where(dd => dd.IdDominio == idDominio && dd.Habilitado == true)
                .Select(dd => new DominioDetalleGridDto(
                    dd.IdDominioDetalle,
                    dd.Valor,
                    dd.IdDominio))
                .ToList();
        }

        public DominioDetalleDto ConsultarDto(int idDominioDetalle)
        {
            return context.DominioDetalle
                      .Where(dd => dd.IdDominioDetalle == idDominioDetalle)
                      .Select(dd => new DominioDetalleDto(
                          dd.IdDominioDetalle,
                          dd.Valor,
                          dd.IdDominio,
                          dd.IdCompania))
                      .FirstOrDefault();
        }

        public DominioDetalle Consultar(int idDominioDetalle)
        {
            return context.DominioDetalle.Where(dd => dd.IdDominioDetalle == idDominioDetalle).FirstOrDefault();
        }

        public DominioDetalle Consultar(string valor, int idDominio)
        {
            return context.DominioDetalle
                      .Where(dd => dd.Valor == valor && dd.IdDominio == idDominio && dd.Habilitado == true)
                      .FirstOrDefault();
        }
    }
}
