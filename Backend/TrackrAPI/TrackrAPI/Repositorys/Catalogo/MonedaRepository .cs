using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class MonedaRepository : Repository<Moneda>, IMonedaRepository
    {
        public MonedaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Moneda ConsultarPorId(int idMoneda)
        {
            return context.Moneda.Where(c => c.IdMoneda == idMoneda).FirstOrDefault();
        }
        public Moneda ConsultarPorNombre(string nombre)
        {
            return context.Moneda.Where(c => c.Nombre == nombre).FirstOrDefault();
        }
        public Moneda ConsultarPorClave(string clave)
        {
            return context.Moneda.Where(c => c.Clave == clave).FirstOrDefault();
        }


        public MonedaDto ConsultarDto(int idMoneda)
        {
            return context.Moneda
                      .Where(c => c.IdMoneda == idMoneda)
                      .Select(c => new MonedaDto {
                          IdMoneda = c.IdMoneda,
                          Clave = c.Clave,
                          Nombre = c.Nombre,
                          Simbolo = c.Simbolo,
                      })
                      .FirstOrDefault();
        }

        public IEnumerable<MonedaGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return context.Moneda
                      .Select(c => new MonedaGridDto
                      {
                          IdMoneda = c.IdMoneda,
                          Clave = c.Clave,
                          Nombre = c.Nombre,
                          Simbolo = c.Simbolo,
                      })
                      .ToList();
        }

        public IEnumerable<MonedaSelectorDto> ConsultarParaSelector()
        {
            return context.Moneda
                      .Select(c => new MonedaSelectorDto
                      {
                          IdMoneda = c.IdMoneda,
                          Clave = c.Clave,
                          Nombre = c.Nombre
                      })
                      .ToList();
        }
    }
}
