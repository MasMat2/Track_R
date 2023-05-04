using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IRestablecerContrasenaRepository : IRepository<RestablecerContrasena>
    {
        public RestablecerContrasena ConsultarPorUsuario(int idUsuario);

    }
}
