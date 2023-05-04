using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Inventario
{
    public interface IDomicilioRepository : IRepository<Domicilio>
    {
        public IEnumerable<Domicilio> ConsultarTodos(int idCompania);
        public IEnumerable<DomicilioGridDto> ConsultarGeneral(int idCompania);
        public Domicilio Consultar(int idDomicilio);
        public DomicilioDto ConsultarDto(int idDomicilio);
        public IEnumerable<DomicilioGridDto> ConsultarPorEstado(int idEstado);
        public Domicilio ConsultarPorDomicilio(Domicilio domicilio);
        public IEnumerable<Domicilio> ConsultarPorUsuario(int idUsuario);
        public Domicilio ConsultarDependencias(int idDomicilio);
        public IEnumerable<DomicilioSelectorDto> ConsultarParaSelector(int idCompania);
    }
}
