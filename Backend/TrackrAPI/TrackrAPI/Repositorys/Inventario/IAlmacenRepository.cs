using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Inventario
{
    public interface IAlmacenRepository : IRepository<Almacen>
    {
        public Almacen Consultar(int idAlmacen);
        public AlmacenDto ConsultarDto(int idAlmacen);
        public IEnumerable<AlmacenDto> ConsultarPorCompania(int idCompania, int idUsuario);
        public IEnumerable<AlmacenDto> ConsultarPorCompania(int idCompania);
        public IEnumerable<AlmacenGridDto> ConsultarGeneral(int idUsuario);
        public IEnumerable<AlmacenGridDto> ConsultarPorEstado(int idEstado);
        public IEnumerable<AlmacenGridDto> ConsultarPorUsuario(int idUsuarioResponsable);
        public IEnumerable<AlmacenGridDto> ConsultarPorEstatus(int idEstatusAlmacen);
        public IEnumerable<AlmacenDto> ConsultarTodosParaSelector(int idCompania);
        public Almacen ConsultarConDependencias(int idAlmacen);
        public Almacen ConsultarPorNumero(string numero, int idCompania);
        public Almacen ConsultarPorNombre(string nombre, int idCompania);
    }
}
