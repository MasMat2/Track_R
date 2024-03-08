using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class RestablecerContrasenaRepository : Repository<RestablecerContrasena>, IRestablecerContrasenaRepository
    {
        public RestablecerContrasenaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public RestablecerContrasena ConsultarPorUsuario(int idUsuario)
        {
            var fechaActual = Utileria.ObtenerFechaActual();

            return context.RestablecerContrasena
                .Where(rc => rc.IdUsuario == idUsuario && rc.FechaAlta.Date == fechaActual.Date)
                .OrderByDescending(rc => rc.FechaAlta)
                .OrderByDescending(rc => rc.IdRestablecerContrasena)
                .Select(rc => rc)
                .FirstOrDefault();
        }

    }
}
