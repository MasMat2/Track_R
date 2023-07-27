using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IMunicipioRepository: IRepository<Municipio>
    {
        IEnumerable<MunicipioGridDto> ConsultarTodosParaGrid();
        IEnumerable<MunicipioDto> ConsultarPorEstadoParaSelector(int idEstado);
        public IEnumerable<MunicipioDto> ConsultarTodosParaSelector();
        IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais);
        MunicipioDto ConsultarDto(int idMunicipio);
        Municipio Consultar(int idMunicipio);
        Municipio Consultar(string nombre, int idEstado);
        Municipio ConsultarPorClave(string Clave);
        bool ConsultarDependencias(int idLocalidad);
    }
}
