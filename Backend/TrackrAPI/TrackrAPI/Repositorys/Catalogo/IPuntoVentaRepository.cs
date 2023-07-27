using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IPuntoVentaRepository : IRepository<PuntoVenta>
    {
        public IEnumerable<PuntoVentaGridDto> ConsultarTodosParaGrid(int idCompania);
        public IEnumerable<PuntoVentaDto> ConsultarTodosParaSelector(int idCompania);
        public PuntoVentaDto ConsultarDto(int idPuntoVenta);
        public PuntoVenta Consultar(int idPuntoVenta);
        public PuntoVenta ConsultarPorNombre(string nombre, int idCompania);
        public PuntoVenta ConsultarPorClave(string clave, int idCompania);
        public PuntoVenta ConsultarDependencias(int idPuntoVenta);
        public IEnumerable<UsuarioDto> ConsultarUsuariosAsignados(int idPuntoVenta);
    }
}
