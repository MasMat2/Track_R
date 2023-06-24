using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo;

public class MunicipioService
{
    private readonly IMunicipioRepository _municipioRepository;
    private readonly MunicipioValidatorService _municipioValidatorService;

    public MunicipioService(
        IMunicipioRepository municipioRepository,
        MunicipioValidatorService municipioValidatorService) {
        _municipioRepository = municipioRepository;
        _municipioValidatorService = municipioValidatorService;
    }


    public IEnumerable<MunicipioSelectorDto> ConsultarTodosParaSelector()
    {
        var municipios = _municipioRepository.Consultar();

        var municipiosDto = municipios.Select(m => new MunicipioSelectorDto
        {
            IdMunicipio = m.IdMunicipio,
            Nombre = m.Nombre
        });

        return municipiosDto;
    }

    public MunicipioFormularioConsultaDto? ConsultarParaFormulario(int idMunicipio)
    {
        var municipio = _municipioRepository.ConsultarParaFormulario(idMunicipio);

        if (municipio == null)
        {
            return null;
        }

        var municipioDto = new MunicipioFormularioConsultaDto
        {
            IdMunicipio = municipio.IdMunicipio,
            IdPais = municipio.IdEstadoNavigation.IdPais,
            IdEstado = municipio.IdEstado,
            Nombre = municipio.Nombre,
            Clave = municipio.Clave ?? string.Empty,
        };

        return municipioDto;
    }

    public IEnumerable<MunicipioGridDto> ConsultarParaGrid()
    {
        var municipios = _municipioRepository.ConsultarParaGrid();

        var municipiosDto = municipios
            .Select(m => new MunicipioGridDto
            {
                IdMunicipio = m.IdMunicipio,
                Nombre = m.Nombre,
                Clave = m.Clave ?? string.Empty,
                NombreEstado = m.IdEstadoNavigation.Nombre,
                NombrePais = m.IdEstadoNavigation.IdPaisNavigation.Nombre
            })
            .OrderBy(m => m.NombrePais)
            .ThenBy(m => m.NombreEstado)
            .ThenBy(m => m.Nombre);

        return municipiosDto;
    }

    public IEnumerable<MunicipioSelectorDto> ConsultarPorEstadoParaSelector(int idEstado)
    {
        var municipios = _municipioRepository.ConsultarPorEstado(idEstado);

        var municipiosDto = municipios.Select(m => new MunicipioSelectorDto
        {
            IdMunicipio = m.IdMunicipio,
            Nombre = m.Nombre
        });

        return municipiosDto;
    }

    public void Agregar(MunicipioFormularioCapturaDto municipioDto)
    {
        _municipioValidatorService.ValidarAgregar(municipioDto);

        var municipio = new Municipio
        {
            Nombre = municipioDto.Nombre,
            IdEstado = municipioDto.IdEstado,
            Clave = municipioDto.Clave
        };

        _municipioRepository.Agregar(municipio);
    }

    public void Editar(MunicipioFormularioCapturaDto municipioDto)
    {
        _municipioValidatorService.ValidarEditar(municipioDto);

        var municipio = _municipioRepository.Consultar(municipioDto.IdMunicipio)!;

        municipio.Nombre = municipioDto.Nombre;
        municipio.IdEstado = municipioDto.IdEstado;

        _municipioRepository.Editar(municipio);
    }

    public void Eliminar(int idMunicipio)
    {
        _municipioValidatorService.ValidarEliminar(idMunicipio);

        var municipio = _municipioRepository.Consultar(idMunicipio)!;

        _municipioRepository.Eliminar(municipio);
    }
}
