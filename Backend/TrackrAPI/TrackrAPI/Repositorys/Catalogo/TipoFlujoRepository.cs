using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;
using TrackrAPI.Repositorys;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class TipoFlujoRepository : Repository<TipoFlujo>, ITipoFlujoRepository
    {
        public TipoFlujoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<TipoFlujo> ConsultarTodos()
        {
            return context.TipoFlujo;
        }

        public TipoFlujo Consultar(int idTipoFlujo)
        {
            return context.TipoFlujo
                .Where(tp => tp.IdTipoFlujo == idTipoFlujo)
                .FirstOrDefault();
        }

        public TipoFlujo ConsultarPorClave(string clave)
        {
            return context.TipoFlujo
                .Where(tp => tp.Clave.ToLower() == clave.ToLower())
                .FirstOrDefault();
        }

        public TipoFlujo ConsultarPorNombre(string nombre)
        {
            return context.TipoFlujo
                .Where(tp => tp.Nombre.ToLower() == nombre.ToLower())
                .FirstOrDefault();
        }

        public TipoFlujo ConsultarDependencias(int idTipoFlujo)
        {
            return context.TipoFlujo
                .Where(tp => tp.IdTipoFlujo == idTipoFlujo)
                .Include(tp => tp.Flujo)
                .FirstOrDefault();
        }
    }
}
