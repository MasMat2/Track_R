using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class UbicacionVentaRepository : Repository<UbicacionVenta>, IUbicacionVentaRepository
    {
        public UbicacionVentaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<UbicacionVentaDto> ConsultarTodosParaSelector()
        {
            return context.UbicacionVenta
                .OrderBy(ac => ac.Nombre)
                .Select(ac => new UbicacionVentaDto
                {
                    IdUbicacionVenta = ac.IdUbicacionVenta,
                    Clave = ac.Clave,
                    Nombre = ac.Nombre,
                    IdPuntoVenta = ac.IdPuntoVenta,
                    NombrePuntoVenta = ac.IdPuntoVentaNavigation.Nombre
                })
                .ToList();
        }

        public UbicacionVentaDto ConsultarDto(int idUbicacionVenta)
        {
            return context.UbicacionVenta
                      .Where(ac => ac.IdUbicacionVenta == idUbicacionVenta)
                      .Select(ac => new UbicacionVentaDto
                      {
                          IdUbicacionVenta = ac.IdUbicacionVenta,
                          Clave = ac.Clave,
                          Nombre = ac.Nombre,
                          IdPuntoVenta = ac.IdPuntoVenta,
                          NombrePuntoVenta = ac.IdPuntoVentaNavigation.Nombre
                      })
                      .FirstOrDefault();
        }

        public UbicacionVenta Consultar(int idUbicacionVenta)
        {
            return context.UbicacionVenta.Where(ac => ac.IdUbicacionVenta == idUbicacionVenta).FirstOrDefault();
        }

        public UbicacionVenta ConsultarPorClave(string clave, int idCompania)
        {
            return context.UbicacionVenta
                      .Where(ac => ac.Clave == clave
                      && ac.IdPuntoVentaNavigation.IdAlmacenNavigation.IdCompania == idCompania)
                      .FirstOrDefault();
        }

        public UbicacionVenta ConsultarPorNombre(string nombre, int idCompania)
        {
            return context.UbicacionVenta
                      .Where(ac => ac.Nombre.ToLower() == nombre.ToLower()
                      && ac.IdPuntoVentaNavigation.IdAlmacenNavigation.IdCompania == idCompania)
                      .FirstOrDefault();
        }

        public IEnumerable<UbicacionVentaDto> ConsultarPorPuntoVenta(int idPuntoVenta, int idUsuarioVendedor, DateTime fechaContable)
        {
            //Se consultan las ubicaciones del punto de venta
            //Se incluyen solo las notas de venta activas de la fecha contable actual
            var ubicacionesGeneral = context.UbicacionVenta
                .Include(u => u.NotaVenta
                    .Where(nv => ((DateTime)nv.FechaContable).Date == fechaContable.Date &&
                                  nv.IdEstatusNotaVentaNavigation.Clave == GeneralConstant.ClaveEstatusNotaVentaActiva &&
                                  (nv.IdReciboNavigation.IdEstatusPagoNavigation.Clave == GeneralConstant.ClaveEstatusPagoNoPagado ||
                                   nv.IdReciboNavigation.IdEstatusPagoNavigation.Clave == GeneralConstant.ClaveEstatusPagoParcial)))
                .Include(u => u.IdPuntoVentaNavigation)
                .Where(up => up.IdPuntoVenta == idPuntoVenta)
                .OrderBy(ac => ac.Nombre)
                .ToList();

            // Se identifican las ubicaciones del vendedor
            var ubicacionesVendedor = ubicacionesGeneral.Where(u => u.NotaVenta.Any() && u.NotaVenta
                                                                               .All(nv => nv.IdUsuarioAlta == idUsuarioVendedor));

            //Se identifican las ubicaciones que no cuentan con ninguna nota de venta en la fecha contable actual
            var ubicacionesDisponibles = ubicacionesGeneral.Where(u => !u.NotaVenta.Any());

            List<UbicacionVenta> ubicaciones = new List<UbicacionVenta>();
            ubicaciones.AddRange(ubicacionesVendedor);
            ubicaciones.AddRange(ubicacionesDisponibles);

            var ubicacionesDto = ubicaciones
                .OrderBy(ac => ac.Nombre)
                .Select(ac => new UbicacionVentaDto
                {
                    IdUbicacionVenta = ac.IdUbicacionVenta,
                    Clave = ac.Clave,
                    Nombre = ac.Nombre,
                    IdPuntoVenta = ac.IdPuntoVenta,
                    NombrePuntoVenta = ac.IdPuntoVentaNavigation.Nombre
                })
                .ToList();

            return ubicacionesDto;
        }

        public IEnumerable<UbicacionVentaDto> ConsultarTodosParaGrid(int idCompania)
        {
            return context.UbicacionVenta
                .Where(ac => ac.IdPuntoVentaNavigation.IdAlmacenNavigation.IdCompania == idCompania)
                .OrderBy(ac => ac.Clave)
                .Select(ac => new UbicacionVentaDto
                {
                    IdUbicacionVenta = ac.IdUbicacionVenta,
                    Clave = ac.Clave,
                    Nombre = ac.Nombre,
                    IdPuntoVenta = ac.IdPuntoVenta,
                    NombrePuntoVenta = ac.IdPuntoVentaNavigation.Nombre
                })
                .ToList();
        }

        public UbicacionVenta ConsultarDependencias(int idUbicacionVenta)
        {
            return context.UbicacionVenta
                .Include(ac => ac.NotaVenta)
                .Include(ac => ac.PuntoVenta)
                .Include(ac => ac.Recibo)
                .Where(ac => ac.IdUbicacionVenta == idUbicacionVenta)
                .FirstOrDefault();
        }
    }
}
