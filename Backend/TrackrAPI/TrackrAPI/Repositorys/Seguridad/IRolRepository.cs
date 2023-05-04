using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IRolRepository : IRepository<Rol>
    {
        public Rol Consultar(int idRol);
        public RolDto ConsultarDto(int idRol);
        public IEnumerable<RolGridDto> ConsultarTodosParaGrid(int idCompania);
        public IEnumerable<RolDto> ConsultarPorUsuario(int idUsuario);
        public Rol ConsultarPorClave(string clave);
        public Rol ConsultarPorNombre(string nombre);
        public Rol ConsultarDependencias(int idRol);
        public IEnumerable<Rol> ConsultarGeneral();
        public IEnumerable<RolDto> ConsultarTodosParaSelector();
    }
}
