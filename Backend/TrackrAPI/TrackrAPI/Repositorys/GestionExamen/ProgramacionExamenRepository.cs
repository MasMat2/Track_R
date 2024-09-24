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
                FechaExamen = p.FechaExamen.Value,
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
        
        var prograexamens = context.ProgramacionExamen
            .Where(p => p.Estatus == true && p.IdUsuarioResponsableNavigation.IdCompania == idCompania)
            .OrderBy(p => p.Clave)
            .Select(p => new ProgramacionExamenGridDto
            {
                IdProgramacionExamen = p.IdProgramacionExamen,
                Clave = p.Clave ?? string.Empty,
                UsuarioResponsable = p.IdUsuarioResponsableNavigation.Nombre + " " + p.IdUsuarioResponsableNavigation.ApellidoPaterno,
                TipoExamen = p.IdTipoExamenNavigation.Nombre ?? string.Empty,
                FechaExamen = p.FechaExamen.Value,
                HoraExamen = p.HoraExamen,
                Duracion = p.Duracion,
                PorcentajeAvance = CalcularPorcentajeAvance(p.IdProgramacionExamen, p.Examen.ToList()),
                Estatus = p.Estatus ?? false
            })
            .ToList();

        return prograexamens;

    }
    public IEnumerable<ProgramacionExamenGridDto> ConsultarGeneral(int idCompania, List<int> idUsuarioSesion)
    {
        
        var prograexamens = context.ProgramacionExamen
            .Where(p => p.Estatus == true && p.IdUsuarioResponsableNavigation.IdCompania == idCompania && idUsuarioSesion.Contains(p.IdUsuarioResponsable))
            .OrderBy(p => p.Clave)
            .Select(p => new ProgramacionExamenGridDto
            {
                IdProgramacionExamen = p.IdProgramacionExamen,
                Clave = p.Clave ?? string.Empty,
                UsuarioResponsable = p.IdUsuarioResponsableNavigation.Nombre + " " + p.IdUsuarioResponsableNavigation.ApellidoPaterno,
                TipoExamen = p.IdTipoExamenNavigation.Nombre ?? string.Empty,
                FechaExamen = p.FechaExamen.Value,
                HoraExamen = p.HoraExamen,
                Duracion = p.Duracion,
                PorcentajeAvance = CalcularPorcentajeAvance(p.IdProgramacionExamen, p.Examen.ToList()),
                Estatus = p.Estatus ?? false
            })
            .ToList();

        return prograexamens;

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

    private static string CalcularPorcentajeAvance(int idProgramacionExamen, List<Examen> participantes)
    {
        var porcentaje = "";
        var todos = participantes.Count();
        var terminados = participantes.Count(e => e.IdEstatusExamen == 3);
        porcentaje = $"{terminados}/{todos}";

        return porcentaje;
    }

}
