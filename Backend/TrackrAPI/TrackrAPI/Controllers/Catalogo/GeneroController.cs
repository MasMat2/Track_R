using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]

    public class GeneroController : ControllerBase
    {
        private GeneroService _generoService;
        

        public GeneroController(GeneroService generoService )
        {
            _generoService = generoService;
        }

        [HttpGet]
        [Route("/idGenero")]
        public GeneroDto? Consultar(int idGenero)
        {
            return _generoService.Consultar(idGenero);
        }

        [HttpGet]
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
        //[Route("editar")]
        public void Editar(GeneroDto generoDto)
        {
            _generoService.Editar(generoDto);
        }

        [HttpDelete("{idGenero}")]
        //[Route("eliminar/{idUsuario}")]
        public void Eliminar(int idGenero)
        {
            _generoService.Eliminar(idGenero);
        }


    }
}