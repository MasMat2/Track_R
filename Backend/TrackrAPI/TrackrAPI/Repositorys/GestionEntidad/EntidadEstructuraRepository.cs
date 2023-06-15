using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.GestionExpediente;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class EntidadEstructuraRepository : Repository<EntidadEstructura>, IEntidadEstructuraRepository
    {
        public EntidadEstructuraRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public EntidadEstructura? Consultar(int idEntidadEstructura)
        {
            return context.EntidadEstructura
                .Include(e => e.IdSeccionNavigation)
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura)
                .FirstOrDefault();
        }

        public EntidadEstructura? ConsultarTabulacionDuplicada(string clave, string nombre, int idEntidad)
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
                    .ThenInclude(s => s!.SeccionCampo)
                        .ThenInclude(sc => sc.IdDominioNavigation)
                            .ThenInclude(d => d.DominioDetalle)
                .Where(e => e.IdEntidadEstructuraPadre == idEntidadEstructuraPadre)
                .Select(e => new EntidadEstructuraDto()
                {
                    IdEntidadEstructura = e.IdEntidadEstructura,
                    Nombre = e.Tabulacion == true
                        ? e.Clave + " - " + e.Nombre
                        : e.IdSeccionNavigation!.Clave + " - " + e.IdSeccionNavigation.Nombre,
                    Clave = e.Clave,
                    Tabulacion = e.Tabulacion,
                    IdEntidad = e.IdEntidad,
                    IdSeccion = e.IdSeccion,
                    IdEntidadEstructuraPadre = e.IdEntidadEstructuraPadre,
                    EsTabla = e.IdSeccionNavigation!.EsTabla ?? false,
                    Campos = e.IdSeccionNavigation.SeccionCampo.ToList()
                });
        }

        public List<EntidadEstructura> ConsultarHijosDeEstructura(int idEntidadEstructuraPadre)
        {
            return context.EntidadEstructura
                .Where(e => e.IdEntidadEstructuraPadre == idEntidadEstructuraPadre)
                .ToList();
        }

        private static EntidadEstructuraDto ToDto(EntidadEstructura estructura)
        {
            return new EntidadEstructuraDto
            {
                IdEntidadEstructura = estructura.IdEntidadEstructura,
                Nombre = estructura.Nombre ?? string.Empty,
                Clave = estructura.Clave ?? string.Empty,
                Tabulacion = estructura.Tabulacion,
                IdEntidad = estructura.IdEntidad,
                IdSeccion = estructura.IdSeccion,
                IdEntidadEstructuraPadre = estructura.IdEntidadEstructuraPadre
            };
        }

        public IEnumerable<EntidadEstructuraDto> ConsultarArbol(int idEntidad)
        {
            var estructuras = context.EntidadEstructura
                .Where(e => e.IdEntidad == idEntidad)
                .Include(e => e.IdSeccionNavigation)
                .ToList();

            var estructurasPadre = estructuras.Where(e => e.Tabulacion == true);

            var arbol = new List<EntidadEstructuraDto>();

            foreach (var estructura in estructurasPadre)
            {
                var hijos = estructuras
                    .Where(e => e.IdEntidadEstructuraPadre == estructura.IdEntidadEstructura)
                    .ToList();

                hijos.ForEach(e => e.Nombre = e.IdSeccionNavigation!.Clave + " - " + e.IdSeccionNavigation.Nombre);

                var padre = ToDto(estructura);
                padre.Nombre = padre.Nombre;
                padre.Hijos = hijos.ConvertAll(h => ToDto(h));

                arbol.Add(padre);
            }

            return arbol;
        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarPadecimientosParaSelector()
        {
            return context.EntidadEstructura
                .Where(e => (e.Tabulacion == true) &&
                            (e.IdEntidadNavigation.Clave == GeneralConstant.ClaveEntidadPadecimiento))
                .Select(e => new ExpedientePadecimientoSelectorDTO
                {
                    IdPadecimiento = e.IdEntidadEstructura,
                    Nombre = e.Nombre ?? string.Empty,
                });
        }
    }
}
