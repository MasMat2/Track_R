using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ICodigoPostalRepository: IRepository<CodigoPostal>
    {
        public CodigoPostal Consultar(int idCodigoPostal);
        public CodigoPostalDto ConsultarDto(int idCodigoPostal);
        public IEnumerable<CodigoPostalGridDto> ConsultarTodosParaGrid();
        public IEnumerable<CodigoPostal> ConsultarTodos();
        public IEnumerable<CodigoPostalDto> ConsultarPorCodigoPostal(string codigoPostal);
        public IEnumerable<CodigoPostalDto> ConsultarPorMunicipio(int idMunicipio);
        public IEnumerable<CodigoPostalDto> ConsultarPorPaisBusqueda(string codigoPostal, int idPais);
        public CodigoPostal ConsultarPorColonia(string colonia);
    }
}
