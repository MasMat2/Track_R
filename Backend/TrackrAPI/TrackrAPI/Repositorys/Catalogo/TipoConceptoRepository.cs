using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class TipoConceptoRepository : Repository<TipoConcepto>, ITipoConceptoRepository
    {
        public TipoConceptoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<TipoConcepto> ConsultarTodos()
        {
            return context.TipoConcepto;
        }

        public TipoConcepto Consultar(int idTipoConcepto)
        {
            return context.TipoConcepto
                .Where(c => c.IdTipoConcepto == idTipoConcepto)
                .FirstOrDefault();
        }

        public TipoConcepto ConsultarPorClave(string clave)
        {
            return context.TipoConcepto
                .Where(c => c.Clave.ToLower() == clave.ToLower())
                .FirstOrDefault();
        }

        public TipoConcepto ConsultarPorNombre(string nombre)
        {
            return context.TipoConcepto
                .Where(c => c.Nombre.ToLower() == nombre.ToLower())
                .FirstOrDefault();
        }

        public TipoConcepto ConsultarDependencias(int idTipoConcepto)
        {
            return context.TipoConcepto
                .Where(c => c.IdTipoConcepto == idTipoConcepto)
                .Include(c => c.ConfiguracionConcepto)
                .FirstOrDefault();
        }
    }
}
