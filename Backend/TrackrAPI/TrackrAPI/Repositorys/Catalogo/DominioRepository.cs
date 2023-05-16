using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class DominioRepository : Repository<Dominio>, IDominioRepository
    {
        public DominioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }
        public DominioDto ConsultarDto(int idDominio)
        {
            return context.Dominio
                      .Where(d => d.IdDominio == idDominio)
                      .Select(d => new DominioDto(
                          d.IdDominio,
                          d.Nombre,
                          d.Descripcion,
                          d.TipoDato,
                          d.TipoCampo,
                          d.LongitudMinima,
                          d.LongitudMaxima,
                          d.ValorMinimo,
                          d.ValorMaximo,
                          d.FechaMinima,
                          d.FechaMaxima))
                      .FirstOrDefault();
        }

        public Dominio Consultar(int idDominio)
        {
            return context.Dominio.Where(d => d.IdDominio == idDominio).FirstOrDefault();
        }

        public Dominio Consultar(string nombre)
        {
            return context.Dominio
                      .Where(d => d.Nombre.ToLower() == nombre.ToLower())
                      .FirstOrDefault();
        }

        public IEnumerable<DominioGridDto> ConsultarTodosParaGrid(int idUsuarioSesion)
        {

            return context.Dominio
                      .OrderBy(d => d.Nombre)
                      .Select(d => new DominioGridDto(
                          d.IdDominio,
                          d.Nombre,
                          d.Descripcion,
                          d.TipoDato,
                          d.TipoCampo,
                          d.LongitudMinima,
                          d.LongitudMaxima,
                          d.ValorMinimo,
                          d.ValorMaximo,
                          d.FechaMinima,
                          d.FechaMaxima))
                      .ToList();
        }

        public Dominio ConsultarDependencias(int idDominio)
        {
            return context.Dominio
                        .Include(e => e.DominioDetalle)
                        .Include(e => e.ExpedienteCampo)
                       .Where(e => e.IdDominio == idDominio)
                       .FirstOrDefault();
        }

        public IEnumerable<DominioDto> consultarTodosParaSelector()
        {
            return context.Dominio
                      .OrderBy(e => e.Nombre)
                      .Select(e => new DominioDto
                      {
                          IdDominio = e.IdDominio,
                          Nombre = e.Nombre,
                          Descripcion = e.Descripcion
                      })
                      .ToList();
        }
    }
}
