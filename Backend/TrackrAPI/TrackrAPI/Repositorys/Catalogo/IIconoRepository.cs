using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IIconoRepository : IRepository<Icono>
    {
        public IEnumerable<Icono> ConsultarGeneral();
    }
}
