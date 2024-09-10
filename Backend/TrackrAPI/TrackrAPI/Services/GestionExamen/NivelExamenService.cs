using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

public class NivelExamenService
{
    private readonly INivelExamenRepository _nivelExamenRepository;
    private readonly NivelExamenValidatorService _nivelExamenValidatorService;

    public NivelExamenService(
        INivelExamenRepository nivelExamenRepository,
        NivelExamenValidatorService nivelExamenValidatorService)
    {
        _nivelExamenRepository = nivelExamenRepository;
        _nivelExamenValidatorService = nivelExamenValidatorService;
    }

    public NivelExamenDto? Consultar(int idNivelExamen)
    {
        return _nivelExamenRepository.Consultar(idNivelExamen);
    }

    public IEnumerable<NivelExamenGridDto> ConsultarGeneral()
    {
        return _nivelExamenRepository.ConsultarGeneral();
    }

    public IEnumerable<NivelExamenGridDto> ConsultarTodosParaSelector()
    {
        return _nivelExamenRepository.ConsultarTodosParaSelector();
    }

    public int Agregar(NivelExamen nivelExamen)
    {
        _nivelExamenValidatorService.ValidarAgregar(nivelExamen);
        _nivelExamenValidatorService.ValidarDuplicado(nivelExamen);
        nivelExamen.Clave = GenerarClave();
        _nivelExamenRepository.Agregar(nivelExamen);
        return nivelExamen.IdNivelExamen;
    }

    public void Editar(NivelExamen nivelExamen)
    {
        _nivelExamenValidatorService.ValidarEditar(nivelExamen);
        _nivelExamenValidatorService.ValidarDuplicado(nivelExamen);
        _nivelExamenRepository.Editar(nivelExamen);
    }

    private string GenerarClave()
    {
        var nivelExamen = _nivelExamenRepository.ConsultarGeneral().OrderByDescending(x => x.IdNivelExamen).FirstOrDefault();

        int idNivelExamen = nivelExamen?.IdNivelExamen ?? 0;

        return (idNivelExamen + 1).ToString();
    }

    public void Eliminar(int idNivelExamen)
    {
        NivelExamenDto? nivelExamen = _nivelExamenRepository.Consultar(idNivelExamen);
        _nivelExamenValidatorService.ValidarEliminar(idNivelExamen);

        if (nivelExamen != null)
        {
            NivelExamen nivelExamenMod = new()
            {
                IdNivelExamen = nivelExamen.IdNivelExamen,
                Clave = nivelExamen.Clave,
                Descripcion = nivelExamen.Descripcion,
                FechaAlta = nivelExamen.FechaAlta,
                Estatus = false
            };

            _nivelExamenRepository.Editar(nivelExamenMod);
        }
    }
}
