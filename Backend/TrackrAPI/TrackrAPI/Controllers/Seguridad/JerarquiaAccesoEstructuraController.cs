using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Services.Seguridad;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System.Collections.Generic;
using CanalDistAPI.Dtos.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class JerarquiaAccesoEstructuraController : ControllerBase
    {
        private JerarquiaAccesoEstructuraService jerarquiaAccesoEstructuraService;
        public JerarquiaAccesoEstructuraController(JerarquiaAccesoEstructuraService jerarquiaAccesoEstructuraService)
        {
            this.jerarquiaAccesoEstructuraService = jerarquiaAccesoEstructuraService;
        }

        [HttpGet]
        [Route("consultarPorJerarquia/{idJerarquia}")]
        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquia(int idJerarquia)
        {
            return jerarquiaAccesoEstructuraService.ConsultarPorJerarquiaArbol(idJerarquia);
        }

        [HttpGet]
        [Route("consultarPorJerarquiaParaSelector/{idJerarquia}")]
        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaParaSelector(int idJerarquia)
        {
            return jerarquiaAccesoEstructuraService.ConsultarPorJerarquiaParaSelector(idJerarquia);
        }

        [HttpGet("consultarArbol/{idJerarquia}")]
        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarArbol(int idJerarquia)
        {
            return jerarquiaAccesoEstructuraService.ConsultarArbol(idJerarquia);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(List<JerarquiaAccesoEstructuraDto> jerarquias)
        {
            jerarquiaAccesoEstructuraService.Agregar(jerarquias);
        }

        [HttpDelete]
        [Route("eliminar/{idJerarquiaAccesoEstructura}")]
        public void Eliminar(int idJerarquiaAccesoEstructura)
        {
            jerarquiaAccesoEstructuraService.Eliminar(idJerarquiaAccesoEstructura);
        }
    }
}