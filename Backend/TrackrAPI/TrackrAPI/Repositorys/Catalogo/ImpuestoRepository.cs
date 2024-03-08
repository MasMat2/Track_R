using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ImpuestoRepository : Repository<Impuesto>, IImpuestoRepository
    {
        public ImpuestoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<ImpuestoGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return context.Impuesto
                .Where(i => i.IdCompania == idCompania)
                .OrderBy(i => i.Nombre)
                .Select(i => new ImpuestoGridDto
                {
                    IdImpuesto = i.IdImpuesto,
                    Clave = i.Clave,
                    Nombre = i.Nombre,
                    CuentaContableNumeroNombre = i.IdCuentaContableRedondeoNavigation.NumeroNombre(),
                    PuedeEditar = !i.ReciboDetalle.Any()
                })
                .ToList();
        }

        public IEnumerable<ImpuestoDto> ConsultarTodosParaSelector(int idCompania)
        {
            return context.Impuesto
                .Where(i => i.IdCompania == idCompania)
                .Select(i => new ImpuestoDto
                {
                    IdImpuesto = i.IdImpuesto,
                    Nombre = i.Nombre,
                    Clave = i.Clave
                })
                .ToList();
        }

        public Impuesto Consultar(int idImpuesto)
        {
            return context.Impuesto
                .Include(i => i.ImpuestoDetalle)
                .Where(i => i.IdImpuesto == idImpuesto)
                .FirstOrDefault();
        }

        public ImpuestoDto ConsultarDto(int idImpuesto)
        {
            return context.Impuesto
                .Where(i => i.IdImpuesto == idImpuesto)
                .Select(i => new ImpuestoDto
                {
                    IdImpuesto = i.IdImpuesto,
                    Clave = i.Clave,
                    Nombre = i.Nombre,
                    PorcentajeNeto = i.PorcentajeNeto,
                    IdCuentaContableRedondeo = i.IdCuentaContableRedondeo
                })
                .FirstOrDefault();
        }

        public ImpuestoDto ConsultarDto(string clave, int idCompania)
        {
            return context.Impuesto
                .Where(i => i.Clave == clave && i.IdCompania == idCompania)
                .Select(i => new ImpuestoDto
                {
                    IdImpuesto = i.IdImpuesto,
                    Clave = i.Clave
                })
                .FirstOrDefault();
        }

        public Impuesto ConsultarPorClave(string clave, int idCompania)
        {
            return context.Impuesto
                .Where(i => i.Clave == clave && i.IdCompania == idCompania)
                .FirstOrDefault();
        }

        public ImpuestoDto ConsultarPorNombre(int idCompania, string nombre)
        {
            return context.Impuesto
                .Where(i => i.Nombre.ToLower() == nombre.ToLower()
                && i.IdCompania == idCompania)
                .Select(i => new ImpuestoDto
                {
                    IdImpuesto = i.IdImpuesto,
                    Clave = i.Clave,
                    Nombre = i.Nombre,
                    PorcentajeNeto = i.PorcentajeNeto,
                    IdCuentaContableRedondeo = i.IdCuentaContableRedondeo
                })
                .FirstOrDefault();
        }

        public Impuesto ConsultarDependencias(int idImpuesto)
        {
            return context.Impuesto
                .Where(i => i.IdImpuesto == idImpuesto)
                .Select(i => new Impuesto
                {
                    FacturaConcepto = i.FacturaConcepto,
                    //ImpuestoDetalle = i.ImpuestoDetalle,
                    ListaPrecioDetalle = i.ListaPrecioDetalle,
                    NotaGastoDetalle = i.NotaGastoDetalle,
                    NotaVentaDetalle = i.NotaVentaDetalle,
                    PolizaAplicadaDetalle = i.PolizaAplicadaDetalle,
                    PolizaDetalle = i.PolizaDetalle,
                    ReciboDetalle = i.ReciboDetalle
                })
                .FirstOrDefault();
        }
    }
}
