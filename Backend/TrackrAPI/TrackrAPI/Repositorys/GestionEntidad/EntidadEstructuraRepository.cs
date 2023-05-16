using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class EntidadEstructuraRepository : Repository<EntidadEstructura>, IEntidadEstructuraRepository
    {
        public EntidadEstructuraRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public EntidadEstructura Consultar(int idEntidadEstructura)
        {
            return context.EntidadEstructura
                .Include(e => e.IdSeccionNavigation)
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura)
                .FirstOrDefault();
        }

        public EntidadEstructura ConsultarTabulacionDuplicada(string clave, string nombre, int idEntidad)
        {
            return context.EntidadEstructura
                .Where(e =>
                    e.IdEntidad == idEntidad &&
                    e.Tabulacion == true &&
                    (e.Clave == clave ||
                    e.Nombre == nombre))
                .FirstOrDefault();
        }

        public IEnumerable<EntidadEstructura> ConsultarPorEntidad(int idEntidad)
        {
            return context.EntidadEstructura
                .Where(e => e.IdEntidad == idEntidad)
                .ToList();
        }

        public IEnumerable<EntidadEstructura> ConsultarPorEntidadSeccion(int idEntidad, int idSeccion)
        {
            return context.EntidadEstructura
                .Where(e => e.IdEntidad == idEntidad && e.IdSeccion == idSeccion)
                .ToList();
        }

        public IEnumerable<EntidadEstructuraDto> ConsultarPadres(int idEntidad)
        {
            return context.EntidadEstructura
                .Where(e => e.IdEntidad == idEntidad && e.Tabulacion == true)
                .Select(e => new EntidadEstructuraDto {
                    IdEntidadEstructura = e.IdEntidadEstructura,
                    Nombre = e.Nombre,
                    Clave = e.Clave,
                    Tabulacion = e.Tabulacion,
                    IdEntidad = e.IdEntidad,
                    IdSeccion = e.IdSeccion,
                    IdEntidadEstructuraPadre = e.IdEntidadEstructuraPadre
                });
        }

        public IEnumerable<EntidadEstructuraDto> ConsultarHijos(int idEntidadEstructuraPadre)
        {
            return context.EntidadEstructura
                .Include(e => e.IdSeccionNavigation)
                .ThenInclude(s => s.SeccionCampo)
                .ThenInclude(sc => sc.IdDominioNavigation)
                .ThenInclude(d => d.DominioDetalle)
                .Where(e => e.IdEntidadEstructuraPadre == idEntidadEstructuraPadre)
                .Select(e => new EntidadEstructuraDto()
                {
                    IdEntidadEstructura = e.IdEntidadEstructura,
                    Nombre = e.Tabulacion == true ?
                             e.Clave + " - " + e.Nombre :
                             e.IdSeccionNavigation.Clave + " - " + e.IdSeccionNavigation.Nombre,
                    Clave = e.Clave,
                    Tabulacion = e.Tabulacion,
                    IdEntidad = e.IdEntidad,
                    IdSeccion = e.IdSeccion,
                    IdEntidadEstructuraPadre = e.IdEntidadEstructuraPadre,
                    Campos = e.IdSeccionNavigation.SeccionCampo.ToList()
                });
        }

        public List<EntidadEstructura> ConsultarHijosDeEstructura(int idEntidadEstructuraPadre)
        {
            return context.EntidadEstructura
                .Where(e => e.IdEntidadEstructuraPadre == idEntidadEstructuraPadre)
                .ToList();
        }
    }
}
