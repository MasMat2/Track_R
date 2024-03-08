
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Services.Seguridad
{
    public class RolAccesoService
    {
        private IRolAccesoRepository rolAccesoRepository;
        public RolAccesoService(IRolAccesoRepository rolAccesoRepository)
        {
            this.rolAccesoRepository = rolAccesoRepository;
        }

        public IEnumerable<RolAcceso> ConsultarTodosParaSelector()
        {
            return rolAccesoRepository.ConsultarTodosParaSelector();
        }
    }
}
