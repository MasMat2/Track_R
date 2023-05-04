using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class GiroComercialRepository : Repository<GiroComercial>, IGiroComercialRepository
    {
        public GiroComercialRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<GiroComercial> ConsultarTodos()
        {
            return context.GiroComercial;
        }

        public GiroComercial Consultar(int idGiroComercial)
        {
            return context.GiroComercial
                .Where(gc => gc.IdGiroComercial == idGiroComercial)
                .FirstOrDefault();
        }

        public GiroComercial ConsultarPorClave(string clave)
        {
            return context.GiroComercial
                .Where(gc => gc.Clave.ToLower() == clave.ToLower())
                .FirstOrDefault();
        }

        public GiroComercial ConsultarPorNombre(string nombre)
        {
            return context.GiroComercial
                .Where(gc => gc.Nombre.ToLower() == nombre.ToLower())
                .FirstOrDefault();
        }

        public GiroComercial ConsultarDependencias(int idGiroComercial)
        {
            return context.GiroComercial
                .Where(gc => gc.IdGiroComercial == idGiroComercial)
                .Include(gc => gc.Compania)
                .Include(gc => gc.Mercado)
                .FirstOrDefault();
        }
    }
}
