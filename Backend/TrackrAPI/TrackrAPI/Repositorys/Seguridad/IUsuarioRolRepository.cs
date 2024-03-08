using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IUsuarioRolRepository : IRepository<UsuarioRol>
    {
        public IEnumerable<UsuarioRolDto> ConsultarPorUsuario(int idUsuario);
        public IEnumerable<UsuarioRolGridDto> ConsultarPorUsuarioParaGrid(int idUsuario);
        public UsuarioRol Consultar(int idUsuarioRol);
        public UsuarioRol Consultar(int idUsuario, int idRol);

    }
}
