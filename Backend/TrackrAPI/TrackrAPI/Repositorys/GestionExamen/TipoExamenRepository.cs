using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.GestionExamen;

public class TipoExamenRepository : Repository<TipoExamen>, ITipoExamenRepository
{
    public TipoExamenRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public TipoExamenDto? Consultar(int idTipoExamen)
    {
        return context.TipoExamen
            .Where(p => p.IdTipoExamen == idTipoExamen)
            .OrderBy(p => p.Clave)
            .Select(p => new TipoExamenDto
            {
                IdTipoExamen = p.IdTipoExamen,
                Clave = p.Clave ?? string.Empty,
                Nombre = p.Nombre ?? string.Empty,
                TotalPreguntas = p.TotalPreguntas,
                Duracion = p.Duracion,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus
            })
            .FirstOrDefault();
    }

    public IEnumerable<TipoExamenGridDto> ConsultarGeneral()
    {
        return context.TipoExamen
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new TipoExamenGridDto
            {
                IdTipoExamen = p.IdTipoExamen,
                Clave = p.Clave ?? string.Empty,
                Nombre = p.Nombre ?? string.Empty,
                TotalPreguntas = p.TotalPreguntas,
                Duracion = p.Duracion
            })
            .ToList();
    }

    public IEnumerable<TipoExamenGridDto> ConsultarTodosParaSelector()
    {
        return context.TipoExamen
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new TipoExamenGridDto
            {
                IdTipoExamen = p.IdTipoExamen,
                Clave = p.Clave ?? string.Empty,
                Nombre = p.Nombre ?? string.Empty,
            })
            .ToList();
    }

    public TipoExamen? ConsultarConDependencias(int idTipoExamen)
    {
        return context.TipoExamen
            .Include(p => p.ContenidoExamen
                .Where(a => a.Estatus == true))
            .Where(p => p.IdTipoExamen == idTipoExamen)
            .FirstOrDefault();
    }

    public TipoExamen? ConsultarDuplicado(TipoExamen tipoExamen)
    {
        return context.TipoExamen
            .Where(p => (p.Clave == tipoExamen.Clave || p.Nombre == tipoExamen.Nombre) && p.IdTipoExamen != tipoExamen.IdTipoExamen)
            .FirstOrDefault();
    }
}
