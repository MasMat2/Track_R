using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class CompaniaLogotipoRepository : Repository<CompaniaLogotipo>, ICompaniaLogotipoRepository
    {
        public CompaniaLogotipoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public CompaniaLogotipo Consultar(int idCompaniaLogotipo)
        {
            return context.CompaniaLogotipo
                .Where(cl => cl.IdCompaniaLogotipo == idCompaniaLogotipo)
                .FirstOrDefault();
        }

        public CompaniaLogotipoDto ConsultarDto(int idCompaniaLogotipo)
        {
            return context.CompaniaLogotipo
                .Where(cl => cl.IdCompaniaLogotipo == idCompaniaLogotipo)
                .Select(hl => new CompaniaLogotipoDto
                {
                    IdCompaniaLogotipo = hl.IdCompaniaLogotipo,
                    IdCompania = hl.IdCompania,
                    NombreImagen = hl.Imagen,
                    TipoMime = hl.TipoMime
                })
                .FirstOrDefault();
        }

        public CompaniaLogotipoDto ConsultarPorCompania(int idCompania)
        {
            return context.CompaniaLogotipo
                .Where(cl => cl.IdCompania == idCompania)
                .Select(cl => new CompaniaLogotipoDto
                {
                    IdCompaniaLogotipo = cl.IdCompaniaLogotipo,
                    IdCompania = cl.IdCompania,
                    Src = GeneralConstant.RutaArchivoCompaniaLogotipo + cl.IdCompaniaLogotipo,
                    TipoMime = cl.TipoMime,
                    NombreImagen = cl.Imagen
                })
                .FirstOrDefault();
        }

    }
}
