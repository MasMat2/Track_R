using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface IEntidadEstructuraValorRepository : IRepository<EntidadEstructuraValor>
    {
        /// <summary>
        ///     Consultar los valores de la EntidadEstructura indicada como tabulación (Pestañas).
        ///     Considerando también el registro de la entidad fisica a la cual estan ligados los valores.
        /// </summary>
        /// <param name="idEntidadEstructura">Identificador primario de la EntidadEstructura (Pestaña)</param>
        /// <param name="idTabla">Identificador del registro de la entidad fisica</param>
        /// <returns>Registros de EntidadEstructuraValor</returns>
        IEnumerable<EntidadEstructuraValorDto> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla);
    }
}
