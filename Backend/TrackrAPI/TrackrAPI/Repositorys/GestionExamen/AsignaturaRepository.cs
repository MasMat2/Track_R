using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public class AsignaturaRepository : Repository<Asignatura>, IAsignaturaRepository
{
    public AsignaturaRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }
    public AsignaturaDto? Consultar(int idAsignatura)
    {
        return context.Asignatura
            .Where(p => p.IdAsignatura == idAsignatura)
            .OrderBy(p => p.Clave)
            .Select(p => new AsignaturaDto
            {
                IdAsignatura = p.IdAsignatura,
                Clave = p.Clave ?? string.Empty,
                Descripcion = p.Descripcion ?? string.Empty,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus
            })
            .FirstOrDefault();
    }

    public IEnumerable<AsignaturaGridDto> ConsultarGeneral()
    {
        return context.Asignatura
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new AsignaturaGridDto
            {
                IdAsignatura = p.IdAsignatura,
                Clave = p.Clave ?? string.Empty,
                Descripcion = p.Descripcion ?? string.Empty
            })
            .ToList();
    }

    public IEnumerable<AsignaturaGridDto> ConsultarTodosParaSelector()
    {
        return context.Asignatura
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new AsignaturaGridDto
            {
                IdAsignatura = p.IdAsignatura,
                Clave = p.Clave ?? string.Empty,
                Descripcion = p.Descripcion ?? string.Empty
            })
            .ToList();
    }

    public Asignatura? ConsultarConDependencias(int idAsignatura)
    {
        return context.Asignatura
            .Include(a => a.Reactivo)
            .Where(a => a.IdAsignatura == idAsignatura)
            .FirstOrDefault();
    }

    public Asignatura? ConsultarDuplicado(Asignatura asignatura)
    {
        return context.Asignatura
            .Where(a => (
                a.Clave == asignatura.Clave ||
                a.Descripcion == asignatura.Descripcion)
                && a.IdAsignatura != asignatura.IdAsignatura)
            .FirstOrDefault();
    }
}
