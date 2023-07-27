using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class CompaniaContactoRepository : Repository<CompaniaContacto>, ICompaniaContactoRepository
    {
        public CompaniaContactoRepository(TrackrContext context) : base(context)
        {
        }

        public CompaniaContacto Consultar(int idCompaniaContacto)
        {
            return context.CompaniaContacto
                .Where(cc => cc.IdCompaniaContacto == idCompaniaContacto)
                .FirstOrDefault();
        }

        public IEnumerable<CompaniaContacto> ConsultarPorCompania(int idCompania)
        {
            return context.CompaniaContacto
                .Where(cc => cc.IdCompania == idCompania);
        }
    }
}