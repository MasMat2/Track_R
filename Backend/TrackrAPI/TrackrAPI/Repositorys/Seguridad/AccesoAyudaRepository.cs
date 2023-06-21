using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class AccesoAyudaRepository : Repository<AccesoAyuda>, IAccesoAyudaRepository
    {
        public AccesoAyudaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }
        public AccesoAyudaDto ConsultarDto(int idAccesoAyuda)
        {
            return
                context.AccesoAyuda
                .Where(a => a.IdAccesoAyuda == idAccesoAyuda)
                .Select(a => new AccesoAyudaDto
                {
                    IdAccesoAyuda = a.IdAccesoAyuda,
                    IdAcceso = a.IdAcceso,
                    EtiquetaCampo = a.EtiquetaCampo,
                    DescripcionAyuda = a.DescripcionAyuda,
                    NombreArchivo = a.NombreArchivo,
                    Imagen = a.Imagen,
                    TipoMime = a.TipoMime,
                    Orden = a.Orden,
                    IdAyudaSeccion = a.IdAyudaSeccion
                })
                .FirstOrDefault();
        }
        public IEnumerable<AccesoAyudaDto> ConsultarPorAcceso(int idAcceso)
        {
            return
                context.AccesoAyuda
                .Where(a => a.IdAcceso == idAcceso)
                .Select(a => new AccesoAyudaDto
                {
                    IdAccesoAyuda = a.IdAccesoAyuda,
                    IdAcceso = a.IdAcceso,
                    EtiquetaCampo = a.EtiquetaCampo,
                    DescripcionAyuda = a.DescripcionAyuda,
                    NombreArchivo = a.NombreArchivo,
                    Imagen = a.Imagen,
                    TipoMime = a.TipoMime,
                    Orden = a.Orden,
                    IdAyudaSeccion = a.IdAyudaSeccion,
                    NombreAyudaSeccion = a.IdAyudaSeccionNavigation.Nombre
                })
                .ToList().OrderBy(a => a.Orden);
        }


        public AccesoAyuda Consultar(int idAccesoAyuda)
        {
            return context.AccesoAyuda
                .Where(a => a.IdAccesoAyuda == idAccesoAyuda)
                .FirstOrDefault();
        }
        public AccesoAyuda ConsultarPorNombre(string nombreArchivo)
        {
            return context.AccesoAyuda
                .Where(a => a.NombreArchivo == nombreArchivo)
                .FirstOrDefault();
        }
        public AccesoAyuda ConsultarPorOrden(int? orden, int idAccesso)
        {
            return context.AccesoAyuda
                .Where(a => a.Orden == orden && a.IdAcceso == idAccesso)
                .FirstOrDefault();
        }

        public AccesoAyuda ConsultarDependencia(int idAccesoAyuda)
        {
            return context.AccesoAyuda
                .Include(a => a.IdAccesoNavigation)
                .Where(a => a.IdAccesoAyuda == idAccesoAyuda)
                .FirstOrDefault();
        }
    }
}
