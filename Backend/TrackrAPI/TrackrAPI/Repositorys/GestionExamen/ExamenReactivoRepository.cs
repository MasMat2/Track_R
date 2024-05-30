using Microsoft.EntityFrameworkCore;
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
                Respuestas = p.IdReactivoNavigation.RespuestaNavigation,
                RespuestaAlumno = p.RespuestaAlumno ?? string.Empty,
                NecesitaRevision = p.IdReactivoNavigation.NecesitaRevision,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus,
                Resultado = p.Resultado
            })
            .ToList();
    }

    public RespuestasExcelDto ConsultarReactivosExamenExcel(int idProgramacionExamen)
    {

        var reactivos = context.ExamenReactivo
            .Where(p => p.IdExamenNavigation.IdProgramacionExamen == idProgramacionExamen && p.Estatus == true)
            .OrderBy(p => p.IdReactivo)
            .Select(p => new ExamenReactivoExcelDto
            {
                IdExamen = p.IdExamen,
                Pregunta = p.IdReactivoNavigation.Pregunta ?? string.Empty,
                RespuestaAlumno = p.RespuestaAlumno ?? string.Empty,
            })
            .ToList();

        List<string> headers = new List<string> { "Marca temporal", "Usuario", "Correo electronico" }; //Datos de la persona que respondió
        List<string> preguntas = reactivos.GroupBy(r => r.IdExamen).FirstOrDefault().Select(p => p.Pregunta).ToList(); //Preguntas del cuestionario
        headers.AddRange(preguntas); //Los headers del excel son los datos + las preguntas

        var respuestas = reactivos.ToList();
        var respuestasConDatos = new List<ExamenReactivoExcelDto>();

        foreach (var grupo in respuestas.GroupBy(r => r.IdExamen))
        {
            var datos = obtenerDatosParaRespuestasExcel(grupo.FirstOrDefault().IdExamen);

            var listaDatos = new List<ExamenReactivoExcelDto> {
                new ExamenReactivoExcelDto {
                    IdExamen = grupo.Key,
                    Pregunta = "Fecha de realizacion",
                    RespuestaAlumno = datos.FechaContestado.ToString()
                },
                new ExamenReactivoExcelDto {
                    IdExamen = grupo.Key,
                    Pregunta = "Nombre del participante",
                    RespuestaAlumno = datos.Nombre
                },
                new ExamenReactivoExcelDto {
                    IdExamen = grupo.Key,
                    Pregunta = "Correo del participante",
                    RespuestaAlumno = datos.Correo
                }
            };
            respuestasConDatos.AddRange(listaDatos);
        }
        respuestasConDatos.AddRange(respuestas);

        RespuestasExcelDto respuestasExcel = new RespuestasExcelDto
        {
            Preguntas = headers,
            Respuestas = respuestasConDatos.GroupBy(r => r.IdExamen)
        };

        return respuestasExcel;
    }

    public DatosExamenReactivoExcelDto obtenerDatosParaRespuestasExcel(int idExamen)
    {
        var reactivos = context.ExamenReactivo.Where(r => r.IdExamen == idExamen).Include(r => r.IdExamenNavigation).ThenInclude(r => r.IdUsuarioParticipanteNavigation).OrderBy(r => r.IdExamenReactivo);
        var reactivo = reactivos.FirstOrDefault();

        var fechaContestado = reactivos.Select(r => r.FechaAlta).LastOrDefault();
        var nombre = reactivo.IdExamenNavigation.IdUsuarioParticipanteNavigation.Nombre + " " + reactivo.IdExamenNavigation.IdUsuarioParticipanteNavigation.ApellidoPaterno + " " + reactivo.IdExamenNavigation.IdUsuarioParticipanteNavigation.ApellidoMaterno;
        var correo = reactivo.IdExamenNavigation.IdUsuarioParticipanteNavigation.CorreoPersonal ?? "";

        return new DatosExamenReactivoExcelDto
        {
            FechaContestado = fechaContestado,
            Nombre = nombre,
            Correo = correo,
        };
    }
}
