using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.GestionExamen;

public class NivelExamenRepository : Repository<NivelExamen>, INivelExamenRepository
{
    public NivelExamenRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public NivelExamenDto? Consultar(int idNivelExamen)
    {
        return context.NivelExamen
            .Where(p => p.IdNivelExamen == idNivelExamen)
            .OrderBy(p => p.Clave)
            .Select(p => new NivelExamenDto
            {
                IdNivelExamen = p.IdNivelExamen,
                Clave = p.Clave ?? string.Empty,
                Descripcion = p.Descripcion ?? string.Empty,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus
            })
            .FirstOrDefault();
    }

    public IEnumerable<NivelExamenGridDto> ConsultarGeneral()
    {
        return context.NivelExamen
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new NivelExamenGridDto
            {
                IdNivelExamen = p.IdNivelExamen,
                Clave = p.Clave ?? string.Empty,
                Descripcion = p.Descripcion ?? string.Empty,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus
            })
            .ToList();
    }

    public IEnumerable<NivelExamenGridDto> ConsultarTodosParaSelector()
    {
        return context.NivelExamen
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new NivelExamenGridDto
            {
                IdNivelExamen = p.IdNivelExamen,
                Clave = p.Clave ?? string.Empty,
                Descripcion = p.Descripcion ?? string.Empty,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus
            })
            .ToList();
    }

    public NivelExamen? ConsultarConDependencias(int idNivelExamen)
    {
        return context.NivelExamen
            .Include(p => p.Reactivo)
            .Where(p => p.IdNivelExamen == idNivelExamen)
            .FirstOrDefault();
    }

    public NivelExamen? ConsultarDuplicado(NivelExamen nivelExamen)
    {
        return context.NivelExamen
            .Where(g => (g.Clave == nivelExamen.Clave || g.Descripcion == nivelExamen.Descripcion) && g.IdNivelExamen != nivelExamen.IdNivelExamen)
            .FirstOrDefault();
    }
}
