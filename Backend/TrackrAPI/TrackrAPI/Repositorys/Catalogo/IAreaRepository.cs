using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IAreaRepository: IRepository<Area>
    {
        public Area Consultar(int idArea);
        public AreaDto ConsultarDto(int idArea);
        public IEnumerable<AreaDto> ConsultarParaSelector(int idCompania);
        public Area Consultar(string nombre);
        public Area ConsultarPorClave(string clave);
        public Area ConsultarExistencia(string clave, string nombre);
        //public Area ConsultarDependencias(int idArea);

    }
}
