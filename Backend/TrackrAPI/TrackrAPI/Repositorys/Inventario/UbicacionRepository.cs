using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Dtos.Inventario;

namespace TrackrAPI.Repositorys.Inventario
{
    public class UbicacionRepository : Repository<Ubicacion>, IUbicacionRepository
    {
        public UbicacionRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<UbicacionDto> ConsultarPorAlmacen(int idAlmacen)
        {
            return
                context.Ubicacion
                .Where(e => e.IdAlmacen == idAlmacen)
                .Select(a => new UbicacionDto
                {
                    IdUbicacion = a.IdUbicacion,
                    IdAlmacen = a.IdAlmacen,
                    Nombre = a.Nombre + " - " + a.IdAlmacenNavigation.Nombre
                });
        }

        public IEnumerable<UbicacionDto> ConsultarPorArticulo(int idArticulo)
        {
            return
                context.Kardex
                .Where(k => k.IdArticulo == idArticulo && k.IdUbicacion != null)
                .Select(k => new UbicacionDto
                {
                    IdUbicacion = (int)k.IdUbicacion,
                    IdAlmacen = k.IdUbicacionNavigation.IdAlmacen,
                    Nombre = k.IdUbicacionNavigation.Nombre + " - " + k.IdUbicacionNavigation.IdAlmacenNavigation.Nombre
                });
        }

        public Ubicacion Consultar(int idUbicacion)
        {
            return
                context.Ubicacion
                .Where(u => u.IdUbicacion == idUbicacion)
                .FirstOrDefault();
        }

        public UbicacionDto ConsultarDto(int idUbicacion)
        {
            return
                context.Ubicacion
                .Where(u => u.IdUbicacion == idUbicacion)
                .Select(u => new UbicacionDto
                {
                    IdUbicacion = u.IdUbicacion,
                    Nombre = u.Nombre,
                    NombreAlmacen = u.IdAlmacenNavigation.Nombre,
                    IdAlmacen = u.IdAlmacen
                })
                .FirstOrDefault();
        }

        public IEnumerable<UbicacionDto> ConsultarGeneral()
        {
            return
                context.Ubicacion
                .Select(u => new UbicacionDto
                {
                    IdUbicacion = u.IdUbicacion,
                    Nombre = u.Nombre,
                    NombreAlmacen = u.IdAlmacenNavigation.Nombre,
                    IdAlmacen = u.IdAlmacen
                })
                .ToList();
        }

        public Ubicacion ConsultarDuplicado(Ubicacion ubicacion)
        {
            return
                context.Ubicacion
                .Where(u =>
                    u.IdAlmacen == ubicacion.IdAlmacen
                    && u.Nombre.ToLower() == ubicacion.Nombre.ToLower())
                .FirstOrDefault();
        }
    }
}
