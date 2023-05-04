using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services
{
    public class TipoClienteLogic {

        private ITipoClienteRepository tipoClienteRepository;

        public TipoClienteLogic(ITipoClienteRepository tipoClienteRepository) {
            this.tipoClienteRepository = tipoClienteRepository;
        }

        public List<TipoCliente> ConsultarPorCompania(int idCompania) {
            return tipoClienteRepository.ConsultarPorCompania(idCompania);
        }
    }
}
