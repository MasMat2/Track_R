using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IConfiguracionRepository : IRepository<Configuracion>
    {
        public IEnumerable<Configuracion> ConsultarPorCompaniaContable(int idCompania);
        public IEnumerable<Configuracion> ConsultarPorCompaniaMedica(int idCompania);
        public Configuracion ConsultarPorClave(string clave, int idCompania);
        public Configuracion Consultar(int idConfiguracion);
    }
}
