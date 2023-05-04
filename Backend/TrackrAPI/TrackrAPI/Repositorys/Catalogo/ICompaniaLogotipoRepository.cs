using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ICompaniaLogotipoRepository : IRepository<CompaniaLogotipo>
    {
        public CompaniaLogotipo Consultar(int idCompaniaLogotipo);
        public CompaniaLogotipoDto ConsultarDto(int idCompaniaLogotipo);
        public CompaniaLogotipoDto ConsultarPorCompania(int idCompania);
    }
}
