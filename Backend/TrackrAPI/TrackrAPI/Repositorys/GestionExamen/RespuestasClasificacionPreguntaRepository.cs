using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

    public class RespuestasClasificacionPreguntaRepository : Repository<RespuestasClasificacionPregunta>, IRespuestasClasificacionPreguntaRepository
    {
        public RespuestasClasificacionPreguntaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public RespuestasClasificacionPreguntaInformacionGeneralDto ConsultarParaFormulario(int idRespuestasClasificacionPregunta)
        {
            var rcp = context.RespuestasClasificacionPregunta
            .Where(rcp => rcp.IdRespuestasClasificacionPregunta == idRespuestasClasificacionPregunta)
            .FirstOrDefault();

            if (rcp == null)
            {
                return null;
            }
            var rcpInformacionGeneralDto = new RespuestasClasificacionPreguntaInformacionGeneralDto
            {
                IdRespuestasClasificacionPregunta = rcp.IdRespuestasClasificacionPregunta,
                IdClasificacionPregunta = rcp.IdClasificacionPregunta,
                Nombre = rcp.Nombre,
                Valor = rcp.Valor,
                Estatus = rcp.Estatus,
                Identificador = rcp.Identificador
            };
            return rcpInformacionGeneralDto;
        }

        public IEnumerable<RespuestasClasificacionPreguntaGridDto> ConsultarParaGrid(int idClasificacionPregunta)
        {
            return context.RespuestasClasificacionPregunta
            .Where(rcp => rcp.IdClasificacionPregunta == idClasificacionPregunta)
            .Select(rcp => new RespuestasClasificacionPreguntaGridDto
            {
                IdRespuestasClasificacionPregunta = rcp.IdRespuestasClasificacionPregunta,
                IdClasificacionPregunta = rcp.IdClasificacionPregunta,
                Nombre = rcp.Nombre,
                Valor = rcp.Valor,
                Estatus = rcp.Estatus,
                Identificador = rcp.Identificador
            }).ToList();
        }

        public void Eliminar(int idRespuestasClasificacionPregunta)
        {
            var rcp = context.RespuestasClasificacionPregunta
            .Where(rcp => rcp.IdRespuestasClasificacionPregunta == idRespuestasClasificacionPregunta).FirstOrDefault();

            if (rcp == null)
            {
                throw new CdisException("La informacion no se encuentra registrada");

            }

            context.RespuestasClasificacionPregunta.Attach(rcp);
            rcp.Estatus = false;
            context.SaveChanges();
        }


        public IEnumerable<Respuesta>? ConsultarRespuestasPorClasificacion(int idClasificacionPregunta)
        {
            return context.RespuestasClasificacionPregunta
            .Where(p => p.IdClasificacionPregunta == idClasificacionPregunta)
            .Select(p => new Respuesta
            {
                Clave = p.Identificador,
                Respuesta1 = p.Nombre,
                Valor = p.Valor
            })
            .ToList();
        }

        public int? ConsultarIdClasificacionPorNombreRespuesta(string respuesta)
        {
            return context.RespuestasClasificacionPregunta
            .Where(r => r.Nombre == respuesta)
            .Select(r => r.IdClasificacionPregunta)
            .FirstOrDefault();
        }
    }
