using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ITipoConceptoRepository : IRepository<TipoConcepto>
    {
        IEnumerable<TipoConcepto> ConsultarTodos();
        TipoConcepto Consultar(int idTipoConcepto);
        TipoConcepto ConsultarPorClave(string clave);
        TipoConcepto ConsultarPorNombre(string nombre);
        TipoConcepto ConsultarDependencias(int idTipoConcepto);
    }
}