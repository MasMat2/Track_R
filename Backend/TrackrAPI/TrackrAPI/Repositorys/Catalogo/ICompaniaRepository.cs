using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ICompaniaRepository : IRepository<Compania>
    {
        public IEnumerable<CompaniaDto> ConsultarPorUsuarioPermiso(int idUsuario);
        public IEnumerable<CompaniaDto> ConsultarGeneral();
        public IEnumerable<CompaniaSelectorDto> ConsultarTodosParaSelector();
        public IEnumerable<CompaniaDto> ConsultarTodosParaGrid(CompaniaFiltroDto filtro, string claveCompania);
        public Compania Consultar(int idCompania);
        public Compania Consultar(string rfc);
        public CompaniaDto ConsultarPorIdentificadorUrl(string identificadorUrl);
        public CompaniaDto ConsultarDto(int idCompania);
        public Compania ConsultarUltimaAgregada();
        public Compania ConsultarPorClave(string clave);
    }
}
