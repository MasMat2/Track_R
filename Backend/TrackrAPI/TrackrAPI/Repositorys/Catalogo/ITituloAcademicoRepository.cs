using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ITituloAcademicoRepository : IRepository<TituloAcademico>
    {
        public IEnumerable<TituloAcademicoSelectorDto> ConsultarTodosParaSelector();
        public IEnumerable<TituloAcademicoGridDto> ConsultarTodosParaGrid();
        public TituloAcademicoDto ConsultarDto(int idTituloAcademico);
        public TituloAcademico Consultar(int idTituloAcademico);
        public TituloAcademico Consultar(string clave);
        public TituloAcademico ConsultarPorNombre(string nombre);
        public TituloAcademico ConsultarDependencias(int idTituloAcademico);
    }
}
