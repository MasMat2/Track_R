﻿using TrackrAPI.Dtos.Examen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Examen;

namespace TrackrAPI.Services.Examen;

public class AsignaturaService
{
    private readonly IAsignaturaRepository _asignaturaRepository;
    private readonly AsignaturaValidatorService _asignaturaValidatorService;

    public AsignaturaService(
        IAsignaturaRepository asignaturaRepository,
        AsignaturaValidatorService asignaturaValidatorService)
    {
        _asignaturaRepository = asignaturaRepository;
        _asignaturaValidatorService = asignaturaValidatorService;
    }

    public AsignaturaDto? Consultar(int idAsignatura)
    {
        return _asignaturaRepository.Consultar(idAsignatura);
    }

    public IEnumerable<AsignaturaGridDto> ConsultarGeneral()
    {
        return _asignaturaRepository.ConsultarGeneral();
    }

    public IEnumerable<AsignaturaGridDto> ConsultarTodosParaSelector()
    {
        return _asignaturaRepository.ConsultarTodosParaSelector();
    }

    public int Agregar(Asignatura asignatura)
    {
        _asignaturaValidatorService.ValidarAgregar(asignatura);
        _asignaturaValidatorService.ValidarDuplicado(asignatura);
        _asignaturaRepository.Agregar(asignatura);
        return asignatura.IdAsignatura;
    }

    public void Editar(Asignatura asignatura)
    {
        _asignaturaValidatorService.ValidarEditar(asignatura);
        _asignaturaValidatorService.ValidarDuplicado(asignatura);
        _asignaturaRepository.Editar(asignatura);
    }

    public void Eliminar(int idAsignatura)
    {
        var asignatura = _asignaturaRepository.Consultar(idAsignatura);
        _asignaturaValidatorService.ValidarEliminar(idAsignatura);

        if (asignatura != null)
        {
            var asignaturaMod = new Asignatura()
            {
                IdAsignatura = asignatura.IdAsignatura,
                Clave = asignatura.Clave,
                Descripcion = asignatura.Descripcion,
                FechaAlta = asignatura.FechaAlta,
                Estatus = false
            };

            _asignaturaRepository.Editar(asignaturaMod);
        }
    }
}
