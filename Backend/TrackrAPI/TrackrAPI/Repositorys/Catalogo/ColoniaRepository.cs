using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ColoniaRepository : Repository<Colonia>, IColoniaRepository
    {
        public ColoniaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Colonia Consultar(int idColonia)
        {
            return context.Colonia
                .Where(c => c.IdColonia == idColonia)
                .FirstOrDefault();
        }

        public IEnumerable<ColoniaGridDto> ConsultarParaGrid()
        {
            return context.Colonia
                .Select(c => new ColoniaGridDto
                {
                    IdColonia = c.IdColonia,
                    Clave = c.Clave,
                    CodigoPostal = c.CodigoPostal,
                    Nombre = c.Nombre
                }).ToList();
        }

        public IEnumerable<Colonia> ConsultarPorCodigoParaSelector(string codigoPostal)
        {
            return context.Colonia
                .Where(c => c.CodigoPostal == codigoPostal);
        }

        public Colonia ConsultarPorCodigoPostal(string codigoPostal)
        {
            return context.Colonia
                .Where(c => c.CodigoPostal == codigoPostal)
                .FirstOrDefault();
        }

    }
}
