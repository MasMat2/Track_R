using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class AccesoPerfilRepository : Repository<AccesoPerfil>, IAccesoPerfilRepository
    {
        public AccesoPerfilRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<AccesoPerfil> ConsultarPorPerfil(int idPerfil)
        {
            var accesoPerfilList =
                from ap in context.AccesoPerfil
                where ap.IdPerfil == idPerfil
                select ap;
            return accesoPerfilList.ToList();
        }

        public AccesoPerfil Consultar(int idPerfil, int idAcceso)
        {
            var accesoPerfil =
                from ap in context.AccesoPerfil
                where ap.IdPerfil == idPerfil
                && ap.IdAcceso == idAcceso
                select ap;
            return accesoPerfil.FirstOrDefault();
        }

    }
}
