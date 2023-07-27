using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ListaPrecioRepository : Repository<ListaPrecio>, IListaPrecioRepository
    {
        public ListaPrecioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public ListaPrecioDetalle ConsultarPorPresentacion(int idPresentacion)
        {
            return context.ListaPrecioDetalle.Where(e => e.IdPresentacion == idPresentacion).FirstOrDefault();
        }

        public IEnumerable<ListaPrecioDto> ConsultarTodosParaSelector()
        {
            return context.ListaPrecio
                .Select(lp => new ListaPrecioDto
                {
                    IdListaPrecio = lp.IdListaPrecio,
                    Nombre = lp.Nombre
                })
                .ToList();
        }

        public ListaPrecio Consultar(int idListaPrecio)
        {
            return context.ListaPrecio
                .Where(e => e.IdListaPrecio == idListaPrecio)
                .FirstOrDefault();
        }

        public ListaPrecioDto ConsultarDto(int idListaPrecios)
        {
            return context.ListaPrecio
                      .Where(e => e.IdListaPrecio == idListaPrecios)
                      .Select(e => new ListaPrecioDto
                      {
                          IdListaPrecio = e.IdListaPrecio,
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          FechaInicioVigencia = e.FechaInicioVigencia,
                          FechaFinVigencia = e.FechaFinVigencia,
                          Observaciones = e.Observaciones,
                          IdMoneda = e.IdMoneda
                      })
                      .FirstOrDefault();
        }

        public IEnumerable<ListaPrecioDto> ConsultarVigente(int idHospital)
        {
            var hoy = Utileria.ObtenerFechaActual();

            return context.ListaPrecio
                      .Where(e => e.ListaPrecioClinica.Any(lpc => lpc.IdClinica == idHospital)
                        && hoy.Date >= e.FechaInicioVigencia.Date
                        && hoy.Date <= e.FechaFinVigencia.Date
                      )
                      .Select(e => new ListaPrecioDto
                      {
                          IdListaPrecio = e.IdListaPrecio,
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          FechaInicioVigencia = e.FechaInicioVigencia,
                          FechaFinVigencia = e.FechaFinVigencia,
                          Observaciones = e.Observaciones,
                          EsDefault = e.HospitalIdListaPrecioDefaultNavigation.Any(lpd => lpd.IdHospital == idHospital)
                      })
                      .ToList();
        }

        public IEnumerable<ListaPrecioDto> ConsultarVigentes()
        {
            var hoy = Utileria.ObtenerFechaActual();

            return context.ListaPrecio
                      .Where(e =>
                        hoy.Date >= e.FechaInicioVigencia.Date
                        && hoy.Date <= e.FechaFinVigencia.Date
                      )
                      .Select(e => new ListaPrecioDto
                      {
                          IdListaPrecio = e.IdListaPrecio,
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          FechaInicioVigencia = e.FechaInicioVigencia,
                          FechaFinVigencia = e.FechaFinVigencia,
                          Observaciones = e.Observaciones
                      })
                      .ToList();
        }

        public IEnumerable<ListaPrecioGridDto> ConsultarTodosPorHospitalParaGrid(int idHospital)
        {
            return context.ListaPrecio
                      .Where(lp => lp.TodasClinicas || lp.ListaPrecioClinica.Any(lpc => lpc.IdClinica == idHospital))
                      .OrderBy(e => e.Clave)
                      .Include(e => e.ListaPrecioClinica)
                      .ThenInclude(e => e.IdClinicaNavigation)
                      .Select(e => new ListaPrecioGridDto
                      {
                          IdListaPrecio = e.IdListaPrecio,
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          FechaInicioVigencia = e.FechaInicioVigencia,
                          FechaFinVigencia = e.FechaFinVigencia,
                          Clinica = e.ListaPrecioClinica.ObtenerClinicas()
                      })
                      .ToList();
        }

        public IEnumerable<ListaPrecioGridDto> ConsultarTodosPorHospitalParaSelector(int idHospital)
        {
            var hoy = Utileria.ObtenerFechaActual();

            return context.ListaPrecio
                      .Where( lp => (lp.TodasClinicas || lp.ListaPrecioClinica.Any(lpc => lpc.IdClinica == idHospital)) &&
                                    (hoy.Date >= lp.FechaInicioVigencia.Date && hoy.Date <= lp.FechaFinVigencia))
                      .OrderBy(e => e.Clave)
                      .Include(e => e.ListaPrecioClinica)
                      .ThenInclude(e => e.IdClinicaNavigation)
                      .Select(e => new ListaPrecioGridDto
                      {
                          IdListaPrecio = e.IdListaPrecio,
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                      })
                      .ToList();
        }


        public ListaPrecio Consultar(string nombre, int idHospital)
        {
            return context.ListaPrecio
                      .Where(e => e.Nombre == nombre && (e.TodasClinicas || e.ListaPrecioClinica.Any(lpc => lpc.IdClinica == idHospital)))
                      .FirstOrDefault();
        }

        public ListaPrecio ConsultarPorClave(string clave, int idHospital)
        {
            return context.ListaPrecio
                      .Where(e => e.Clave == clave && (e.TodasClinicas || e.ListaPrecioClinica.Any(lpc => lpc.IdClinica == idHospital)))
                      .FirstOrDefault();
        }

        public ListaPrecio ConsultarDependencias(int idListaPrecio)
        {
            return context.ListaPrecio
                      .Include(e => e.HospitalIdListaPrecioDefaultNavigation)
                      .Include(e => e.HospitalIdListaPrecioLineaNavigation)
                      .Where(e => e.IdListaPrecio == idListaPrecio)
                      .FirstOrDefault();
        }

        public IEnumerable<ListaPrecio> ConsultarPorCompania(int idCompania)
        {
            return context.ListaPrecio
                      .Where(lp => lp.ListaPrecioClinica.Any(lpc => lpc.IdClinicaNavigation.IdCompania == idCompania))
                      .ToList();
        }

        public IEnumerable<ListaPrecio> ConsultarVigentesPorCompania(int idCompania)
        {
            var hoy = Utileria.ObtenerFechaActual();

            return context.ListaPrecio
                      .Where(
                            lp => lp.ListaPrecioClinica.Any(lpc => lpc.IdClinicaNavigation.IdCompania == idCompania)
                            && (hoy.Date >= lp.FechaInicioVigencia.Date && hoy.Date <= lp.FechaFinVigencia)
                      )
                      .ToList();
        }
    }
}
