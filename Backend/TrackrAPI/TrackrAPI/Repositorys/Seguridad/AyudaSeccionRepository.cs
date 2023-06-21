using DocumentFormat.OpenXml.InkML;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class AyudaSeccionRepository: Repository<AyudaSeccion>, IAyudaSeccionRepository
    {
        public AyudaSeccionRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public AyudaSeccion Consultar(int idAyudaSeccion)
        {
            return context.AyudaSeccion
                .Where(s => s.IdAyudaSeccion == idAyudaSeccion)
                .FirstOrDefault();
        }

        public IEnumerable<AyudaSeccionSelectorDto> ConsultarParaSelector()
        {
            return context.AyudaSeccion
                .Select(s => new AyudaSeccionSelectorDto
                {
                    IdAyudaSeccion = s.IdAyudaSeccion,
                    Clave = s.Clave,
                    Nombre = s.Nombre,
                    Orden = s.Orden
                }).ToList();
        }

        public AyudaSeccion ConsultarPorClave(string clave)
        {
            return context.AyudaSeccion
                .Where(s => s.Clave.ToLower() == clave.ToLower())
                .FirstOrDefault();
        }

        public AyudaSeccion ConsultarPorNombre(string nombre)
        {
            return context.AyudaSeccion
                .Where(s => s.Nombre.ToLower() == nombre.ToLower())
                .FirstOrDefault();
        }

        public AyudaSeccion ConsultarPorOrden(int orden)
        {
            return context.AyudaSeccion
                .Where(s => s.Orden == orden)
                .FirstOrDefault();
        }
    }
}
