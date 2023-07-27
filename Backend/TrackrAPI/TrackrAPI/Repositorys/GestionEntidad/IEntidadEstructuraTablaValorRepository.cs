using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface IEntidadEstructuraTablaValorRepository : IRepository<EntidadEstructuraTablaValor>
    {
        IEnumerable<EntidadEstructuraTablaValor> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorNumeroRegistro(int idEntidadEstructura, int idTabla, int numeroRegistro);
        int ConsultarUltimoRegistro(int idEntidadEstructura, int idTabla);
    }
}
