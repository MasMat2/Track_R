using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Inventario
{
    public interface IUbicacionRepository : IRepository<Ubicacion>
    {
        public Ubicacion Consultar(int idUbicacion);
        public UbicacionDto ConsultarDto(int idUbicacion);
        public IEnumerable<UbicacionDto> ConsultarPorAlmacen(int idAlmacen);
        public IEnumerable<UbicacionDto> ConsultarPorArticulo(int idArticulo);
        public IEnumerable<UbicacionDto> ConsultarGeneral();
        public Ubicacion ConsultarDuplicado(Ubicacion ubicacion);
    }
}
