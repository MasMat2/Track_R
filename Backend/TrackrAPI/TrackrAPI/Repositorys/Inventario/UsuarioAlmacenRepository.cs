using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Inventario
{
    public class UsuarioAlmacenRepository : Repository<UsuarioAlmacen>, IUsuarioAlmacenRepository
    {
        public UsuarioAlmacenRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<UsuarioAlmacenDto> ConsultarPorAlmacenDto(int idAlmacen)
        {
            return context.UsuarioAlmacen
                .Where(ua => ua.IdAlmacen == idAlmacen)
                .Select(ua => new UsuarioAlmacenDto
                {
                    IdUsuarioAlmacen = ua.IdUsuarioAlmacen,
                    IdAlmacen = ua.IdAlmacen,
                    IdUsuario = ua.IdUsuario
                })
                .ToList();
        }

        public IEnumerable<UsuarioAlmacen> ConsultarPorAlmacen(int idAlmacen)
        {
            return context.UsuarioAlmacen
                .Where(ua => ua.IdAlmacen == idAlmacen)
                .ToList();
        }

        public UsuarioAlmacen Consultar(int idUsuarioAlmacen)
        {
            return context.UsuarioAlmacen
                .Where(ua => ua.IdUsuarioAlmacen == idUsuarioAlmacen)
                .FirstOrDefault();
        }

    }
}
