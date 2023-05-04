using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class PaisRepository : Repository<Pais>, IPaisRepository
    {
        public PaisRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<PaisDto> ConsultarTodosParaSelector()
        {
            return context.Pais
                .OrderBy(p => p.Nombre)
                .Select(p => new PaisDto
                {
                    IdPais = p.IdPais,
                    Clave = p.Clave,
                    Nombre = p.Nombre
                })
                .ToList();
        }

        public PaisDto ConsultarDto(string clave)
        {
            return context.Pais
                .Where(p => p.Clave.Equals(clave))
                .Select(p => new PaisDto
                {
                    IdPais = p.IdPais,
                    Nombre = p.Nombre
                })
                .FirstOrDefault();
        }

        public PaisDto ConsultarDto(int idPais)
        {
            return context.Pais
                .Where(p => p.IdPais == idPais)
                .Select(p => new PaisDto
                {
                    IdPais = p.IdPais,
                    Clave = p.Clave,
                    Nombre = p.Nombre
                })
                .FirstOrDefault();
        }

        public Pais Consultar(int idPais)
        {
            return context.Pais
                .Where(p => p.IdPais == idPais)
                .FirstOrDefault();
        }

        public Pais ConsultarConDependencias(int idPais)
        {
            return context.Pais
                .Where(p => p.IdPais == idPais)
                .FirstOrDefault();
        }

        public IEnumerable<PaisGridDto> ConsultarGeneral()
        {
            return context.Pais
                .OrderBy(p => p.Nombre)
                .Select(p => new PaisGridDto
                {
                    IdPais = p.IdPais,
                    Clave = p.Clave,
                    Nombre = p.Nombre
                })
                .ToList();
        }

        public Pais Consultar(string nombre)
        {
            return context.Pais
                .Where(p => p.Nombre.ToLower() == nombre.ToLower())
                .FirstOrDefault();
        }
    }
}
