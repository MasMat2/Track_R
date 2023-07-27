using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private AreaService areaService;
        private UsuarioService usuarioService;

        public AreaController(AreaService areaService,
            UsuarioService usuarioService)
        {
            this.areaService = areaService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarParaSelector")]
        public IEnumerable<AreaDto> ConsultarParaSelector()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return areaService.ConsultarParaSelector((int)usuario.IdCompania);
        }

        [HttpGet]
        [Route("consultar/{idArea}")]
        public AreaDto Consultar(int idArea)
        {
            return areaService.ConsultarDto(idArea);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Area area)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            area.IdCompania = usuario.IdCompania;
            areaService.Agregar(area);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Area area)
        {
            areaService.Editar(area);
        }

        [HttpDelete]
        [Route("eliminar/{idArea}")]
        public void Eliminar(int idArea)
        {
            areaService.Eliminar(idArea);
        }
    }
}
