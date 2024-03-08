using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class SeccionRepository : Repository<Seccion>, ISeccionRepository
    {
        public SeccionRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<SeccionGridDto> ConsultarGeneral()
        {
            return context.Seccion
                      .OrderBy(s => s.Orden)
                      .Select(s => new SeccionGridDto
                      {
                          IdSeccion = s.IdSeccion,
                          Nombre = s.Nombre,
                          Orden = s.Orden,
                          Clave = s.Clave
                      })
                      .ToList();
        }

        public Seccion Consultar(int idSeccion)
        {
            var seccion = context.Seccion.Where(e => e.IdSeccion == idSeccion).ToList();
            return seccion.FirstOrDefault();
        }

        public Seccion ConsultarConDependencias(int idSeccion)
        {
            var seccion = context.Seccion.Where(e => e.IdSeccion == idSeccion)
                .Include(e => e.SeccionCampo)
                .Include(e => e.EntidadEstructura)
                .ToList();
            return seccion.FirstOrDefault();
        }

        public Seccion Consultar(string nombre, string clave)
        {
            return context.Seccion
                      .Where(e => e.Nombre.ToLower() == nombre.ToLower() || (e.Clave.Equals(clave) && !String.IsNullOrEmpty(clave)))
                      .FirstOrDefault();
        }

        public IEnumerable<SeccionDto> consultarTodosParaSelector()
        {
            return context.Seccion
                      .OrderBy(e => e.Nombre)
                      .Select(e => new SeccionDto {
                          IdSeccion = e.IdSeccion,
                          Nombre = e.Nombre,
                          Orden = e.Orden,
                          Clave = e.Clave
                      })
                      .ToList();
        }

        public Seccion ConsultarPorNombre(string nombre)
        {
            var seccion = context.Seccion
                .Where(e => e.Nombre == nombre)
                .ToList();
            return seccion.FirstOrDefault();
        }

        public Seccion ConsultarPorClave(string clave)
        {
            var seccion = context.Seccion
                .Where(e => e.Clave == clave)
                .ToList();
            return seccion.FirstOrDefault();
        }

        public string ConsultarUltimaClave()
        {
            return context.Seccion
                .OrderByDescending(s => s.IdSeccion)
                .FirstOrDefault()?.Clave;
        }
    }
}
