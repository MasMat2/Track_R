using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.GestionExamen;

public class ExamenRepository : Repository<Examen>, IExamenRepository
{
    public ExamenRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public Examen? Consultar(int idExamen)
    {
        return context.Examen
            .Where(p => p.IdExamen == idExamen)
            .Include(p => p.ExamenReactivo)
            .Include(p => p.IdProgramacionExamenNavigation)
            .Include(p => p.IdProgramacionExamenNavigation.IdTipoExamenNavigation)
            .OrderBy(p => p.IdExamen)
            .FirstOrDefault();
    }

    public IEnumerable<Examen> ConsultarGeneral(int idProgramacionExamen)
    {
        return context.Examen
            .Where(p => p.IdProgramacionExamen == idProgramacionExamen && p.Estatus == true)
            .OrderBy(p => p.IdExamen)
            .ToList();
    }

    public IEnumerable<Examen> ConsultarTodosParaSelector(int idProgramacionExamen)
    {
        return context.Examen
            .Where(p => p.IdProgramacionExamen == idProgramacionExamen && p.Estatus == true)
            .OrderBy(p => p.IdExamen)
            .ToList();
    }

    public IEnumerable<ExamenGridDto> ConsultarMisExamenes(int idUsuario)
    {
        return context.Examen
            .Where(p => p.IdUsuarioParticipante == idUsuario && p.IdEstatusExamen == 1 && p.Estatus == true
                    && p.IdProgramacionExamenNavigation.FechaExamen >= System.DateTime.Now.Date)
            .OrderBy(p => p.IdExamen)
            .Select(p => new ExamenGridDto {
                IdExamen = p.IdExamen,
                TipoExamen = p.IdProgramacionExamenNavigation.IdTipoExamenNavigation.Nombre ?? string.Empty,
                FechaExamen = p.IdProgramacionExamenNavigation.FechaExamen.Value,
                HoraExamen = p.IdProgramacionExamenNavigation.HoraExamen,
                Duracion = p.IdProgramacionExamenNavigation.Duracion,
                TotalPreguntas = p.IdProgramacionExamenNavigation.IdTipoExamenNavigation.TotalPreguntas
            })
            .ToList();
    }

    public IEnumerable<ExamenGridDto> ConsultarMisExamenesContestados(int idUsuario)
    {
        return context.Examen
            .Where(p => p.IdUsuarioParticipante == idUsuario && p.IdEstatusExamen == 3 && p.Estatus == true)
            .OrderByDescending(p => p.IdProgramacionExamenNavigation.FechaExamen)
            .Select(p => new ExamenGridDto
            {
                IdExamen = p.IdExamen,
                TipoExamen = p.IdProgramacionExamenNavigation.IdTipoExamenNavigation.Nombre ?? string.Empty,
                FechaExamen = p.IdProgramacionExamenNavigation.FechaExamen,
                HoraExamen = p.IdProgramacionExamenNavigation.HoraExamen,
                Duracion = p.IdProgramacionExamenNavigation.Duracion,
                TotalPreguntas = p.IdProgramacionExamenNavigation.IdTipoExamenNavigation.TotalPreguntas
            })
            .ToList();
    }

    public IEnumerable<CuestionariosPorResponsableDto> ConsultarExamenesPendientesPorResponsable(int idUsuario)
    {
        return context.Examen
            .Where(p => p.IdProgramacionExamenNavigation.IdUsuarioResponsable == idUsuario && p.IdEstatusExamen == 1 && p.Estatus == true
                    && p.IdProgramacionExamenNavigation.FechaExamen >= System.DateTime.Now.Date)
            .OrderBy(p => p.IdExamen)
            .Select(p => new CuestionariosPorResponsableDto
            {
                IdExamen = p.IdExamen,
                clave = p.IdProgramacionExamenNavigation.Clave ?? string.Empty,
                NombreUsuario = p.IdUsuarioParticipanteNavigation.Nombre + " " + p.IdUsuarioParticipanteNavigation.ApellidoPaterno + " " + p.IdUsuarioParticipanteNavigation.ApellidoMaterno,
                IdProgramacionExamen = p.IdProgramacionExamen,
                IdUsuarioParticipante = p.IdUsuarioParticipante,
                Resultado = p.Resultado,
                PreguntasCorrectas = p.PreguntasCorrectas
            })
            .ToList();
    }

