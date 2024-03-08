using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IAccesoAyudaRepository: IRepository<AccesoAyuda>
    {
        AccesoAyudaDto ConsultarDto(int idAccesoAyuda);
        AccesoAyuda Consultar(int idAccesoAyuda);
        AccesoAyuda ConsultarPorNombre(string nombreArchivo);
        AccesoAyuda ConsultarPorOrden(int? orden, int idAcceso);
        IEnumerable<AccesoAyudaDto> ConsultarPorAcceso(int idAcceso);
        AccesoAyuda ConsultarDependencia(int idAccesoAyuda);
    }
}
