using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class EntidadEstructuraTablaValorRepository : Repository<EntidadEstructuraTablaValor>, IEntidadEstructuraTablaValorRepository
    {
        public EntidadEstructuraTablaValorRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            return context.EntidadEstructuraTablaValor
                    .Where(e => e.IdEntidadEstructuraNavigation.IdEntidadEstructuraPadre == idEntidadEstructura &&
                                e.IdTabla == idTabla);
        }

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla)
        {
            return context.EntidadEstructuraTablaValor
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura && e.IdTabla == idTabla);
        }

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorNumeroRegistro(int idEntidadEstructura, int idTabla, int numeroRegistro)
        {
            return context.EntidadEstructuraTablaValor
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura &&
                    e.IdTabla == idTabla &&
                    e.Numero == numeroRegistro);
        }

        public int ConsultarUltimoRegistro(int idEntidadEstructura, int idTabla)
        {
            var ultimoRegistro = context.EntidadEstructuraTablaValor
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura && e.IdTabla == idTabla)
                .OrderByDescending(e => e.Numero)
                .FirstOrDefault();

            return ultimoRegistro?.Numero ?? 0;
        }
    }
}
