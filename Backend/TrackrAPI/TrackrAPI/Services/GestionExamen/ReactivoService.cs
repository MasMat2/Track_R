using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

public class ReactivoService
{
    private readonly IReactivoRepository reactivoRepository;
    private readonly ReactivoValidatorService reactivoValidatorService;

    private readonly IRespuestaRepository _respuestaRepository;
    private readonly IRespuestasClasificacionPreguntaRepository _respuestasClasificacionPreguntaRepository;

    public ReactivoService(
        IReactivoRepository reactivoRepository,
        ReactivoValidatorService reactivoValidatorService,
        IRespuestaRepository respuestaRepository,
        IRespuestasClasificacionPreguntaRepository respuestasClasificacionPreguntaRepository)
    {
        this.reactivoRepository = reactivoRepository;
        this.reactivoValidatorService = reactivoValidatorService;
        _respuestaRepository = respuestaRepository;
        _respuestasClasificacionPreguntaRepository = respuestasClasificacionPreguntaRepository;
    }

    public ReactivoDto? Consultar(int idReactivo)
    {
        ReactivoDto? reactivo = reactivoRepository.Consultar(idReactivo);

        if (reactivo == null)
        {
            return null;
        }
        if (reactivo.RespuestaList.Count > 0)
        {
            reactivo.IdClasificacionPregunta = _respuestasClasificacionPreguntaRepository.ConsultarIdClasificacionPorNombreRespuesta(reactivo.RespuestaList[0].Respuesta1);
        }
        return reactivo;
    }

      public IEnumerable<ReactivoGridDto> ConsultarGeneral()
    {
        var grid = reactivoRepository.ConsultarGeneral();
        foreach(var registro in grid)
        {
            string respuestaString = "";
            string respuestaCorrecta = "";
            foreach(var respuesta in registro.RespuestasList)
            {
                if(respuesta.Respuesta1.Length > 20)
                {
                    respuestaString += respuesta.Clave + ") " + respuesta.Respuesta1.Substring(0, 20) + "\n";
                }
                else
                {
                    respuestaString += respuesta.Clave + ") " + respuesta.Respuesta1 + "\n";
                }
                
                if (respuesta.RespuestaCorrecta.Value)
                {
                    respuestaCorrecta = respuesta.Clave + ") ";
                }
            }
            registro.Respuesta = respuestaString;
            registro.RespuestaCorrecta = respuestaCorrecta;
        }
        return grid;
    }

    public IEnumerable<ReactivoGridDto> ConsultarTodosParaSelector()
    {
        return reactivoRepository.ConsultarTodosParaSelector();
    }

  public int Agregar(ReactivoDto reactivo)
    {
        reactivoValidatorService.ValidarAgregar(reactivo);
        var agregar = ConvertirDtoModel(reactivo);
        reactivo.IdReactivo = reactivoRepository.Agregar(agregar).IdReactivo;
        EscalaLikert(reactivo);
        return reactivo.IdReactivo;
    }

   public void Editar(ReactivoDto reactivo)
    {
        reactivoValidatorService.ValidarEditar(reactivo);
        var editar = ConvertirDtoModel(reactivo);
        reactivoRepository.Editar(editar);
        EscalaLikert(reactivo);
    }


    public void Eliminar(int idReactivo)
    {
        ReactivoDto? reactivo = reactivoRepository.Consultar(idReactivo);

        reactivoValidatorService.ValidarEliminar(idReactivo);

        if (reactivo != null)
        {
            Reactivo reactivoMod = new()
            {
                IdReactivo = reactivo.IdReactivo,
                IdAsignatura = reactivo.IdAsignatura,
                IdNivelExamen = reactivo.IdNivelExamen,
                Clave = reactivo.Clave,
                Pregunta = reactivo.Pregunta,
                Imagen = reactivo.Imagen,
                ImagenTipoMime = reactivo.ImagenTipoMime,
                ImagenNombre = reactivo.ImagenNombre,
                Respuesta = reactivo.Respuesta,
                RespuestaCorrecta = reactivo.RespuestaCorrecta,
                NecesitaRevision = reactivo.NecesitaRevision,
                FechaAlta = reactivo.FechaAlta,
                Estatus = false
            };

            reactivoRepository.Editar(reactivoMod);
        }
    }

    public string ConsultarImagen(ReactivoDto reactivo)
    {
        string base64 = Convert.ToBase64String(reactivo.Imagen);
        return "data:" + reactivo.ImagenTipoMime + ";base64," + base64;
    }


       public void AgregarRespuestasEscala(int idClasificacionPregunta, int idReactivo)
    {
        var respuestasLikert = _respuestasClasificacionPreguntaRepository.ConsultarRespuestasPorClasificacion(idClasificacionPregunta).ToList();
        var respuestas = _respuestaRepository.ConsultarTodosPorReactivo(idReactivo).Select(r => new Respuesta
        {
            IdRespuesta = r.IdRespuesta,
            IdReactivo = r.IdReactivo,
            Respuesta1 = r.Respuesta1,
            RespuestaCorrecta = r.RespuestaCorrecta,
            Clave = r.Clave
        }).ToList();

        if(respuestas.Count() > 0)
        {
            _respuestaRepository.Eliminar(respuestas);
        }
        foreach(var respuesta in respuestasLikert)
        {
            respuesta.IdReactivo = idReactivo;
        }
        _respuestaRepository.Agregar(respuestasLikert);
    }

       public void EscalaLikert(ReactivoDto reactivo)
    {
        if(reactivo.EscalaLikert.Value) 
        {
            AgregarRespuestasEscala(reactivo.IdClasificacionPregunta.Value, reactivo.IdReactivo);
        }
        else
        {
            if (reactivo.Abierta.Value)
            {
                EliminarRespuestas(reactivo.IdReactivo);
            }
        }
    }

    
    public Reactivo ConvertirDtoModel(ReactivoDto dto)
    {
        return new Reactivo{
            IdReactivo = dto.IdReactivo,        
            IdAsignatura = dto.IdAsignatura,
            IdNivelExamen = dto.IdNivelExamen,
            Clave = dto.Clave,
            Pregunta = dto.Pregunta,
            Imagen = dto.Imagen,
            ImagenTipoMime = dto.ImagenTipoMime,
            ImagenNombre = dto.ImagenNombre,
            NecesitaRevision = dto.NecesitaRevision,
            FechaAlta = dto.FechaAlta,
            Estatus = dto.Estatus,
            EscalaLikert = dto.EscalaLikert,
            PreguntaAbierta = dto.Abierta,
            RespuestaSimple = dto.Simple,
            RespuestaMultiple = dto.Multiple
        };
    }

    public void EliminarRespuestas(int idReactivo)
    {
        var respuestas = _respuestaRepository.ConsultarTodosPorReactivo(idReactivo);
        List<Respuesta> respuestasEliminar = new List<Respuesta>(); 
        foreach(var item in respuestas)
        {
            Respuesta eliminar = new Respuesta();
            eliminar.IdRespuesta = item.IdRespuesta;
            eliminar.Respuesta1 = item.Respuesta1;
            eliminar.RespuestaCorrecta = item.RespuestaCorrecta;
            respuestasEliminar.Add(eliminar);
        }
        if(respuestasEliminar.Count > 0)
        {
            _respuestaRepository.Eliminar(respuestasEliminar);
        }
    }
}
