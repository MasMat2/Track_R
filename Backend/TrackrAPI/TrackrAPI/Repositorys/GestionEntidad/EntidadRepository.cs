using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class EntidadRepository : Repository<Entidad>, IEntidadRepository
    {
        public EntidadRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Entidad Consultar(int idEntidad)
        {
            return context.Entidad
                .Where(e => e.IdEntidad == idEntidad)
                .FirstOrDefault();
        }

        public Entidad Consultar(string clave, string nombre)
        {
            return context.Entidad
                    .Where(e => e.Clave == clave || e.Nombre.Trim().ToLower() == nombre.Trim().ToLower())
                    .FirstOrDefault();
        }

        public Entidad ConsultarPorClave(string clave)
        {
            return context.Entidad
                .Where(e => e.Clave == clave)
                .FirstOrDefault();
        }

        public EntidadDto ConsultarDto(int idEntidad)
        {
            return context.Entidad
                .Where(e => e.IdEntidad == idEntidad)
                .Select(e => new EntidadDto
                {
                    IdEntidad = e.IdEntidad,
                    Clave = e.Clave,
                    Nombre = e.Nombre
                })
                .FirstOrDefault();
        }

        public IEnumerable<EntidadGridDto> ConsultarTodosParaGrid()
        {
            return context.Entidad
                .OrderBy(e => e.Clave)
                .Select(e => new EntidadGridDto
                {
                    IdEntidad = e.IdEntidad,
                    Clave = e.Clave,
                    Nombre = e.Nombre
                })
                .ToList();
        }

    }
}
