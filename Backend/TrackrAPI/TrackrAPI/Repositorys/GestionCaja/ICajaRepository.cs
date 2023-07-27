using TrackrAPI.Dtos.GestionCaja;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionCaja
{
    public interface ICajaRepository : IRepository<Caja>
    {
        public IEnumerable<CajaDto> ConsultarGeneral();
        public IEnumerable<CajaGridDto> ConsultarTodosParaGrid(int idCompania, CajaFiltroDto filtro);
        public IEnumerable<CajaDto> ConsultarCajasPorHotelEnSesion(int? idHotel);
        public IEnumerable<CajaDto> ConsultarChequerasPorHospital(int? idHospital);
        public IEnumerable<CajaDto> ConsultarObjetosFlujoPorLocacion(int idLocacion);
        public IEnumerable<Caja> ConsultarPorHotel(int idHotel);
        public Caja Consultar(int idCaja);
        public CajaDto ConsultarDto(int idCaja);
        public Caja ConsultarConDependencias(int idCaja);
        public Caja ConsultarCajaAutomatica(int idHotel);
        public int consultarCajasAutomaticasPorHotel(int idHotel);
        public IEnumerable<CajaDto> ConsultarResponsables();
        public IEnumerable<CajaDto> ConsultarParaSelectorPagos(int idCompania, int idUsuario);
    }
}
