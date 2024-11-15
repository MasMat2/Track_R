using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo;

public interface IMunicipioRepository: IRepository<Municipio>
{
    Municipio? Consultar(int idMunicipio);
    Municipio? ConsultarParaFormulario(int idMunicipio);
    Municipio? Consultar(string nombre, int idEstado);
    Municipio? ConsultarPorClave(string clave);
    IEnumerable<Municipio> Consultar();
    IEnumerable<Municipio> ConsultarParaGrid();
    IEnumerable<Municipio> ConsultarTodos();
    IEnumerable<MunicipioDto> ConsultarTodosDto();

    IEnumerable<Municipio> ConsultarPorEstado(int idPais);
    Municipio? ConsultarDependencias(int idLocalidad);
    public Municipio? ConsultarPorNombre(string nombre);
}
