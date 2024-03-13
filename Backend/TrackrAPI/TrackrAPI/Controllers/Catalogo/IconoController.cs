using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class IconoController : ControllerBase
    {
        private IconoService estadoService;

        public IconoController(IconoService estadoService)
        {
            this.estadoService = estadoService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<Icono> ConsultarGeneral()
        {
            var iconos = estadoService.ConsultarGeneral();
            return iconos;
        }
    }
}