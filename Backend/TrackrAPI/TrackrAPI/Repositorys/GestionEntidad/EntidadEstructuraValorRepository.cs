using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class EntidadEstructuraValorRepository : Repository<EntidadEstructuraValor>, IEntidadEstructuraValorRepository
    {
        public EntidadEstructuraValorRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<EntidadEstructuraValorDto> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            return context.EntidadEstructuraValor
                    .Where(e => e.IdEntidadEstructura == idEntidadEstructura && e.IdTabla == idTabla)
                    .Select(e => new EntidadEstructuraValorDto
                    {
                        IdEntidadEstructuraValor = e.IdEntidadEstructuraValor,
                        IdEntidadEstructura = e.IdEntidadEstructura,
                        ClaveCampo = e.ClaveCampo,
                        Valor = e.Valor,
                        IdTabla = e.IdTabla
                    });
        }
    }
}
