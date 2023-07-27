using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionCaja;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionCaja
{
    public class CajaRepository : Repository<Caja>, ICajaRepository
    {
        public CajaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<CajaDto> ConsultarGeneral()
        {
            return context.Caja
                .OrderBy(c => c.IdCaja)
                .Select(c => new CajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    IdHospital = c.IdHospital,
                    Descripcion = c.Descripcion,
                    Automatica = c.Automatica,
                    IdUsuarioResponsable = c.IdUsuarioResponsable,
                    NombreUsuarioResponsable = c.IdUsuarioResponsableNavigation.ObtenerNombreCompleto(),
                    IdMoneda = c.IdMoneda
                })
                .ToList();
        }

        public IEnumerable<CajaGridDto> ConsultarTodosParaGrid(int idCompania, CajaFiltroDto filtro)
        {
            return context.Caja
                .Where(c => c.IdHospitalNavigation.IdCompania == idCompania
                            && (string.IsNullOrEmpty(filtro.Nombre) || c.Nombre.ToLower().Contains(filtro.Nombre))
                            && (c.IdTipoActivo == filtro.IdTipoActivo || filtro.IdTipoActivo == 0))
                .OrderBy(c => c.IdCaja)
                .Select(c => new CajaGridDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    IdHotel = c.IdHospital,
                    NombreHotel = c.IdHospitalNavigation.Nombre,
                    NombreTipoActivo = c.IdTipoActivoNavigation.Descripcion,
                    Descripcion = c.Descripcion
                })
                .ToList();
        }

        public IEnumerable<CajaDto> ConsultarCajasPorHotelEnSesion(int? idHotel)
        {
            return context.Caja
                .Where(c => c.IdHospital == idHotel && c.IdTipoActivoNavigation.Clave == GeneralConstant.ClaveTipoActivoCirculanteCaja)
                .Select(c => new CajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    IdHospital = c.IdHospital,
                    Descripcion = c.Descripcion,
                    Automatica = c.Automatica,
                    IdUsuarioResponsable = c.IdUsuarioResponsable,
                    NombreUsuarioResponsable = c.IdUsuarioResponsableNavigation.ObtenerNombreCompleto()
                })
                .ToList();
        }

        public IEnumerable<CajaDto> ConsultarChequerasPorHospital(int? idHospital)
        {
            return context.Caja
                .Where(c => c.IdHospital == idHospital && c.IdTipoActivoNavigation.Clave == GeneralConstant.ClaveTipoActivoCirculanteChequera)
                .Select(c => new CajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    IdHospital = c.IdHospital,
                    Descripcion = c.Descripcion,
                    Automatica = c.Automatica,
                    IdUsuarioResponsable = c.IdUsuarioResponsable,
                    IdMoneda = c.IdMoneda,
                    NombreUsuarioResponsable = c.IdUsuarioResponsableNavigation.ObtenerNombreCompleto()
                })
                .ToList();
        }

        public IEnumerable<CajaDto> ConsultarObjetosFlujoPorLocacion(int idLocacion)
        {
            return context.Caja
                .Where(c => c.IdHospital == idLocacion)
                .Select(c => new CajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    IdHospital = c.IdHospital,
                    Descripcion = c.Descripcion,
                    Automatica = c.Automatica,
                    IdUsuarioResponsable = c.IdUsuarioResponsable,
                    NombreUsuarioResponsable = c.IdUsuarioResponsableNavigation.ObtenerNombreCompleto()
                })
                .ToList();
        }

        public IEnumerable<Caja> ConsultarPorHotel(int idHotel)
        {
            return context.Caja
                .Where(c => c.IdHospital == idHotel)
                .ToList();
        }

        public int consultarCajasAutomaticasPorHotel(int idHotel)
        {
            var cajaList = from c in context.Caja
                        .Include(c => c.IdHospitalNavigation)
                           where c.IdHospital == idHotel && c.Automatica == true
                           select c;
            return cajaList.Count();
        }

        public Caja Consultar(int idCaja)
        {
            return context.Caja
                .Include(c => c.IdTipoActivoNavigation)
                .Where(c => c.IdCaja == idCaja)
                .FirstOrDefault();
        }

        public CajaDto ConsultarDto(int idCaja)
        {
            return context.Caja
                .Where(c => c.IdCaja == idCaja)
                .Select(c => new CajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    IdHospital = c.IdHospital,
                    Descripcion = c.Descripcion,
                    Automatica = c.Automatica,
                    IdUsuarioResponsable = c.IdUsuarioResponsable,
                    IdTipoActivo = c.IdTipoActivo,
                    IdCuentaContable = c.IdCuentaContable,
                    NombreUsuarioResponsable = c.IdUsuarioResponsableNavigation.ObtenerNombreCompleto(),
                    IdMoneda = c.IdMoneda,
                    IdCuentaContableAutomatica = c.IdCuentaContableAutomatica
                })
                .FirstOrDefault();
        }

        public Caja ConsultarConDependencias(int idCaja)
        {
            return context.Caja
                .Where(c => c.IdCaja == idCaja)
                .Select(c => new Caja
                {
                    CajaTurno = c.CajaTurno,
                    Recibo = c.Recibo
                })
                .FirstOrDefault();
        }

        public Caja ConsultarCajaAutomatica(int idHotel)
        {
            return context.Caja
                .Where(c => c.IdHospital == idHotel && c.Automatica == true)
                .FirstOrDefault();
        }

        public IEnumerable<CajaDto> ConsultarResponsables()
        {
            return context.Caja
                .Include(a => a.IdUsuarioResponsableNavigation)
                .ToList()
                .GroupBy(x => x.IdUsuarioResponsable)
                .Select(y => new CajaDto{
                    IdCaja = 0,
                    IdUsuarioResponsable = y.Key
                });
        }

        /*
            Consulta los objetos de flujo de efectivo de una compania,
            cuando son de tipoActivo "caja" : solo se considera la caja que tenga un turno abierto por el usuario en sesión.
            Se utiliza en el gestor de pagos.
        */

        public IEnumerable<CajaDto> ConsultarParaSelectorPagos(int idCompania, int idUsuario)
        {
            return context.Caja
                .Where(
                    c => (c.IdHospitalNavigation.IdCompania == idCompania) && c.Automatica == false &&
                         ((c.IdTipoActivoNavigation.Clave == GeneralConstant.ClaveTipoActivoCirculanteCaja) &&
                          c.CajaTurno.Any(ct => ct.TurnoCerrado == false && ct.IdUsuario == idUsuario))
                      )
                    .Select(c => new CajaDto
                    {
                        IdCaja = c.IdCaja,
                        Nombre = c.Nombre
                    })
                    .ToList();
        }
    }
}
