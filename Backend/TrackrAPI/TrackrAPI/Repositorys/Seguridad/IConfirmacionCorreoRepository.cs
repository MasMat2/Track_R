using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;


namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IConfirmacionCorreoRepository : IRepository<ConfirmacionCorreo>
    {
        public ConfirmacionCorreo ConsultarPorUsuario(int idUsuario);
    }
}
