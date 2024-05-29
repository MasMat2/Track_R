

using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;
public class RespuestasClasificacionPreguntaService
{

    private readonly IRespuestasClasificacionPreguntaRepository _respuestasClasificacionPreguntaRepository;

    public RespuestasClasificacionPreguntaService(IRespuestasClasificacionPreguntaRepository respuestasClasificacionPreguntaRepository)
    {
        _respuestasClasificacionPreguntaRepository = respuestasClasificacionPreguntaRepository;
    }
    public IEnumerable<RespuestasClasificacionPreguntaGridDto> ConsultarParaGrid(int idClasificacionPregunta)
    {
        return _respuestasClasificacionPreguntaRepository.ConsultarParaGrid(idClasificacionPregunta);
    }
    public RespuestasClasificacionPreguntaInformacionGeneralDto ConsultarParaFormulario(int idRespuestasClasificacionPregunta)
    {
        return _respuestasClasificacionPreguntaRepository.ConsultarParaFormulario(idRespuestasClasificacionPregunta);
    }
    public void Agregar(RespuestasClasificacionPreguntaFormularioDto captura)
    {
        try
        {
            var rcp = new RespuestasClasificacionPregunta
            {
                IdRespuestasClasificacionPregunta = captura.IdRespuestasClasificacionPregunta,
                IdClasificacionPregunta = captura.IdClasificacionPregunta,
                Nombre = captura.Nombre,
                Valor = captura.Valor,
                Estatus = true,
                Identificador = captura.Identificador
            };
            _respuestasClasificacionPreguntaRepository.Agregar(rcp);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Editar(RespuestasClasificacionPreguntaFormularioDto captura)
    {
        try
        {
            var rcp = new RespuestasClasificacionPregunta
            {
                IdRespuestasClasificacionPregunta = captura.IdRespuestasClasificacionPregunta,
                IdClasificacionPregunta = captura.IdClasificacionPregunta,
                Nombre = captura.Nombre,
                Valor=captura.Valor,
                Estatus = captura.Estatus,
                Identificador = captura.Identificador
            };
            _respuestasClasificacionPreguntaRepository.Editar(rcp);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Eliminar(int idRespuestasClasificacionPregunta)
    {
        _respuestasClasificacionPreguntaRepository.Eliminar(idRespuestasClasificacionPregunta);
    }
}
