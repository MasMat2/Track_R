using TrackrAPI.Models;
using TrackrAPI.Repositorys.Facturacion;

namespace TrackrAPI.Services.Facturacion
{
    public class SatFormaPagoService
    {
        private ISatFormaPagoRepository satFormaPagoRepository;

        public SatFormaPagoService(ISatFormaPagoRepository satFormaPagoRepository)
        {
            this.satFormaPagoRepository = satFormaPagoRepository;
        }

        public IEnumerable<SatFormaPago> ConsultarParaSelector()
        {
            return satFormaPagoRepository.ConsultarParaSelector();
        }

    }
}
