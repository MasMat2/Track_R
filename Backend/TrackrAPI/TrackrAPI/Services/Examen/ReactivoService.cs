using TrackrAPI.Dtos.Examen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Examen;

namespace TrackrAPI.Services.Examen;

public class ReactivoService
{
    private readonly IReactivoRepository reactivoRepository;
    private readonly ReactivoValidatorService reactivoValidatorService;

    public ReactivoService(
        IReactivoRepository reactivoRepository,
        ReactivoValidatorService reactivoValidatorService)
    {
        this.reactivoRepository = reactivoRepository;
        this.reactivoValidatorService = reactivoValidatorService;
    }

    public ReactivoDto? Consultar(int idReactivo)
    {
        ReactivoDto? reactivo = reactivoRepository.Consultar(idReactivo);

        if (reactivo == null)
        {
            return null;
        }

        if (reactivo.ImagenTipoMime != null)
        {
            reactivo.ImagenBase64 = ConsultarImagen(reactivo);
        }

        return reactivo;
    }

    public IEnumerable<ReactivoGridDto> ConsultarGeneral()
    {
        return reactivoRepository.ConsultarGeneral();
    }

    public IEnumerable<ReactivoGridDto> ConsultarTodosParaSelector()
    {
        return reactivoRepository.ConsultarTodosParaSelector();
    }

    public int Agregar(Reactivo reactivo)
    {
        reactivoValidatorService.ValidarAgregar(reactivo);
        reactivoRepository.Agregar(reactivo);
        return reactivo.IdReactivo;
    }

    public void Editar(Reactivo reactivo)
    {
        reactivoValidatorService.ValidarEditar(reactivo);
        reactivoRepository.Editar(reactivo);
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
}
