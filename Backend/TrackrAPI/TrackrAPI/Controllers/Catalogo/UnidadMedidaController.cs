using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;

namespace TrackrAPI.Controllers.Catalogo;

[Route("api/[controller]")]
[ApiController]
public class UnidadMedidaController : ControllerBase{
    private readonly  UnidadMedidaService _unidadMedidaService;
    

    public UnidadMedidaController(UnidadMedidaService unidadMedidaService){
        _unidadMedidaService = unidadMedidaService;
    }

    [HttpGet("formulario/{idUnidadMedida}")]
    public async Task<UnidadMedidaFormularioCapturaDto?> ConsultarParaFormulario(int idUnidadMedida){
        return await _unidadMedidaService.ConsultarParaFormulario(idUnidadMedida);
    }

    [HttpGet("grid")]
    public async Task<IEnumerable<UnidadMedidaGridDto>> Consultar(){
        return await _unidadMedidaService.ConsultarParaGrid();
    }

    [HttpPost]
    public async Task Agregar(UnidadMedidaFormularioCapturaDto unidadMedidaDto){
        await _unidadMedidaService.Agregar(unidadMedidaDto);
    }

    [HttpPut]
    public async Task Editar(UnidadMedidaFormularioCapturaDto unidadMedidaDto){
        await _unidadMedidaService.Editar(unidadMedidaDto);
    }

    [HttpDelete("{idUnidadMedida}")]
    public async Task Eliminar(int idUnidadMedida){
        await _unidadMedidaService.Eliminar(idUnidadMedida);
    }

    

}