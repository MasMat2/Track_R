using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Services.Seguridad
{
    public class TipoAccesoService
    {
        private ITipoAccesoRepository tipoAccesoRepository;

        public TipoAccesoService(ITipoAccesoRepository tipoAccesoRepository)
        {
            this.tipoAccesoRepository = tipoAccesoRepository;
        }

        public IEnumerable<TipoAcceso> ConsultarGeneral()
        {
            return tipoAccesoRepository.ConsultarGeneral();
        }

    }
}
