using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys
{
    public class IconoRepository : Repository<Icono>, IIconoRepository
    {
        public IconoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<Icono> ConsultarGeneral()
        {
            return context.Icono
                .OrderBy(i => i.Nombre)
                .ToList();
        }
    }
}
