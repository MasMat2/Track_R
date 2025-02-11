﻿using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public class ReactivoRepository : Repository<Reactivo>, IReactivoRepository
{
    public ReactivoRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }
    public ReactivoDto? Consultar(int idReactivo)
    {
        return context.Reactivo
            .Where(p => p.IdReactivo == idReactivo)
            .OrderBy(p => p.Clave)
            .Select(p => new ReactivoDto
            {
                IdReactivo = p.IdReactivo,
                IdAsignatura = p.IdAsignatura,
                IdNivelExamen = p.IdNivelExamen,
                Clave = p.Clave ?? string.Empty,
                Pregunta = p.Pregunta ?? string.Empty,
                Imagen = p.Imagen ?? Array.Empty<byte>(),
                ImagenTipoMime = p.ImagenTipoMime ?? string.Empty,
                ImagenNombre = p.ImagenNombre ?? string.Empty,
                Respuesta = p.Respuesta ?? string.Empty,
                RespuestaCorrecta = p.RespuestaCorrecta ?? string.Empty,
                NecesitaRevision = p.NecesitaRevision,
                FechaAlta = p.FechaAlta,
                Estatus = p.Estatus ?? false,
                EscalaLikert = p.EscalaLikert,
                Simple = p.RespuestaSimple,
                Abierta = p.PreguntaAbierta,
                Multiple = p.RespuestaMultiple,
                RespuestaList = p.RespuestaNavigation
                .Where(r => r.IdReactivo ==  p.IdReactivo)
                .Select(r => new Respuesta
                {
                    IdReactivo = r.IdReactivo,
                    IdRespuesta = r.IdRespuesta,
                    Clave = r.Clave,
                    Respuesta1 = r.Respuesta1,
                    RespuestaCorrecta = r.RespuestaCorrecta,
                    Valor = r.Valor
                }).ToList(),
            })
            .FirstOrDefault();
    }
    public ReactivoDto? ConsultarImagen(int idReactivo)
    {
        return context.Reactivo
            .Where(p => p.IdReactivo == idReactivo)
            .OrderBy(p => p.Clave)
            .Select(p => new ReactivoDto
            {
                IdReactivo = p.IdReactivo,
                Imagen = p.Imagen ?? Array.Empty<byte>(),
                ImagenTipoMime = p.ImagenTipoMime ?? string.Empty,
                ImagenNombre = p.ImagenNombre ?? string.Empty,
            })
            .FirstOrDefault();
    }

    public IEnumerable<ReactivoGridDto> ConsultarGeneral()
    {
        return context.Reactivo
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new ReactivoGridDto
            {
                IdReactivo = p.IdReactivo,
                Asignatura = p.IdAsignaturaNavigation.Descripcion ?? string.Empty,
                NivelExamen = p.IdNivelExamenNavigation.Descripcion ?? string.Empty,
                Clave = p.Clave ?? string.Empty,
                Pregunta = p.Pregunta ?? string.Empty,
                Respuesta = p.Respuesta ?? string.Empty,
                RespuestaCorrecta = p.RespuestaCorrecta ?? string.Empty,
                RespuestasList = p.RespuestaNavigation.Select(r => new RespuestaDto
                {
                    IdReactivo = r.IdReactivo,
                    IdRespuesta = r.IdRespuesta,
                    Clave = r.Clave ?? string.Empty,
                    Respuesta1 = r.Respuesta1 ?? string.Empty,
                    RespuestaCorrecta = r.RespuestaCorrecta == true ? true : false,
                }).ToList(),
            })
            .ToList();
    }

    public IEnumerable<ReactivoGridDto> ConsultarTodosParaSelector()
    {
        return context.Reactivo
            .Where(p => p.Estatus == true)
            .OrderBy(p => p.Clave)
            .Select(p => new ReactivoGridDto
            {
                IdReactivo = p.IdReactivo,
                Clave = p.Clave ?? string.Empty,
                Pregunta = p.Pregunta ?? string.Empty,
            })
            .ToList();
    }

    public IEnumerable<Reactivo> ConsultarReactivosAleatorio(int idAsignatura, int idNivelExamen, int cantidadPreguntas)
    {
        return context.Reactivo
            .Where(p => p.IdAsignatura == idAsignatura && p.IdNivelExamen == idNivelExamen && p.Estatus == true)
            .OrderBy(p => Guid.NewGuid())
            .Take(cantidadPreguntas)
            .ToList();
    }

    public string ConsultarRespuestaCorrecta(int idReactivo)
    {
        return context.Reactivo
            .Where(p => p.IdReactivo == idReactivo)
            .OrderBy(p => p.Clave)
            .Select(p => p.RespuestaCorrecta)
            .FirstOrDefault() ?? string.Empty;
    }

    public int ConsultarCantidadReactivos(int idAsignatura, int idNivelExamen)
    {
        return context.Reactivo
            .Where(p => p.IdAsignatura == idAsignatura && p.IdNivelExamen == idNivelExamen && p.Estatus == true)
            .Count();
    }
}
