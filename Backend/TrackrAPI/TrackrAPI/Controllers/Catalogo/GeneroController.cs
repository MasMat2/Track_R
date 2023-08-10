using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;


namespace TrackrAPI.Controllers.Catalogo;

[Route("api/[controller]")]
[ApiController]

public class GeneroController : ControllerBase
{
    private readonly GeneroService _generoService;


    public GeneroController(GeneroService generoService)
    {
        _generoService = generoService;
    }

    [HttpGet]
    [Route("{idGenero}")]
    public GeneroDto? Consultar(int idGenero)
    {
        return _generoService.Consultar(idGenero);
    }

    [HttpGet("{idGenero}")]
    public IEnumerable<GeneroDto> Consultar()
    {
        return _generoService.Consultar();
    }

    [HttpPost]
    public void Agregar(GeneroDto generoDto)
    {
        _generoService.Agregar(generoDto);
    }

    [HttpPut]
    
    public void Editar(GeneroDto generoDto)
    {
        _generoService.Editar(generoDto);
    }

    [HttpDelete("{idGenero}")]
    
    public void Eliminar(int idGenero)
    {
        _generoService.Eliminar(idGenero);
    }


}