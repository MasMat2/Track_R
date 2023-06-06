using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface IEntidadEstructuraRepository : IRepository<EntidadEstructura>
    {
        EntidadEstructura Consultar(int idEntidadEstructura);
        EntidadEstructura ConsultarTabulacionDuplicada(string clave, string nombre, int idEntidad);
        IEnumerable<EntidadEstructura> ConsultarPorEntidad(int idEntidad);
        IEnumerable<EntidadEstructura> ConsultarPorEntidadSeccion(int idEntidad, int idSeccion);
        IEnumerable<EntidadEstructuraDto> ConsultarPadres(int idEntidad);
        IEnumerable<EntidadEstructuraDto> ConsultarHijos(int idEntidadEstructuraPadre);
        List<EntidadEstructura> ConsultarHijosDeEstructura(int idEntidadEstructuraPadre);
        IEnumerable<EntidadEstructuraDto> ConsultarArbol(int idEntidad);
        IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarPadecimientosParaSelector();
    }
}
