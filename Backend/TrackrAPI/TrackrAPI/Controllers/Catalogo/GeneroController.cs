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
        private GeneroService generoService;
        // private readonly UsuarioService usuarioService;

        public GeneroController(GeneroService generoService /*UsuarioService usuarioService*/)//
        {
            this.generoService = generoService;
            //this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultar/{idUsuario}")]
        public GeneroDto Consultar(int IdUsuario)
        {
            return generoService.ConsultarDto(IdUsuario);
        }

        [HttpGet]
        [Route("consultarPorTipoDeGenero/{tipoDeGenero}/")]
        public IEnumerable<GeneroDto> ConsultarPorTipoDeGenero(int IdUsuario, string tipoDeGenero)
        {
            return generoService.ConsultarPorTipoDeGenero(IdUsuario, tipoDeGenero);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(GeneroDto generoDto)
        {
            generoService.Agregar(generoDto);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(GeneroDto generoDto)
        {
            generoService.Editar(generoDto);
        }

        [HttpDelete]
        [Route("eliminar/{idUsuario}")]
        public void Eliminar(int idUsuario)
        {
            generoService.Eliminar(idUsuario);
        }


    }
}