using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionCaja;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Helpers;

namespace TrackrAPI.Repositorys.GestionCaja
{
    public class CajaTurnoRepository : Repository<CajaTurno>, ICajaTurnoRepository
    {
        public CajaTurnoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<CajaTurnoDto> ConsultarGeneral()
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                    .OrderBy(ct => ct.IdCajaTurno)
                .Select(ct => new CajaTurnoDto
                {
                    Descripcion = ct.FechaContable.FormatoFecha(),
                    FechaAlta = ct.FechaAlta,
                    FechaContable = ct.FechaContable,
                    FechaFin = ct.FechaFin,
                    FechaInicio = ct.FechaInicio,
                    IdCaja = ct.IdCaja,
                    IdCajaTurno = ct.IdCajaTurno,
                    IdTurno = (int)ct.IdTurno,
                    IdUsuario = (int)ct.IdUsuario,
                    FondoCaja = (double)ct.FondoCaja,
                    MontoEsperado = (double?)ct.MontoEsperado,
                    MontoIngresado = (double?)ct.MontoIngresado,
                    NombreUsuario = ct.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    NombreCaja = ct.IdCajaNavigation.Nombre,
                    TurnoCerrado = ct.TurnoCerrado
                })
                .ToList();
        }

        public CajaTurno Consultar(int idCajaTurno)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdCajaTurno == idCajaTurno)
                .FirstOrDefault();
        }

        public CajaTurnoDto ConsultarDto(int idCajaTurno)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdCajaTurno == idCajaTurno)
                .Select(ct => new CajaTurnoDto
                {
                    Descripcion = ct.FechaContable.FormatoFecha(),
                    FechaAlta = ct.FechaAlta,
                    FechaContable = ct.FechaContable,
                    FechaFin = ct.FechaFin,
                    FechaInicio = ct.FechaInicio,
                    IdCaja = ct.IdCaja,
                    IdCajaTurno = ct.IdCajaTurno,
                    IdTurno = (int)ct.IdTurno,
                    IdUsuario = (int)ct.IdUsuario,
                    FondoCaja = (double)ct.FondoCaja,
                    MontoEsperado = (double?)ct.MontoEsperado,
                    MontoIngresado = (double?)ct.MontoIngresado,
                    NombreUsuario = ct.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    NombreCaja = ct.IdCajaNavigation.Nombre,
                    TurnoCerrado = ct.TurnoCerrado,
                    Turno = ct.IdTurnoNavigation.Nombre
                })
                .FirstOrDefault();
        }

        public CajaTurno ConsultarAutomatica(int idHotel)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdCajaNavigation.Automatica == true
                            && ct.IdCajaNavigation.IdHospital == idHotel
                            && ct.TurnoCerrado == false)
                .FirstOrDefault();
        }

        public IEnumerable<CajaTurno> ConsultarTurnosAbiertosCaja(int idCaja)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.TurnoCerrado == false && ct.IdCaja == idCaja)
                .ToList();
        }

        public IEnumerable<CajaTurno> ConsultarTurnoAbiertoUsuario(int idUsuario)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdUsuario == idUsuario
                                && ct.TurnoCerrado == false
                                && ct.IdCajaNavigation.Automatica == false
                                && ct.IdCajaNavigation.IdHospital == ct.IdUsuarioNavigation.IdHospital)
                .ToList();
        }

        public IEnumerable<CajaTurnoDto> ConsultarPorHotel(int idHotel)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdCajaNavigation.IdHospital == idHotel)
                .Select(ct => new CajaTurnoDto
                {
                    Descripcion = ct.FechaContable.FormatoFecha(),
                    FechaAlta = ct.FechaAlta,
                    FechaContable = ct.FechaContable,
                    FechaFin = ct.FechaFin,
                    FechaInicio = ct.FechaInicio,
                    IdCaja = ct.IdCaja,
                    IdCajaTurno = ct.IdCajaTurno,
                    IdTurno = (int)ct.IdTurno,
                    IdUsuario = (int)ct.IdUsuario,
                    FondoCaja = (double)ct.FondoCaja,
                    MontoEsperado = (double?)ct.MontoEsperado,
                    MontoIngresado = (double?)ct.MontoIngresado,
                    NombreUsuario = ct.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    NombreCaja = ct.IdCajaNavigation.Nombre,
                    TurnoCerrado = ct.TurnoCerrado
                })
                .ToList();
        }

        public IEnumerable<CajaTurnoDto> ConsultarParaCierre(int idHotel)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdCajaNavigation.IdHospital == idHotel
                            && ct.IdCierre == null)
                .OrderByDescending(ct => ct.FechaInicio)
                .Select(ct => new CajaTurnoDto
                {
                    Descripcion = ct.FechaContable.FormatoFecha(),
                    FechaAlta = ct.FechaAlta,
                    FechaContable = ct.FechaContable,
                    FechaFin = ct.FechaFin,
                    FechaInicio = ct.FechaInicio,
                    IdCaja = ct.IdCaja,
                    IdCajaTurno = ct.IdCajaTurno,
                    IdTurno = ct.IdTurno == null ? 0 : (int)ct.IdTurno,
                    IdUsuario = ct.IdUsuario == null ? 0 : (int)ct.IdUsuario,
                    FondoCaja = ct.FondoCaja == null ? 0 : (double)ct.FondoCaja,
                    MontoEsperado = (double?)ct.MontoEsperado,
                    MontoIngresado = (double?)ct.MontoIngresado,
                    NombreUsuario = ct.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    NombreCaja = ct.IdCajaNavigation.Nombre,
                    TurnoCerrado = ct.TurnoCerrado
                })
                .ToList();
        }

        public IEnumerable<CajaTurnoDto> ConsultarPorUsuario(int idUsuario)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdUsuario == idUsuario
                             && ct.IdCajaNavigation.Automatica == false)
                .Select(ct => new CajaTurnoDto
                {
                    Descripcion = ct.FechaContable.FormatoFecha(),
                    FechaAlta = ct.FechaAlta,
                    FechaContable = ct.FechaContable,
                    FechaFin = ct.FechaFin,
                    FechaInicio = ct.FechaInicio,
                    IdCaja = ct.IdCaja,
                    IdCajaTurno = ct.IdCajaTurno,
                    IdTurno = (int)ct.IdTurno,
                    IdUsuario = (int)ct.IdUsuario,
                    FondoCaja = (double)ct.FondoCaja,
                    MontoEsperado = (double?)ct.MontoEsperado,
                    MontoIngresado = (double?)ct.MontoIngresado,
                    NombreUsuario = ct.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    NombreCaja = ct.IdCajaNavigation.Nombre,
                    TurnoCerrado = ct.TurnoCerrado
                })
                .OrderByDescending(ct => ct.FechaContable)
                .ToList();
        }

        public IEnumerable<CajaTurno> ConsultarDeCajaAutomatica(int idHotel)
        {
            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdCajaNavigation.IdHospital == idHotel)
                .OrderByDescending(ct => ct.FechaInicio)
                .ToList();
        }

        public IEnumerable<CajaTurnoDto> ConsultarPorCierre(int idCierre, int idLocacion)
        {
            int? idCierreAux = idCierre == 0 ? (int?)null : idCierre;

            return context.CajaTurno
                    .Include(ct => ct.IdCajaNavigation)
                    .Include(ct => ct.IdTurnoNavigation)
                    .Include(ct => ct.IdUsuarioNavigation)
                .Where(ct => ct.IdCierre == idCierreAux
                    && ct.IdCajaNavigation.IdHospital == idLocacion)
                .Select(ct => new CajaTurnoDto
                {
                    Descripcion = ct.FechaContable.FormatoFecha(),
                    FechaAlta = ct.FechaAlta,
                    FechaContable = ct.FechaContable,
                    FechaFin = ct.FechaFin,
                    FechaInicio = ct.FechaInicio,
                    IdCaja = ct.IdCaja,
                    IdCajaTurno = ct.IdCajaTurno,
                    IdTurno = (int)ct.IdTurno,
                    IdUsuario = (int)ct.IdUsuario,
                    FondoCaja = (double)ct.FondoCaja,
                    MontoEsperado = (double?)ct.MontoEsperado,
                    MontoIngresado = (double?)ct.MontoIngresado,
                    NombreUsuario = ct.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    NombreCaja = ct.IdCajaNavigation.Nombre,
                    TurnoCerrado = ct.TurnoCerrado
                })
                .ToList();
        }
    }
}