using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class AreaRepository: Repository<Area>, IAreaRepository
    {
        public AreaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Area Consultar(int idArea)
        {
            return context.Area.Where(a => a.IdArea == idArea).FirstOrDefault();
        }

        public AreaDto ConsultarDto(int idArea)
        {
            return context.Area
                    .Where(a => a.IdArea == idArea)
                    .Select(a => new AreaDto (
                        a.IdArea,
                        a.Clave,
                        a.Nombre,
                        a.IdCompania))
                    .FirstOrDefault();
        }

        public IEnumerable<AreaDto> ConsultarParaSelector(int idCompania)
        {
            int? idCompaniaBase = context.Compania.FirstOrDefault(cc => cc.Clave == GeneralConstant.ClaveCompaniaBase)?.IdCompania;

            return context.Area
                        .Where(a => a.IdCompania == idCompania || a.IdCompania == idCompaniaBase)
                        .OrderBy(a => a.Nombre)
                        .Select(a => new AreaDto(
                            a.IdArea,
                            a.Clave,
                            a.Nombre,
                            a.IdCompania))
                        .ToList();
        }

        public Area Consultar(string nombre)
        {
            return context.Area
                        .Where(a => a.Nombre == nombre)
                        .FirstOrDefault();
        }

        public Area ConsultarPorClave(string clave)
        {
            return context.Area
                       .Where(a => a.Clave == clave)
                       .FirstOrDefault();
        }

        public Area ConsultarExistencia(string clave, string nombre)
        {
            return context.Area
             .Where(d => d.Clave == clave || d.Nombre.ToLower() == nombre.ToLower())
             .FirstOrDefault();
        }

        public Area ConsultarDependencias(int idArea)
        {
            return context.Area
                .Include(d => d.Presentacion)
                .Where(d => d.IdArea == idArea)
                .FirstOrDefault();
        }
    }
}
