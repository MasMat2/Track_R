using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;

namespace TrackrAPI.Controllers.Catalogo;

[Route("api/[controller]")]
[ApiController]
public class MunicipioController : ControllerBase
{
    private readonly MunicipioService _municipioService;

    public MunicipioController(MunicipioService municipioService) {
        _municipioService = municipioService;
    }

    [HttpGet("formulario/{idMunicipio}")]
    public MunicipioFormularioConsultaDto? ConsultarParaFormulario(int idMunicipio)
    {
        return _municipioService.ConsultarParaFormulario(idMunicipio);
    }

    [HttpGet("grid")]
    public IEnumerable<MunicipioGridDto> ConsultarParaGrid()
    {
        return _municipioService.ConsultarParaGrid();
    }

    [HttpGet("selector")]
    public IEnumerable<MunicipioSelectorDto> ConsultarTodosParaSelector()
    {
        return _municipioService.ConsultarTodosParaSelector();
    }

    [HttpGet("selector/estado/{idEstado}")]
    public IEnumerable<MunicipioSelectorDto> ConsultarPorEstadoParaSelector(int idEstado)
    {
        return _municipioService.ConsultarPorEstadoParaSelector(idEstado);
    }

    [HttpPost]
    public void Agregar(MunicipioFormularioCapturaDto municipio)
    {
        _municipioService.Agregar(municipio);
    }

    [HttpPut]
    public void Editar(MunicipioFormularioCapturaDto municipio)
    {
        _municipioService.Editar(municipio);
    }

    [HttpDelete("{idMunicipio}")]
    public void Eliminar(int idMunicipio)
    {
        _municipioService.Eliminar(idMunicipio);
    }

    [HttpPost("actualizarEstadosExcel")]
    public void SincronizarPlantillaExcel()
    {
        _municipioService.SincronizarPlantillaExcel();
    }

}
