using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        public EstadoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Estado Consultar(int idEstado)
        {
            return context.Estado.Where(e => e.IdEstado == idEstado).FirstOrDefault();
        }

        public EstadoDto ConsultarDto(int idEstado)
        {
            return context.Estado
                      .Where(e => e.IdEstado == idEstado)
                      .Select(e => new EstadoDto
                      {
                          IdEstado = e.IdEstado,
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          IdPais = e.IdPais
                      })
                      .FirstOrDefault();
        }

        public IEnumerable<EstadoGridDto> ConsultarTodosParaGrid()
        {
            return context.Estado
                      .OrderBy(e => e.Nombre)
                      .Select(e => new EstadoGridDto
                      {
                          IdEstado = e.IdEstado,
                          Clave = e.Clave,
                          Nombre = e.Nombre,
                          NombrePais = e.IdPaisNavigation.Nombre
                      })
                      .ToList();
        }

        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            return context.Estado
                      .Where(e => e.IdPais == idPais)
                      .OrderBy(e => e.Nombre)
                      .Select(e => new EstadoSelectorDto(
                          e.IdEstado,
                          e.Nombre
                       ))
                      .ToList();
        }

        public IEnumerable<Estado> ConsultarPorPais(int idPais)
        {
            return context.Estado.Where(e => e.IdPais == idPais).ToList();
        }

        public Estado Consultar(string nombre, int idPais)
        {
            return context.Estado
                      .Where(e => e.Nombre.ToLower() == nombre.ToLower() && e.IdPais == idPais)
                      .FirstOrDefault();
        }

        public IEnumerable<EstadoSelectorDto> ConsultarGeneral()
        {
            return context.Estado
                      .OrderBy(e => e.Nombre)
                      .Select(e => new EstadoSelectorDto(
                          e.IdEstado,
                          e.Nombre
                       ))
                      .ToList();
        }

        public Estado ConsultarDependencias(int idEstado)
        {
            return context.Estado
                .Where(e => e.IdEstado == idEstado)
                .Select(e => new Estado
                {
                    Municipio = e.Municipio
                }).FirstOrDefault();
        }
    }
}
