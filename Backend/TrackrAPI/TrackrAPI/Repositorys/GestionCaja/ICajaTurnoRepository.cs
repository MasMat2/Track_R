using TrackrAPI.Dtos.GestionCaja;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.GestionCaja
{
    public interface ICajaTurnoRepository : IRepository<CajaTurno>
    {
        public IEnumerable<CajaTurnoDto> ConsultarGeneral();
        public CajaTurno Consultar(int idCajaTurno);
        public CajaTurnoDto ConsultarDto(int idCajaTurno);
        public CajaTurno ConsultarAutomatica(int idHotel);
        public IEnumerable<CajaTurno> ConsultarTurnosAbiertosCaja(int idCaja);
        public IEnumerable<CajaTurno> ConsultarTurnoAbiertoUsuario(int idUsuario);
        public IEnumerable<CajaTurnoDto> ConsultarPorHotel(int idHotel);
        public IEnumerable<CajaTurnoDto> ConsultarPorUsuario(int idUsuario);
        public IEnumerable<CajaTurno> ConsultarDeCajaAutomatica(int idHospital);
        public IEnumerable<CajaTurnoDto> ConsultarParaCierre(int idHospital);
        public IEnumerable<CajaTurnoDto> ConsultarPorCierre(int idCierre, int idLocacion);
    }
}