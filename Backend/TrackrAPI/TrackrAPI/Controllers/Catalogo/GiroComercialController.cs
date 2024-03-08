using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiroComercialController : ControllerBase
    {
        private GiroComercialService giroComercialService;
        private UsuarioService usuarioService;

        public GiroComercialController(
            GiroComercialService giroComercialService,
            UsuarioService usuarioService
        )
        {
            this.giroComercialService = giroComercialService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarTodos")]
        public IEnumerable<GiroComercial> ConsultarTodos()
        {
            return giroComercialService.ConsultarTodos();
        }

        [HttpGet]
        [Route("consultar/{idGiroComercial}")]
        public GiroComercial Consultar(int idGiroComercial)
        {
            return giroComercialService.Consultar(idGiroComercial);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(GiroComercial giroComercial)
        {
            return giroComercialService.Agregar(giroComercial);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(GiroComercial giroComercial)
        {
            giroComercialService.Editar(giroComercial);
        }

        [HttpDelete]
        [Route("eliminar/{idGiroComercial}")]
        public void Eliminar(int idGiroComercial)
        {
            giroComercialService.Eliminar(idGiroComercial);
        }
    }
}
