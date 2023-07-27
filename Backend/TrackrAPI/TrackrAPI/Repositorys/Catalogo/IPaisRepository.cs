using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IPaisRepository : IRepository<Pais>
    {
        public IEnumerable<PaisDto> ConsultarTodosParaSelector();
        public PaisDto ConsultarDto(string clave);
        public PaisDto ConsultarDto(int idPais);
        public IEnumerable<PaisGridDto> ConsultarGeneral();
        public Pais Consultar(int idPais);
        public Pais ConsultarConDependencias(int idPais);
        public Pais Consultar(string nombre);
    }
}
