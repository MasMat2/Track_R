using TrackrAPI.Models;
using TrackrAPI.Repositorys.Examen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.Examen;

public class ReactivoValidatorService
{
    private readonly IReactivoRepository _reactivoRepository;

    public ReactivoValidatorService(IReactivoRepository reactivoRepository)
    {
        _reactivoRepository = reactivoRepository;
    }

    private readonly string MensajeClaveRequerido = "La clave es requerida";
    private readonly string MensajePreguntaRequerida = "La pregunta es requerida";
    private readonly string MensajeRespuestaRequerida = "La respuesta es requerida";
    private readonly string MensajeRespuestaCorrectaRequerida = "La respuesta correcta es requerida";

    private readonly string MensajeExistencia = "La asignacion que se requería actualizar no existe";

    public void ValidarAgregar(Reactivo reactivo)
    {
        ValidarRequerido(reactivo);
    }

    public void ValidarEditar(Reactivo reactivo)
    {
        ValidarRequerido(reactivo);
        ValidarExistencia(reactivo.IdReactivo);
    }

    public void ValidarEliminar(int idAsignacion)
    {
        ValidarExistencia(idAsignacion);
    }

    public void ValidarRequerido(Reactivo reactivo)
    {
        Validator.ValidarRequerido(reactivo.Clave, MensajeClaveRequerido);
        Validator.ValidarRequerido(reactivo.Pregunta, MensajePreguntaRequerida);
        Validator.ValidarRequerido(reactivo.Respuesta, MensajeRespuestaRequerida);
        Validator.ValidarRequerido(reactivo.RespuestaCorrecta, MensajeRespuestaCorrectaRequerida);

    }

    public void ValidarExistencia(int idAsignacion)
    {
        if (_reactivoRepository.Consultar(idAsignacion) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }
}