    public IEnumerable<CuestionariosPorResponsableDto> ConsultarExamenesVencidosPorResponsable(int idUsuario)
    {
        return context.Examen
            .Where(p => p.IdProgramacionExamenNavigation.IdUsuarioResponsable == idUsuario && p.IdEstatusExamen == 1 && p.Estatus == true
                    && p.IdProgramacionExamenNavigation.FechaExamen < System.DateTime.Now.Date)
            .OrderBy(p => p.IdExamen)
            .Select(p => new CuestionariosPorResponsableDto
            {
                IdExamen = p.IdExamen,
                clave = p.IdProgramacionExamenNavigation.Clave ?? string.Empty,
                NombreUsuario = p.IdUsuarioParticipanteNavigation.Nombre + " " + p.IdUsuarioParticipanteNavigation.ApellidoPaterno + " " + p.IdUsuarioParticipanteNavigation.ApellidoMaterno,
                IdProgramacionExamen = p.IdProgramacionExamen,
                IdUsuarioParticipante = p.IdUsuarioParticipante,
                Resultado = p.Resultado,
                PreguntasCorrectas = p.PreguntasCorrectas
            })
            .ToList();
    }

    public IEnumerable<CuestionariosPorResponsableDto> ConsultarExamenesContestadosPorResponsable(int idUsuario)
    {
        return context.Examen
            .Where(p => p.IdProgramacionExamenNavigation.IdUsuarioResponsable == idUsuario && p.IdEstatusExamen == 3 && p.Estatus == true)
            .OrderBy(p => p.IdExamen)
            .Select(p => new CuestionariosPorResponsableDto
            {
                IdExamen = p.IdExamen,
                clave = p.IdProgramacionExamenNavigation.Clave ?? string.Empty,
                NombreUsuario = p.IdUsuarioParticipanteNavigation.Nombre + " " + p.IdUsuarioParticipanteNavigation.ApellidoPaterno + " " + p.IdUsuarioParticipanteNavigation.ApellidoMaterno,
                IdProgramacionExamen = p.IdProgramacionExamen,
                IdUsuarioParticipante = p.IdUsuarioParticipante,
                Resultado = p.Resultado,
                PreguntasCorrectas = p.PreguntasCorrectas
            })
            .ToList();
    }

    public ExamenDto? ConsultarMiExamen(int idExamen)
    {
        return context.Examen
            .Where(p => p.IdExamen == idExamen)
            .OrderBy(p => p.IdExamen)
            .Select(p => new ExamenDto
            {
                IdExamen = p.IdExamen,
                TipoExamen = p.IdProgramacionExamenNavigation.IdTipoExamenNavigation.Nombre ?? string.Empty,
                FechaExamen = p.IdProgramacionExamenNavigation.FechaExamen.HasValue 
                    ? p.IdProgramacionExamenNavigation.FechaExamen.Value 
                    : (DateTime?)null,
                HoraExamen = p.IdProgramacionExamenNavigation.HoraExamen,
                Duracion = p.IdProgramacionExamenNavigation.Duracion,
                NombreUsuario = p.IdUsuarioParticipanteNavigation.Nombre + " " + p.IdUsuarioParticipanteNavigation.ApellidoPaterno + " " + p.IdUsuarioParticipanteNavigation.ApellidoMaterno,
                Clave = p.IdProgramacionExamenNavigation.Clave ?? string.Empty,
                TotalPreguntas = p.IdProgramacionExamenNavigation.IdTipoExamenNavigation.TotalPreguntas
            })
            .FirstOrDefault();
    }

    public IEnumerable<ExamenCalificacionDto> ConsultarCalificaciones(int idProgramacionExamen)
    {
        return context.Examen
            .Where(p => p.IdProgramacionExamen == idProgramacionExamen && p.Estatus == true)
            .Select(p => new ExamenCalificacionDto
            {
                IdExamen = p.IdExamen,
                NombreUsuario = p.IdUsuarioParticipanteNavigation.Nombre + " " + p.IdUsuarioParticipanteNavigation.ApellidoPaterno + " " + p.IdUsuarioParticipanteNavigation.ApellidoMaterno,
                IdProgramacionExamen = p.IdProgramacionExamen,
                IdUsuarioParticipante = p.IdUsuarioParticipante,
                Resultado = p.Resultado,
                PreguntasCorrectas = p.PreguntasCorrectas
            })
            .OrderBy(p => p.IdExamen)
            .ToList();
    }
}
