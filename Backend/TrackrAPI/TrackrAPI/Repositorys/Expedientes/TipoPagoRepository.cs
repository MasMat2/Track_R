using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Expedientes
{
    public class TipoPagoRepository : Repository<TipoPago>, ITipoPagoRepository
    {

        public TipoPagoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public TipoPago ConsultarPorClave(string clave)
        {
            return context.TipoPago
              .Where(tr => tr.Clave == clave)
              .FirstOrDefault();
        }
        public TipoPago ConsultarPorNombre(string nombre)
        {
            return context.TipoPago
              .Where(tr => tr.Nombre == nombre)
              .FirstOrDefault();
        }

        public TipoPago Consultar(int idTipoPago)
        {
            return context.TipoPago
              .Where(tr => tr.IdTipoPago == idTipoPago)
              .FirstOrDefault();
        }

        public TipoPagoDto ConsultarDto(int idTipoPago)
        {
            return context.TipoPago
              .Where(tr => tr.IdTipoPago == idTipoPago)
              .Select(tr => new TipoPagoDto
              {
                IdTipoPago = tr.IdTipoPago,
                Clave = tr.Clave,
                Nombre = tr.Nombre,
                IdConcepto = tr.IdConcepto
              })
              .FirstOrDefault();
        }

        public IEnumerable<TipoPagoDto> ConsultarParaSelector()
        {
            return context.TipoPago
                .Select(tp => new TipoPagoDto
                {
                    IdTipoPago = tp.IdTipoPago,
                    Clave = tp.Clave,
                    Nombre = tp.Nombre
                })
                .ToList();
        }
        public IEnumerable<TipoPagoDto> ConsultarParaGrid()
        {
            return context.TipoPago
                .Select(tp => new TipoPagoDto
                {
                    IdTipoPago = tp.IdTipoPago,
                    Clave = tp.Clave,
                    Nombre = tp.Nombre
                })
                .ToList();
        }
        public TipoPago ConsultarDependencias(int idTipoPago)
        {
            return context.TipoPago
              .Where(tr => tr.IdTipoPago == idTipoPago)
              .Include(tr => tr.Pago)
              .FirstOrDefault();
        }

    }
}
