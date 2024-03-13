using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class TituloAcademicoRepository : Repository<TituloAcademico>, ITituloAcademicoRepository
    {
        public TituloAcademicoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<TituloAcademicoSelectorDto> ConsultarTodosParaSelector()
        {
            return context.TituloAcademico
                      .OrderBy(e => e.Nombre)
                      .Select(e => new TituloAcademicoSelectorDto
                      {
                        IdTituloAcademico = e.IdTituloAcademico,
                        Nombre = e.Nombre
                      })
                      .ToList();
        }

        public TituloAcademico Consultar(int idTituloAcademico)
        {
            return context.TituloAcademico.Where(e => e.IdTituloAcademico == idTituloAcademico).FirstOrDefault();
        }

        public TituloAcademicoDto ConsultarDto(int idTituloAcademico)
        {
            return context.TituloAcademico
                      .Where(e => e.IdTituloAcademico == idTituloAcademico)
                      .Select(e => new TituloAcademicoDto {
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          IdTituloAcademico = e.IdTituloAcademico})
                      .FirstOrDefault();
        }

        public IEnumerable<TituloAcademicoGridDto> ConsultarTodosParaGrid()
        {
            return context.TituloAcademico
                      .OrderBy(e => e.Nombre)
                      .Select(e => new TituloAcademicoGridDto
                      {
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          IdTituloAcademico = e.IdTituloAcademico
                      })
                      .OrderBy( e => e.Clave)
                      .ToList();
        }

        public TituloAcademico Consultar(string clave)
        {
            return context.TituloAcademico
                      .Where(e => e.Clave.ToLower() == clave.ToLower())
                      .FirstOrDefault();
        }
        public TituloAcademico ConsultarPorNombre(string nombre)
        {
            return context.TituloAcademico
                      .Where(e => e.Nombre.ToLower() == nombre.ToLower())
                      .FirstOrDefault();
        }

        public TituloAcademico ConsultarDependencias(int idTituloAcademico)
        {
            return context.TituloAcademico
                .Include(e => e.Usuario)
                .Where(e => e.IdTituloAcademico == idTituloAcademico)
                .FirstOrDefault();
        }
    }
}
