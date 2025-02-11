﻿using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public class ContenidoExamenRepository : Repository<ContenidoExamen>, IContenidoExamenRepository
{
    public ContenidoExamenRepository(TrackrContext context): base(context)
    {
        base.context = context;
    }

    public ContenidoExamenDto? Consultar(int idContenidoExamen)
    {
        return context.ContenidoExamen
            .Where(p => p.IdContenidoExamen == idContenidoExamen)
            .OrderBy(p => p.Clave)
            .Select(p => new ContenidoExamenDto
            {
                IdContenidoExamen = p.IdContenidoExamen,
                IdTipoExamen = p.IdTipoExamen,
                IdAsignatura = p.IdAsignatura,
                IdNivelExamen = p.IdNivelExamen,
                Clave = p.Clave ?? string.Empty,
                TotalPreguntas = p.TotalPreguntas,
                Duracion = p.Duracion,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus ?? false
            })
            .FirstOrDefault();
    }

    public IEnumerable<ContenidoExamenGridDto> ConsultarGeneral(int idTipoExamen)
    {
        return context.ContenidoExamen
            .Where(p => p.IdTipoExamen == idTipoExamen)
            .OrderBy(p => p.Clave)
            .Select(p => new ContenidoExamenGridDto
            {
                IdContenidoExamen = p.IdContenidoExamen,
                Asignatura = p.IdAsignaturaNavigation.Descripcion ?? string.Empty,
                NivelExamen = p.IdNivelExamenNavigation.Descripcion ?? string.Empty,
                Clave = p.Clave ?? string.Empty,
                TotalPreguntas = p.TotalPreguntas,
                Duracion = p.Duracion,
                Estatus = p.Estatus ?? false
            })
            .ToList();
    }

    public IEnumerable<ContenidoExamenGridDto> ConsultarTodosParaSelector()
    {
        return context.ContenidoExamen
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new ContenidoExamenGridDto
            {
                IdContenidoExamen = p.IdContenidoExamen,
                Clave = p.Clave ?? string.Empty,
            })
            .ToList();
    }

    public IEnumerable<ContenidoExamen> ConsultarTodosNoFormato(int idTipoExamen)
    {
        return context.ContenidoExamen
            .Where(p => p.Estatus == true && p.IdTipoExamen == idTipoExamen)
            .ToList();
    }

    public ContenidoExamen ConsultarDuplicado(ContenidoExamen contenidoExamen)
    {
        return context.ContenidoExamen
            .Where(p => p.IdAsignatura  == contenidoExamen.IdAsignatura && p.IdNivelExamen == contenidoExamen.IdNivelExamen && p.Estatus == true && p.IdTipoExamen == contenidoExamen.IdTipoExamen)
            .FirstOrDefault();
    }
}
