using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class LocalidadService
    {
        private ILocalidadRepository localidadRepository;

        public LocalidadService(
            ILocalidadRepository localidadRepository
        )
        {
            this.localidadRepository = localidadRepository;
        }

        public IEnumerable<Localidad> ConsultarPorEstado(int idEstado)
        {
            return localidadRepository.ConsultarPorEstado(idEstado);
        }
    }
}
