using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface IEntidadRepository : IRepository<Entidad>
    {
        public Entidad Consultar(int idEntidad);
        public Entidad Consultar(string clave, string nombre);
        public Entidad ConsultarPorClave(string clave);
        public EntidadDto ConsultarDto(int idEntidad);
        public IEnumerable<EntidadGridDto> ConsultarTodosParaGrid();
    }
}
