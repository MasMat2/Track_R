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
    public class TipoCompaniaController : ControllerBase
    {
        private TipoCompaniaService tipoCompaniaService;

        public TipoCompaniaController(TipoCompaniaService tipoCompaniaService)
        {
            this.tipoCompaniaService = tipoCompaniaService;
        }

        [HttpGet]
        [Route("consultar/{idTipoCompaia}")]
        public TipoCompania Consultar(int idTipoCompaia)
        {
            return tipoCompaniaService.Consultar(idTipoCompaia);
        }

        [HttpGet]
        [Route("consultarParaSelector")]
        public IEnumerable<TipoCompaniaSelectorDto> ConsultarParaSelector()
        {
            return tipoCompaniaService.ConsultarParaSelector();
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<TipoCompania> ConsultarTodosParaGrid()
        {
            return tipoCompaniaService.ConsultarTodosParaGrid();
        }

        [HttpGet]
        [Route("consultarPorClave/{claveTipoCompania}")]
        public TipoCompania ConsultarPorClave(string claveTipoCompania)
        {
            return tipoCompaniaService.ConsultarPorClave(claveTipoCompania);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(TipoCompania tipoCompania)
        {
            return tipoCompaniaService.Agregar(tipoCompania);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(TipoCompania tipoCompania)
        {
            tipoCompaniaService.Editar(tipoCompania);
        }

        [HttpDelete]
        [Route("eliminar/{idTipoCompania}")]
        public void Eliminar(int idTipoCompania)
        {
            tipoCompaniaService.Eliminar(idTipoCompania);
        }
    }
}
