using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class ExpedientePadecimientoService
    {
        private IExpedientePadecimientoRepository expedientePadecimientoRepository;

        public ExpedientePadecimientoService(
            IExpedientePadecimientoRepository expedientePadecimientoRepository
            )
        {
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;

        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector()
        {
            return expedientePadecimientoRepository.ConsultarParaSelector();
        }

        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario);
        }

    }
}
