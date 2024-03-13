using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionCaja
{
    public interface IMetodoPagoRepository : IRepository<MetodoPago>
    {
        public IEnumerable<MetodoPago> ConsultarTodos();
        public MetodoPago ConsultarPorClave(string clave);
    }
}
