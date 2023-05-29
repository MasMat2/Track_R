using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IExpedientePadecimientoRepository: IRepository<ExpedientePadecimiento>
    {
        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario);
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector();
    }
}
