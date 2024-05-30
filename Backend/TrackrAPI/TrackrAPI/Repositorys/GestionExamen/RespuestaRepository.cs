
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public class RespuestaRepository : Repository<Respuesta>, IRespuestaRepository
{
    public RespuestaRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public RespuestaDto? ConsultarParaFormulario(int idRespuesta)
    {
        return context.Respuesta
            .Where(p => p.IdRespuesta == idRespuesta)
            .OrderBy(p => p.Clave)
            .Select(p => new RespuestaDto
            {
                IdReactivo = p.IdReactivo,
                IdRespuesta = p.IdRespuesta,
                Clave = p.Clave ?? string.Empty,
                Respuesta1 = p.Respuesta1 ?? string.Empty,
                RespuestaCorrecta = p.RespuestaCorrecta,
                Valor = p.Valor
            })
            .FirstOrDefault();
    }

    public IEnumerable<RespuestaDto> ConsultarTodosPorReactivo(int idReactivo)
    {
        return context.Respuesta
            .Where(p => p.IdReactivo == idReactivo)
            .Select(p => new RespuestaDto
            {
                IdReactivo = p.IdReactivo,
                IdRespuesta = p.IdRespuesta,
                Clave = p.Clave ?? string.Empty,
                Respuesta1 = p.Respuesta1 ?? string.Empty,
                RespuestaCorrecta = p.RespuestaCorrecta,
                Valor = p.Valor
            })
            .ToList();
    }

    public RespuestaDto ConsultarRespuestaContestada(int idReactivo , string clave)
    {
        return context.Respuesta
            .Where(p => p.IdReactivo == idReactivo)
            .Where( r => r.Clave == clave )
            .Select(p => new RespuestaDto
            {
                Clave = p.Clave ?? string.Empty,
                Respuesta1 = p.Respuesta1 ?? string.Empty,
                RespuestaCorrecta = p.RespuestaCorrecta,
                Valor = p.Valor
            })
            .FirstOrDefault();
    }

    public Respuesta Consultar(int idRespuesta)
    {
        return context.Respuesta
            .Where(p => p.IdRespuesta == idRespuesta)
            .First();
    }
}
