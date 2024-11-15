using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            .Include(c => c.IdCodigoPostalNavigation)
                .Where(c => c.IdColonia == idColonia)
                .FirstOrDefault();
        }

        public IEnumerable<ColoniaGridDto> ConsultarParaGrid()
        {
            return context.Colonia
                .Include(c => c.IdCodigoPostalNavigation)
                .Select(c => new ColoniaGridDto
                {
                    IdColonia = c.IdColonia,
                    Clave = c.Clave,
                    CodigoPostal = c.IdCodigoPostalNavigation.CodigoPostal1,
                    Nombre = c.Nombre
                }).ToList();
        }

        public IEnumerable<Colonia> Consultar(){
            return context.Colonia.Include(c => c.IdCodigoPostalNavigation).ToList();
        }


        public IEnumerable<Colonia> ConsultarPorCodigoParaSelector(string codigoPostal)
        {
            return context.Colonia
                .Include(c => c.IdCodigoPostalNavigation)
                .Where(c => c.IdCodigoPostalNavigation.CodigoPostal1 == codigoPostal);
        }

        public Colonia ConsultarPorCodigoPostal(string codigoPostal)
        {
            return context.Colonia
                .Include(c => c.IdCodigoPostalNavigation)
                .Where(c => c.IdCodigoPostalNavigation.CodigoPostal1 == codigoPostal)
                .FirstOrDefault();
        }

    }
}
