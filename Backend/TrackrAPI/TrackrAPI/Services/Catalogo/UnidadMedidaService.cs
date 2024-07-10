using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo;

public class UnidadMedidaService
{
    private readonly IUnidadesMedidaRepository _unidadesMedidaRepository;
    private readonly UnidadMedidaValidatorService _unidadMedidaValidatorService;

    public UnidadMedidaService(IUnidadesMedidaRepository unidadesMedidaRepository, UnidadMedidaValidatorService unidadMedidaValidatorService)
    {
        _unidadesMedidaRepository = unidadesMedidaRepository;
        _unidadMedidaValidatorService = unidadMedidaValidatorService;
    }
    
    public async Task<UnidadMedidaFormularioCapturaDto?> ConsultarParaFormulario(int idUnidadMedida)
    {
        var unidadMedida = await _unidadesMedidaRepository.Consultar(idUnidadMedida);

        if (unidadMedida is null)
        {
            return null;
        }

        var unidadMedidaDto = new UnidadMedidaFormularioCapturaDto
        {
            IdUnidadMedida = unidadMedida.Id,
            Nombre = unidadMedida.Nombre
        };

        return unidadMedidaDto;
    }

    public async Task<IEnumerable<UnidadMedidaGridDto>> ConsultarParaGrid()
    {
        var unidadesMedida = await _unidadesMedidaRepository.Consultar();

        var unidadesMedidaDto = unidadesMedida
            .OrderBy(es => es.Nombre)
            .Select(es => new UnidadMedidaGridDto
            {
                IdUnidadMedida = es.Id,
                Nombre = es.Nombre
            });

        return unidadesMedidaDto;
    }

    public async Task Agregar(UnidadMedidaFormularioCapturaDto unidadMedidaDto)
    {
        await _unidadMedidaValidatorService.ValidarAgregar(unidadMedidaDto);
        var unidadMedida = new UnidadesMedida
        {
            Nombre = unidadMedidaDto.Nombre
        };

        _unidadesMedidaRepository.Agregar(unidadMedida);
    }

    public async Task Editar(UnidadMedidaFormularioCapturaDto unidadMedidaDto)
    {

        await _unidadMedidaValidatorService.ValidarEditar(unidadMedidaDto);
        var unidadMedida = await _unidadesMedidaRepository.Consultar(unidadMedidaDto.IdUnidadMedida);
        unidadMedida.Nombre = unidadMedidaDto.Nombre;

        _unidadesMedidaRepository.Editar(unidadMedida);
    }

    public async Task Eliminar(int idUnidadMedida)
    {
        await _unidadMedidaValidatorService.ValidarEliminar(idUnidadMedida);
        var unidadMedida = await _unidadesMedidaRepository.Consultar(idUnidadMedida);
        _unidadesMedidaRepository.Eliminar(unidadMedida);
    }
}
