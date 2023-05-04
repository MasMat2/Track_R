using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class TipoUsuarioRepository : Repository<TipoUsuario>, ITipoUsuarioRepository
    {
        public TipoUsuarioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public TipoUsuarioDto ConsultarDto(string clave)
        {
            return context.TipoUsuario
                .Where(tu => tu.Clave == clave)
                .Select(tu => new TipoUsuarioDto
                {
                    IdTipoUsuario = tu.IdTipoUsuario,
                    Clave = tu.Clave,
                    Nombre = tu.Nombre
                })
                .FirstOrDefault();
        }

        public TipoUsuario Consultar(int idTipoUsuario)
        {
            return context.TipoUsuario
                .Where(tu => tu.IdTipoUsuario == idTipoUsuario)
                .FirstOrDefault();
        }

        public TipoUsuarioDto ConsultarDto(int idTipoUsuario)
        {
            return context.TipoUsuario
                .Where(tu => tu.IdTipoUsuario == idTipoUsuario)
                .Select(tu => new TipoUsuarioDto
                {
                    IdTipoUsuario = tu.IdTipoUsuario,
                    Clave = tu.Clave,
                    Nombre = tu.Nombre
                })
                .FirstOrDefault();
        }

        public IEnumerable<TipoUsuarioDto> ConsultarTiposUsuarioSelector()
        {
            return context.TipoUsuario
                .Select(tu => new TipoUsuarioDto
                {
                    IdTipoUsuario = tu.IdTipoUsuario,
                    Clave = tu.Clave,
                    Nombre = tu.Nombre
                })
                .ToList();
        }
    }
}
