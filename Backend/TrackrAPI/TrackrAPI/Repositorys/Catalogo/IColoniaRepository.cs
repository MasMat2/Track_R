using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IColoniaRepository : IRepository<Colonia>
    {
        public Colonia Consultar(int idColonia);
        public IEnumerable<Colonia> ConsultarPorCodigoParaSelector(string codigoPostal);
        public IEnumerable<ColoniaGridDto> ConsultarParaGrid();
        public Colonia ConsultarPorCodigoPostal(string codigoPostal);
    }
}
