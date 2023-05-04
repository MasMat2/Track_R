using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Expedientes
{
    public interface ITipoPagoRepository : IRepository<TipoPago>
    {
        public TipoPago ConsultarPorClave(string clave);
        public TipoPago ConsultarPorNombre(string nombre);
        public TipoPago Consultar(int idTipoPago);
        public TipoPagoDto ConsultarDto(int idTipoPago);
        public IEnumerable<TipoPagoDto> ConsultarParaSelector();
        public IEnumerable<TipoPagoDto> ConsultarParaGrid();
        public TipoPago ConsultarDependencias(int idTipoPago);
    }
}
