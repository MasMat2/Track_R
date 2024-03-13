using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class TipoCompaniaRepository : Repository<TipoCompania>, ITipoCompaniaRepository
    {
        public TipoCompaniaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<TipoCompaniaSelectorDto> ConsultarParaSelector()
        {
            var productoTipoList = context.TipoCompania
                                    .Select(tc => new TipoCompaniaSelectorDto
                                    {
                                        IdTipoCompania = tc.IdTipoCompania,
                                        Nombre = tc.Nombre,
                                    }).ToList();

            return productoTipoList;
        }
        public TipoCompania Consultar(int idTipoCompania)
        {
            return context.TipoCompania
                .Where(tc => tc.IdTipoCompania == idTipoCompania)
                .Select(tc => new TipoCompania
                {
                    IdTipoCompania = tc.IdTipoCompania,
                    Clave = tc.Clave,
                    Nombre = tc.Nombre
                }).FirstOrDefault();
        }
        public TipoCompania ConsultarPorClave(string clave)
        {
            return context.TipoCompania
                .Where(tc => tc.Clave == clave)
                .Select(tc => new TipoCompania
                {
                    IdTipoCompania = tc.IdTipoCompania,
                    Clave = tc.Clave,
                    Nombre = tc.Nombre
                }).FirstOrDefault();
        }
        public TipoCompania ConsultarPorNombre(string nombre)
        {
            return context.TipoCompania
                .Where(tc => tc.Nombre == nombre)
                .Select(tc => new TipoCompania
                {
                    IdTipoCompania = tc.IdTipoCompania,
                    Clave = tc.Clave,
                    Nombre = tc.Nombre
                }).FirstOrDefault();
        }

        public IEnumerable<TipoCompania> ConsultarTodosParaGrid()
        {
            return context.TipoCompania
                .Select(tc => new TipoCompania
                {
                    IdTipoCompania = tc.IdTipoCompania,
                    Clave = tc.Clave,
                    Nombre = tc.Nombre
                }).ToList();
        }
        public TipoCompania ConsultarDependencias(int idTipoCompania)
        {
            return context.TipoCompania
                .Where(tc => tc.IdTipoCompania == idTipoCompania)
                .Select(tc => new TipoCompania
                {
                    Compania = tc.Compania,
                    Perfil = tc.Perfil,
                }).FirstOrDefault();
        }
    }
}
