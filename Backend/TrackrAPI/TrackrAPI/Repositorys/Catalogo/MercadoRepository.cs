using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class MercadoRepository : Repository<Mercado>, IMercadoRepository
    {
        public MercadoRepository(TrackrContext context) : base(context)
        {
        }

        public IEnumerable<Mercado> ConsultarTodos()
        {
            return context.Mercado
                .ToList();
        }

        public IEnumerable<MercadoGridDto> ConsultarParaGrid()
        {
            return context.Mercado
                .Select(m => new MercadoGridDto
                {
                    IdMercado = m.IdMercado,
                    Nombre = m.Nombre,
                    NombreGiroComercial = m.IdGiroComercialNavigation.Nombre
                });
        }

        public Mercado Consultar(int idMercado)
        {
            return context.Mercado
                .Where(m => m.IdMercado == idMercado)
                .FirstOrDefault();
        }


        public MercadoFormularioDto ConsultarFormularioDto(int idMercado)
        {
            return context.Mercado
                .Where(m => m.IdMercado == idMercado)
                .Select(m => new MercadoFormularioDto
                {
                    IdMercado = m.IdMercado,
                    Clave = m.Clave,
                    Nombre = m.Nombre,
                    IdGiroComercial = m.IdGiroComercial,
                    Companias = m.MercadoCompania
                        .Select(mc => new CompaniaDto
                        {
                            IdCompania = mc.IdCompania,
                            Nombre = mc.IdCompaniaNavigation.Nombre
                        })
                        .ToList()
                })
                .FirstOrDefault();
        }
    }
}