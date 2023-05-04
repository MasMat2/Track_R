using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public class TipoActivoRepository : Repository<TipoActivo>, ITipoActivoRepository
    {
        public TipoActivoRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<TipoActivoDto> ConsultarParaSelector(int idCompania)
        {
            return context.TipoActivo
                    .Where(ta => ta.IdCompania == idCompania || ta.IdCompaniaNavigation.Clave == GeneralConstant.ClaveCompaniaBase )
                    .Select(ta => new TipoActivoDto
                    {
                        IdTipoActivo = ta.IdTipoActivo,
                        Clave = ta.Clave,
                        Descripcion = ta.Descripcion
                    })
                    .ToList();
        }

        public TipoActivo ConsultarPorClave(string clave)
        {
            return context.TipoActivo
                    .Where(ta => ta.Clave == clave)
                    .FirstOrDefault();
        }
    }
}
