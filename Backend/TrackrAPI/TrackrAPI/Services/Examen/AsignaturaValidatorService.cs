﻿using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Examen;

namespace TrackrAPI.Services.Examen;

public class AsignaturaValidatorService
{
    private readonly IAsignaturaRepository _asignaturaRepository;

    public AsignaturaValidatorService(IAsignaturaRepository asignaturaRepository)
    {
        _asignaturaRepository = asignaturaRepository;
    }

    private readonly string MensajeDescripcionRequerido = "La descripcion es requerido";
    private readonly string MensajeClaveRequerida = "La clave es requerida";

    private static readonly int LongitudDescripcion = 50;
    private static readonly int LongitudClave = 5;

    private readonly string MensajeDescripcionLongitud = $"La longitud máxima de la descripción son {LongitudDescripcion} caracteres";
    private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave} caracteres";

    private readonly string MensajeDependencia = "La asignacion tiene reactivos agregados y no se puede eliminar";

    private readonly string MensajeExistencia = "La asignacion que se requería actualizar no existe";

    private readonly string MensajeDuplicado = "La asignatura que intenta agregar ya existe";

    public void ValidarAgregar(Asignatura asignatura)
    {
        ValidarRequerido(asignatura);
    }

    public void ValidarEditar(Asignatura asignatura)
    {
        ValidarRequerido(asignatura);
        ValidarExistencia(asignatura.IdAsignatura);
    }

    public void ValidarEliminar(int idAsignacion)
    {
        ValidarExistencia(idAsignacion);
        ValidarDependencia(idAsignacion);
    }

    public void ValidarRequerido(Asignatura asignatura)
    {
        Validator.ValidarRequerido(
            asignatura.Descripcion,
            MensajeDescripcionRequerido);

        Validator.ValidarRequerido(
            asignatura.Clave,
            MensajeClaveRequerida);

        Validator.ValidarLongitudMaximaString(
            asignatura.Clave,
            LongitudClave,
            MensajeClaveLongitud);

        Validator.ValidarLongitudMaximaString(
            asignatura.Descripcion,
            LongitudDescripcion,
            MensajeDescripcionLongitud);
    }

    public void ValidarExistencia(int idAsignacion)
    {
        if (_asignaturaRepository.Consultar(idAsignacion) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarDependencia(int idAsignacion)
    {
        var asignatura = _asignaturaRepository.ConsultarConDependencias(idAsignacion)!;

        if (asignatura.Reactivo.Any())
        {
            throw new CdisException(MensajeDependencia);
        }
    }

    public void ValidarDuplicado(Asignatura asignatura)
    {
        var asignaturaDuplicada = _asignaturaRepository.ConsultarDuplicado(asignatura);
        if (asignaturaDuplicada is not null)
        {
            throw new CdisException(MensajeDuplicado);
        }
    }
}
