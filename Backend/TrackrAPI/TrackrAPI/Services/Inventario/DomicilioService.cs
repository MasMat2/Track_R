using TrackrAPI.Models;
using TrackrAPI.Repositorys.Inventario;

namespace TrackrAPI.Services.Inventario
{
    public class DomicilioService
    {
        private IDomicilioRepository domicilioRepository;
        public DomicilioService(
            IDomicilioRepository domicilioRepository
            )
        { 
            this.domicilioRepository = domicilioRepository;
        }

        public Domicilio Consultar(int idDomicilio)
        {
            return domicilioRepository.Consultar(idDomicilio);
        }
    }
}
