using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

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
     private readonly string MensajeRespuestaSimple = "Solo debe existir una respuesta correcta";

     public void ValidarAgregar(ReactivoDto reactivo)
    {
        ValidarRequerido(reactivo);
        ValidarSimple(reactivo);
        ValidarMultiple(reactivo);
    }

public void ValidarEditar(ReactivoDto reactivo)
    {
        ValidarRequerido(reactivo);
        ValidarExistencia(reactivo.IdReactivo);
        ValidarSimple(reactivo);
        ValidarMultiple(reactivo);
    }

    public void ValidarEliminar(int idAsignacion)
    {
        ValidarExistencia(idAsignacion);
    }

     public void ValidarRequerido(ReactivoDto reactivo)
    {
        Validator.ValidarRequerido(reactivo.Clave, MensajeClaveRequerido);
        Validator.ValidarRequerido(reactivo.Pregunta, MensajePreguntaRequerida);
    }

    public void ValidarExistencia(int idAsignacion)
    {
        if (_reactivoRepository.Consultar(idAsignacion) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

      public void ValidarSimple(ReactivoDto reactivo)
    {
        int respuestasCorrectas = 0;
        if (reactivo.Simple.HasValue)
        {
            if (reactivo.Simple.Value)
            {
                if(reactivo.RespuestaList != null)
                {
                    if (reactivo.RespuestaList.Count == 0)
                    {
                        throw new CdisException(MensajeRespuestaRequerida);
                    }
                    respuestasCorrectas = reactivo.RespuestaList.Where(a => a.RespuestaCorrecta.Value).Select(a => a.RespuestaCorrecta).Count();
                    if (respuestasCorrectas > 1 || respuestasCorrectas == 0)
                    {
                        throw new CdisException(MensajeRespuestaSimple);
                    }
                }
            }
        }
    }

     public void ValidarMultiple(ReactivoDto reactivo)
    {
        int respuestasCorrectas = 0;
        if (reactivo.Multiple.HasValue)
        {
            if (reactivo.Multiple.Value)
            {
                if(reactivo.RespuestaList != null)
                {
                    if (reactivo.RespuestaList.Count == 0)
                    {
                        throw new CdisException(MensajeRespuestaRequerida);
                    }
                    respuestasCorrectas = reactivo.RespuestaList.Where(a => a.RespuestaCorrecta.Value).Select(a => a.RespuestaCorrecta).Count();
                    if (respuestasCorrectas == 0)
                    {
                        throw new CdisException(MensajeRespuestaCorrectaRequerida);
                    }
                }
            }
        }
    }
}
