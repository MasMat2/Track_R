using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionCaja;
using System.Collections.Generic;

namespace TrackrAPI.Services.GestionCaja
{
    public class MetodoPagoService
    {
        private IMetodoPagoRepository metodoPagoRepository;

        public MetodoPagoService(
            IMetodoPagoRepository metodoPagoRepository
        )
        {
            this.metodoPagoRepository = metodoPagoRepository;
        }

        public IEnumerable<MetodoPago> ConsultarTodos()
        {
            return metodoPagoRepository.ConsultarTodos();
        }

        public MetodoPago ConsultarPorClave(string clave)
        {
            return metodoPagoRepository.ConsultarPorClave(clave);
        }
    }
}
