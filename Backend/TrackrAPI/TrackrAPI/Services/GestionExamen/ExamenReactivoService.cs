using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

public class ExamenReactivoService
{
    private readonly IExamenReactivoRepository _examenReactivoRepository;
    private readonly ExamenReactivoValidatorService _examenReactivoValidatorService;
    private readonly IReactivoRepository _reactivoRepository;
    private readonly IExamenRepository _examenRepository;
    private readonly IRespuestaRepository _respuestaRepository;

    public ExamenReactivoService(
        IExamenReactivoRepository examenReactivoRepository,
        ExamenReactivoValidatorService examenReactivoValidatorService,
        IReactivoRepository reactivoRepository,
        IExamenRepository examenRepository,
        IRespuestaRepository respuestaRepository)
    {
        _examenReactivoRepository = examenReactivoRepository;
        _examenReactivoValidatorService = examenReactivoValidatorService;
        _reactivoRepository = reactivoRepository;
        _examenRepository = examenRepository;
        _respuestaRepository = respuestaRepository;
    }

    public ExamenReactivo? Consultar(int idExamenReactivo)
    {
        ExamenReactivo? examenReactivo = _examenReactivoRepository.Consultar(idExamenReactivo);
        return examenReactivo;
    }

    public IEnumerable<ExamenReactivo> ConsultarGeneral()
    {
        return _examenReactivoRepository.ConsultarGeneral();
    }

    public IEnumerable<ExamenReactivo> ConsultarTodosParaSelector()
    {
        return _examenReactivoRepository.ConsultarTodosParaSelector();
    }

    public IEnumerable<ExamenReactivoDto> ConsultarReactivosExamen(int idExamen)
    {
        Examen? examen = _examenRepository.Consultar(idExamen);

        if (examen == null)
        {
            throw new CdisException("El examen no existe");
        }

        if(examen.IdEstatusExamen != GeneralConstant.idEstatusExamenTerminado)
        {
            examen.IdEstatusExamen = GeneralConstant.idEstatusExamenPresentandose; // Cambiar estado a presentando
        }
        _examenRepository.Editar(examen);

        return _examenReactivoRepository.ConsultarReactivosExamen(idExamen);
    }

    public RespuestasExcelDto ConsultarReactivosExamenExcel(int idProgramacionExamen)
    {
        var reactivos = _examenReactivoRepository.ConsultarReactivosExamenExcel(idProgramacionExamen);
        
        foreach (var reactivo in reactivos)
        {
            reactivo.RespuestaAlumno = _respuestaRepository.ConsultarRespuestaContestada(reactivo.IdReactivo , reactivo.RespuestaAlumno).RespuestaFormateada;
        }

        List<string> headers = new List<string> { "Marca temporal", "Usuario", "Correo electronico" }; //Datos de la persona que respondió
        List<string> preguntas = reactivos.GroupBy(r => r.IdExamen).FirstOrDefault().Select(p => p.Pregunta).ToList(); //Preguntas del cuestionario
        headers.AddRange(preguntas); //Los headers del excel son los datos + las preguntas

        var respuestas = reactivos.ToList();
        var respuestasConDatos = new List<ExamenReactivoExcelDto>();

        foreach (var grupo in respuestas.GroupBy(r => r.IdExamen))
        {
            var datos = _examenReactivoRepository.obtenerDatosParaRespuestasExcel(grupo.FirstOrDefault().IdExamen);

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



    public int Agregar(ExamenReactivo examenReactivo)
    {
        _examenReactivoValidatorService.ValidarAgregar(examenReactivo);
        _examenReactivoRepository.Agregar(examenReactivo);
        return examenReactivo.IdExamenReactivo;
    }

    public void Editar(ExamenReactivo examenReactivo)
    {
        _examenReactivoValidatorService.ValidarEditar(examenReactivo);
        _examenReactivoRepository.Editar(examenReactivo);
    }

    public void Eliminar(int idExamenReactivo)
    {
        ExamenReactivo? examenReactivo = _examenReactivoRepository.Consultar(idExamenReactivo);
        _examenReactivoValidatorService.ValidarEliminar(idExamenReactivo);

        if (examenReactivo != null)
        {
            examenReactivo.Estatus = false;

            _examenReactivoRepository.Editar(examenReactivo);
        }
    }

    public float Revisar(List<ExamenReactivo> examenReactivoList)
    {
        if(examenReactivoList.Count == 0)
        {
            return 0;
        }

        int preguntasCorrectas = 0;


        Examen? examen = _examenRepository.Consultar(examenReactivoList[0].IdExamen);
   
        if (examen is null)
        {
            throw new CdisException("El examen no existe");
        }

        examen.Resultado = 0;


        foreach (ExamenReactivo examenReactivo in examenReactivoList)
        {
            if(examenReactivo.RespuestaValor != null)
            {
                examen.Resultado += examenReactivo.RespuestaValor;
            }

            string respuestaCorrecta = _reactivoRepository.ConsultarRespuestaCorrecta(examenReactivo.IdReactivo);

            if (examenReactivo.RespuestaAlumno == respuestaCorrecta)
            {
                examenReactivo.Resultado = true;
                preguntasCorrectas++;

                _examenReactivoRepository.Editar(examenReactivo);
            }
            else
            {
                examenReactivo.Resultado = false;

                _examenReactivoRepository.Editar(examenReactivo);
            }
        }



        int totalPreguntas = examen.IdProgramacionExamenNavigation.IdTipoExamenNavigation.TotalPreguntas == null ? 0 : examen.IdProgramacionExamenNavigation.IdTipoExamenNavigation.TotalPreguntas ?? 0;
        float calificacion = (float?)examen.Resultado ?? 0;
        examen.IdEstatusExamen = 3; //Examen Terminado

        _examenRepository.Editar(examen);

        return calificacion;
    }


}
