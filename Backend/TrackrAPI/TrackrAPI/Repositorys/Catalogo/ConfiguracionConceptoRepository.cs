using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ConfiguracionConceptoRepository : Repository<ConfiguracionConcepto>, IConfiguracionConceptoRepository
    {
        public ConfiguracionConceptoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<ConfiguracionConcepto> ConsultarTodos()
        {
            return context.ConfiguracionConcepto
                .Include(cc => cc.IdConceptoNavigation)
                .Include(cc => cc.IdTipoConceptoNavigation);
        }

        public IEnumerable<ConfiguracionConcepto> ConsultarPorConcepto(int idConcepto)
        {
            return ConsultarTodos()
                .Where(cc => cc.IdConcepto == idConcepto);
        }

        public ConfiguracionConcepto Consultar(int idConfiguracionConcepto)
        {
            return ConsultarTodos()
                .Where(cc => cc.IdConfiguracionConcepto == idConfiguracionConcepto)
                .FirstOrDefault();
        }
    }
}
