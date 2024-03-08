using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IConfiguracionConceptoRepository : IRepository<ConfiguracionConcepto>
    {
        IEnumerable<ConfiguracionConcepto> ConsultarTodos();
        IEnumerable<ConfiguracionConcepto> ConsultarPorConcepto(int idConcepto);
        ConfiguracionConcepto Consultar(int idConfiguracionConcepto);
    }
}
