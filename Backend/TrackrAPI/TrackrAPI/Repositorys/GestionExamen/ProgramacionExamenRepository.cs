using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.GestionExamen;

public class ProgramacionExamenRepository : Repository<ProgramacionExamen>, IProgramacionExamenRepository
{
    public ProgramacionExamenRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public ProgramacionExamenDto? Consultar(int idProgramacionExamen)
    {
        return context.ProgramacionExamen
            .Where(p => p.IdProgramacionExamen == idProgramacionExamen)
            .OrderBy(p => p.Clave)
            .Select(p => new ProgramacionExamenDto
            {
                IdProgramacionExamen = p.IdProgramacionExamen,
                IdTipoExamen = p.IdTipoExamen,
                IdUsuarioResponsable = p.IdUsuarioResponsable,
                Clave = p.Clave ?? string.Empty,
                Duracion = p.Duracion,
                CantidadParticipantes = p.CantidadParticipantes,
                FechaExamen = p.FechaExamen,
                HoraExamen = p.HoraExamen,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus ?? false,
                Promedio = (
                    context.Examen
                    .Where(x => x.IdProgramacionExamen == idProgramacionExamen)
                    .Average(x => x.Resultado)),
                IdProyectoElementoTecnica = p.IdProyectoElementoTecnica
            })
            .FirstOrDefault();
    }

    public IEnumerable<ProgramacionExamenGridDto> ConsultarGeneral(int idCompania)
    {
        return context.ProgramacionExamen
            .Where(p => p.Estatus == true && p.IdUsuarioResponsableNavigation.IdCompania == idCompania)
            .OrderBy(p => p.Clave)
            .Select(p => new ProgramacionExamenGridDto
            {
                IdProgramacionExamen = p.IdProgramacionExamen,
                Clave = p.Clave ?? string.Empty,
                UsuarioResponsable = p.IdUsuarioResponsableNavigation.Nombre + " " + p.IdUsuarioResponsableNavigation.ApellidoPaterno,
                TipoExamen = p.IdTipoExamenNavigation.Nombre ?? string.Empty,
                FechaExamen = p.FechaExamen,
                HoraExamen = p.HoraExamen,
                Duracion = p.Duracion,
                CantidadParticipantes = p.CantidadParticipantes,
                Estatus = p.Estatus ?? false
            })
            .ToList();
    }

    public IEnumerable<ProgramacionExamenGridDto> ConsultarTodosParaSelector()
    {
        return context.ProgramacionExamen
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new ProgramacionExamenGridDto
            {
                IdProgramacionExamen = p.IdProgramacionExamen,
                TipoExamen = p.IdTipoExamenNavigation.Nombre ?? string.Empty,
                FechaExamen = p.FechaExamen,
                HoraExamen = p.HoraExamen
            })
            .ToList();
    }

    public ProgramacionExamen? ConsultarConDependencias(int idProgramacionExamen)
    {
        return context.ProgramacionExamen
            .Include(p => p.Examen)
            .Where(p => p.IdProgramacionExamen == idProgramacionExamen)
            .FirstOrDefault();
    }
}
