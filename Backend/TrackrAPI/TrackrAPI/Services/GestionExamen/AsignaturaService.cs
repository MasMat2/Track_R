using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

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
        asignatura.Clave = GenerarClave();
        _asignaturaRepository.Agregar(asignatura);
        return asignatura.IdAsignatura;
    }

    private string GenerarClave()
    {
        var asignatura = _asignaturaRepository.ConsultarGeneral().OrderByDescending(x => x.IdAsignatura).FirstOrDefault();

        int idAsignatura = asignatura?.IdAsignatura ?? 0;

        return (idAsignatura + 1).ToString();
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
