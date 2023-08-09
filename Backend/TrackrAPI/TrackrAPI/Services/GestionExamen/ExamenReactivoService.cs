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

    public ExamenReactivoService(
        IExamenReactivoRepository examenReactivoRepository,
        ExamenReactivoValidatorService examenReactivoValidatorService,
        IReactivoRepository reactivoRepository,
        IExamenRepository examenRepository)
    {
        _examenReactivoRepository = examenReactivoRepository;
        _examenReactivoValidatorService = examenReactivoValidatorService;
        _reactivoRepository = reactivoRepository;
        _examenRepository = examenRepository;
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

        examen.IdEstatusExamen = 2; // Cambiar estado a presentando
        _examenRepository.Editar(examen);

        return _examenReactivoRepository.ConsultarReactivosExamen(idExamen);
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

        foreach (ExamenReactivo examenReactivo in examenReactivoList)
        {
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

        Examen? examen = _examenRepository.Consultar(examenReactivoList[0].IdExamen);

        if (examen is null)
        {
            throw new CdisException("El examen no existe");
        }

        int totalPreguntas = examen.IdProgramacionExamenNavigation.IdTipoExamenNavigation.TotalPreguntas == null ? 0 : examen.IdProgramacionExamenNavigation.IdTipoExamenNavigation.TotalPreguntas ?? 0;
        float calificacion = (preguntasCorrectas * 100) / totalPreguntas;

        examen.PreguntasCorrectas = preguntasCorrectas;
        examen.Resultado = calificacion;
        examen.IdEstatusExamen = 3; //Examen Terminado

        _examenRepository.Editar(examen);

        return calificacion;
    }
}
