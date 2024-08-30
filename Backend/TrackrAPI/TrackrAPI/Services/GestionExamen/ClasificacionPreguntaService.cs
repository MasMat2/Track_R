using TrackrAPI.Dtos.General;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

public class ClasificacionPreguntaService
{

    private readonly IClasificacionPreguntaRepository _clasificacionPreguntaRepository;

    public ClasificacionPreguntaService(IClasificacionPreguntaRepository clasificacionPreguntaRepository)
    {
        _clasificacionPreguntaRepository = clasificacionPreguntaRepository;
    }
    public IEnumerable<ClasificacionPreguntaGridDto> ConsultarParaGrid()
    {
        return _clasificacionPreguntaRepository.ConsultarParaGrid();
    }
    public ClasificacionPreguntaFormularioDto Consultar(int idClasificacionPregunta)
    {
        return _clasificacionPreguntaRepository.Consultar(idClasificacionPregunta);
    }
    public void Agregar(ClasificacionPreguntaFormularioDto dto)
    {
        try
        {
            var clasificacionPregunta = new ClasificacionPregunta
            {
                IdClasificacionPregunta = dto.IdClasificacionPregunta,
                Nombre = dto.Nombre,
                Estatus = true,
                Clave =  GenerarClave(),
            };
            _clasificacionPreguntaRepository.Agregar(clasificacionPregunta);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private string GenerarClave()
    {
        var clasificacionPregunta = _clasificacionPreguntaRepository.ConsultarParaGrid().OrderByDescending(x => x.IdClasificacionPregunta).FirstOrDefault();

        return (clasificacionPregunta.IdClasificacionPregunta + 1).ToString();
    }

    public void Editar(ClasificacionPreguntaFormularioDto dto)
    {
            try
        {
            var clasificacionPregunta = new ClasificacionPregunta
            {
                IdClasificacionPregunta = dto.IdClasificacionPregunta,
                Nombre = dto.Nombre,
                Estatus = dto.Estatus,
                Clave =  dto.Clave,
            };
            _clasificacionPreguntaRepository.Editar(clasificacionPregunta);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Eliminar(int idClasificacionPregunta)
    {
        _clasificacionPreguntaRepository.Eliminar(idClasificacionPregunta);
    }

    public IEnumerable<SimpleSelectorDto> ConsultarTodosParaSelector()
    {
        return _clasificacionPreguntaRepository.ConsultarTodosParaSelector();
    }
}
