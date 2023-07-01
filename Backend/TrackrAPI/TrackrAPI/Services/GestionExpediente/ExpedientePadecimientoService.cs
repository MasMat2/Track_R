using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente
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

        public IEnumerable<PadecimientoFueraRangoDTO> ConsultarValoresFueraRango(int idPadecimiento, int idUsuario)
        {
            return expedientePadecimientoRepository.ConsultarValoresFueraRango(idPadecimiento, idUsuario);
        }

    }
}
