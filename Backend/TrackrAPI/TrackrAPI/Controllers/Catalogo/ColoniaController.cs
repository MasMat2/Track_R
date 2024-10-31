using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Dtos.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColoniaController : ControllerBase
    {
        private ColoniaService coloniaService;

        public ColoniaController(
            ColoniaService coloniaService
        )
        {
            this.coloniaService = coloniaService;
        }

        [HttpGet]
        [Route("consultarPorCodigoParaSelector/{codigoPostal}")]
        public IEnumerable<Colonia> ConsultarPorCodigoParaSelector(string codigoPostal)
        {
            var temp = coloniaService.ConsultarPorCodigoParaSelector(codigoPostal);
            return coloniaService.ConsultarPorCodigoParaSelector(codigoPostal);
        }

        [HttpGet]
        [Route("consultarParaGrid")]
        public IEnumerable<ColoniaGridDto> ConsultarParaGrid()
        {
            return coloniaService.ConsultarParaGrid();
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Colonia colonia)
        {
            coloniaService.Agregar(colonia);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Colonia colonia)
        {
            coloniaService.Editar(colonia);
        }

        [HttpDelete]
        [Route("eliminar/{idColonia}")]
        public void Eliminar(int idColonia)
        {
            coloniaService.Eliminar(idColonia);
        }

        [HttpGet]
        [Route("actualizarPlantillaExcel")] 
        public void ActualizarPlantillaExcel()
        {
            coloniaService.ActualizarPlantillaExcel();
        }
    }
}
