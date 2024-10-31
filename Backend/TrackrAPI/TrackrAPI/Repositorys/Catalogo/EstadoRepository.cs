using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        public EstadoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Estado? Consultar(int idEstado)
        {
            return context.Estado
                .Where(e => e.IdEstado == idEstado)
                .FirstOrDefault();
        }

        public IEnumerable<Estado> ConsultarParaGrid()
        {
            return context.Estado
                .Include(e => e.IdPaisNavigation);
        }

        public IEnumerable<Estado> ConsultarTodos()
        {
            return context.Estado;
        }

        public IEnumerable<Estado> ConsultarPorPais(int idPais)
        {
            return context.Estado
                .Where(e => e.IdPais == idPais);
        }

        public Estado? Consultar(string nombre, int idPais)
        {
            return context.Estado
                .Where(e => e.Nombre.ToLower() == nombre.ToLower() && e.IdPais == idPais)
                .FirstOrDefault();
        }

        public Estado? ConsultarDependencias(int idEstado)
        {
            return context.Estado
                .Include(e => e.Municipio)
                .Where(e => e.IdEstado == idEstado)
                .FirstOrDefault();
        }
    }
}
