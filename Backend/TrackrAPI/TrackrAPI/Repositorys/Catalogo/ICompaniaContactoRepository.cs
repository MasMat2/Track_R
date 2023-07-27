using System.Collections.Generic;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ICompaniaContactoRepository : IRepository<CompaniaContacto>
    {
        public CompaniaContacto Consultar(int idCompania);
        public IEnumerable<CompaniaContacto> ConsultarPorCompania(int idCompania);
    }
}