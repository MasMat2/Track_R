using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

public class ContenidoExamenService
{
    private readonly IContenidoExamenRepository _contenidoExamenRepository;
    private readonly ContenidoExamenValidatorService _contenidoExamenValidatorService;
    private readonly TipoExamenService _tipoExamenService;

    public ContenidoExamenService(
        IContenidoExamenRepository contenidoExamenRepository,
        ContenidoExamenValidatorService contenidoExamenValidatorService,
        TipoExamenService tipoExamenService)
    {
        _contenidoExamenRepository = contenidoExamenRepository;
        _contenidoExamenValidatorService = contenidoExamenValidatorService;
        _tipoExamenService = tipoExamenService;
    }

    public ContenidoExamenDto? Consultar(int idContenidoExamen)
    {
        return _contenidoExamenRepository.Consultar(idContenidoExamen);
    }

    public IEnumerable<ContenidoExamenGridDto> ConsultarGeneral(int idTipoExamen)
    {
        return _contenidoExamenRepository.ConsultarGeneral(idTipoExamen);
    }

    public IEnumerable<ContenidoExamenGridDto> ConsultarTodosParaSelector()
    {
        return _contenidoExamenRepository.ConsultarTodosParaSelector();
    }

    public int Agregar(ContenidoExamen contenidoExamen)
    {
        _contenidoExamenValidatorService.ValidarAgregar(contenidoExamen);
        _contenidoExamenRepository.Agregar(contenidoExamen);
        _tipoExamenService.CalcularReactivos(contenidoExamen.IdTipoExamen);
        return contenidoExamen.IdContenidoExamen;
    }

    public void Editar(ContenidoExamen contenidoExamen)
    {
        _contenidoExamenValidatorService.ValidarEditar(contenidoExamen);
        _contenidoExamenRepository.Editar(contenidoExamen);
        _tipoExamenService.CalcularReactivos(contenidoExamen.IdTipoExamen);
    }

    public void Eliminar(int idContenidoExamen)
    {
        ContenidoExamenDto? contenidoExamen = _contenidoExamenRepository.Consultar(idContenidoExamen);
        _contenidoExamenValidatorService.ValidarEliminar(idContenidoExamen);

        if (contenidoExamen != null)
        {
            ContenidoExamen contenidoExamenMod = new()
            {
                IdContenidoExamen = contenidoExamen.IdContenidoExamen,
                IdTipoExamen = contenidoExamen.IdTipoExamen,
                IdAsignatura = contenidoExamen.IdAsignatura,
                IdNivelExamen = contenidoExamen.IdNivelExamen,
                Clave = contenidoExamen.Clave,
                TotalPreguntas = contenidoExamen.TotalPreguntas,
                Duracion = contenidoExamen.Duracion,
                FechaAlta = contenidoExamen.FechaAlta,
                Estatus = false
            };

            _contenidoExamenRepository.Editar(contenidoExamenMod);
            _tipoExamenService.CalcularReactivos(contenidoExamen.IdTipoExamen);
        }
    }
}
