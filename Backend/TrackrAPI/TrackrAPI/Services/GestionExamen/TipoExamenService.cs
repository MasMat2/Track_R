using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

public class TipoExamenService
{
    private readonly ITipoExamenRepository _tipoExamenRepository;
    private readonly TipoExamenValidatorService _tipoExamenValidatorService;
    private readonly IContenidoExamenRepository _contenidoExamenRepository;

    public TipoExamenService(
        ITipoExamenRepository tipoExamenRepository,
        TipoExamenValidatorService tipoExamenValidatorService,
        IContenidoExamenRepository contenidoExamenRepository)
    {
        _tipoExamenRepository = tipoExamenRepository;
        _tipoExamenValidatorService = tipoExamenValidatorService;
        _contenidoExamenRepository = contenidoExamenRepository;
    }

    public TipoExamenDto? Consultar(int idTipoExamen)
    {
        return _tipoExamenRepository.Consultar(idTipoExamen);
    }

    public IEnumerable<TipoExamenGridDto> ConsultarGeneral()
    {
        return _tipoExamenRepository.ConsultarGeneral();
    }

    public IEnumerable<TipoExamenGridDto> ConsultarTodosParaSelector()
    {
        return _tipoExamenRepository.ConsultarTodosParaSelector();
    }

    public int Agregar(TipoExamen tipoExamen)
    {
        _tipoExamenValidatorService.ValidarAgregar(tipoExamen);
        _tipoExamenValidatorService.ValidarDuplicado(tipoExamen);
        _tipoExamenRepository.Agregar(tipoExamen);
        return tipoExamen.IdTipoExamen;
    }

    public void Editar(TipoExamen tipoExamen)
    {
        _tipoExamenValidatorService.ValidarEditar(tipoExamen);
        _tipoExamenValidatorService.ValidarDuplicado(tipoExamen);
        _tipoExamenRepository.Editar(tipoExamen);
    }

    public void Eliminar(int idTipoExamen)
    {
        TipoExamenDto? tipoExamen = _tipoExamenRepository.Consultar(idTipoExamen);
        _tipoExamenValidatorService.ValidarEliminar(idTipoExamen);

        if (tipoExamen != null)
        {
            TipoExamen tipoExamenMod = new()
            {
                IdTipoExamen = tipoExamen.IdTipoExamen,
                Clave = tipoExamen.Clave,
                Nombre = tipoExamen.Nombre,
                FechaAlta = tipoExamen.FechaAlta,
                TotalPreguntas = tipoExamen.TotalPreguntas,
                Duracion = tipoExamen.Duracion,
                Estatus = false
            };

            _tipoExamenRepository.Editar(tipoExamenMod);
        }
    }

    public void CalcularReactivos(int idTipoExamen)
    {
        var contenidoExamen = _contenidoExamenRepository.ConsultarGeneral(idTipoExamen);
        var tipoExamen = _tipoExamenRepository.Consultar(idTipoExamen);

        if (tipoExamen == null)
        {
            throw new Exception("No se encontró el tipo de examen");
        }

        tipoExamen.TotalPreguntas = 0;
        tipoExamen.Duracion = 0;

        foreach (var contenido in contenidoExamen)
        {
            tipoExamen.TotalPreguntas += contenido.TotalPreguntas;
            tipoExamen.Duracion += contenido.Duracion;
        }

        if (tipoExamen != null)
        {
            TipoExamen tipoExamenMod = new()
            {
                IdTipoExamen = tipoExamen.IdTipoExamen,
                Clave = tipoExamen.Clave,
                Nombre = tipoExamen.Nombre,
                FechaAlta = tipoExamen.FechaAlta,
                TotalPreguntas = tipoExamen.TotalPreguntas,
                Duracion = tipoExamen.Duracion,
                Estatus = tipoExamen.Estatus
            };

            _tipoExamenRepository.Editar(tipoExamenMod);
        }
    }
}
