using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.CanalDistribucion;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ListaPrecioClinicaRepository : Repository<ListaPrecioClinica>, IListaPrecioClinicaRepository
    {
        public ListaPrecioClinicaRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public ListaPrecioClinica Consultar(int idListaPrecioClinica)
        {
            return context.ListaPrecioClinica
                .Where(lpc => lpc.IdListaPrecioClinica == idListaPrecioClinica)
                .FirstOrDefault();
        }


        public IEnumerable<ListaPrecioClinicaDto> ConsultarPorListaPrecio(int idListaPrecio)
        {
            return context.ListaPrecioClinica
                .Where(lpc => lpc.IdListaPrecio == idListaPrecio)
                .Select(lpc => new ListaPrecioClinicaDto
                {
                    IdListaPrecioClinica = lpc.IdListaPrecioClinica,
                    IdListaPrecio = lpc.IdListaPrecio,
                    IdClinica = lpc.IdClinica
                })
                .ToList();
        }

        //Canal Distribución

        public IEnumerable<PrecioListaGeneralDto> ConsultarParaCanal(int idCompania)
        {
            return context.ListaPrecioClinica
                .Where(lpc => lpc.IdClinicaNavigation.IdCompania == idCompania)
                .Select(plg => new PrecioListaGeneralDto
                {
                    idPrecioListaGeneral = plg.IdListaPrecio,
                    nombre = plg.IdListaPrecioNavigation.Nombre,
                }).ToList();
        }
    }
}
