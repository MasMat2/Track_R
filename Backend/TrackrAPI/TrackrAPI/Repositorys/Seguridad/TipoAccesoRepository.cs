
using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class TipoAccesoRepository : Repository<TipoAcceso>, ITipoAccesoRepository
    {
        public TipoAccesoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<TipoAcceso> ConsultarGeneral()
        {
            var tipoAccesoList = from e in context.TipoAcceso orderby e.Nombre ascending select e;
            return tipoAccesoList;
        }

    }
}
