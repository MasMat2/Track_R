using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public class ExamenReactivoRepository : Repository<ExamenReactivo>, IExamenReactivoRepository
{
    public ExamenReactivoRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public ExamenReactivo? Consultar(int idExamenReactivo)
    {
        return context.ExamenReactivo
            .Where(p => p.IdExamenReactivo == idExamenReactivo)
            .OrderBy(p => p.IdExamenReactivo)
            .FirstOrDefault();
    }

    public IEnumerable<ExamenReactivo> ConsultarGeneral()
    {
        return context.ExamenReactivo
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.IdExamenReactivo)
            .ToList();
    }

    public IEnumerable<ExamenReactivo> ConsultarTodosParaSelector()
    {
        return context.ExamenReactivo
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.IdExamenReactivo)
            .ToList();
    }

    public IEnumerable<ExamenReactivoDto> ConsultarReactivosExamen(int idExamen)
    {
        return context.ExamenReactivo
            .Where(p => p.IdExamen == idExamen && p.Estatus == true)
            .OrderBy(p => p.IdExamenReactivo)
            .Select(p => new ExamenReactivoDto
            {
                IdExamenReactivo = p.IdExamenReactivo,
                IdReactivo = p.IdReactivo,
                IdExamen = p.IdExamen,
                Asignatura = p.IdReactivoNavigation.IdAsignaturaNavigation.Descripcion ?? string.Empty,
                Clave = p.IdReactivoNavigation.Clave ?? string.Empty,
                Pregunta = p.IdReactivoNavigation.Pregunta ?? string.Empty,
                ImagenBase64 = p.IdReactivoNavigation.ImagenTipoMime == null
                    ? ""
                    : "data:" + p.IdReactivoNavigation.ImagenTipoMime + ";base64," + Convert.ToBase64String(p.IdReactivoNavigation.Imagen ?? Array.Empty<byte>()),
                Respuesta = p.IdReactivoNavigation.Respuesta ?? string.Empty,
                RespuestaAlumno = p.RespuestaAlumno ?? string.Empty,
                NecesitaRevision = p.IdReactivoNavigation.NecesitaRevision,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus,
                Resultado = p.Resultado
            })
            .ToList();
    }
}
