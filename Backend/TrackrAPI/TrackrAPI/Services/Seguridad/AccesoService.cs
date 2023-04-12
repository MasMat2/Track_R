using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services
{
    public class AccesoService
    {
        private readonly IAccesoRepository accesoRepository;
        public AccesoService(IAccesoRepository accesoRepository)
        {
            this.accesoRepository = accesoRepository;
        }

        public Acceso Consultar(int id)
        {
            return accesoRepository.Consultar(id);
        }
    }
}
