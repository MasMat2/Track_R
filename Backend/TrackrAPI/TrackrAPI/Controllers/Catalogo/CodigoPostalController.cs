using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodigoPostalController: ControllerBase
    {
        private CodigoPostalService codigoPostalService;

        public CodigoPostalController(CodigoPostalService codigoPostalService)
        {
            this.codigoPostalService = codigoPostalService;
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<CodigoPostalGridDto> ConsultarTodosParaGrid()
        {
            return codigoPostalService.ConsultarTodosParaGrid();
        }

        [HttpGet]
        [Route("consultar/{idCodigoPostal}")]
        public CodigoPostalDto Consultar(int idCodigoPostal)
        {
            return codigoPostalService.ConsultarDto(idCodigoPostal);
        }

        [HttpGet]
        [Route("consultarPorCodigoPostal/{codigoPostal}")]
        public IEnumerable<CodigoPostalDto> ConsultarPorCodigoPostal(string codigoPostal)
        {
           return codigoPostalService.ConsultarPorCodigoPostal(codigoPostal);
        }

        [HttpGet]
        [Route("consultarPorMunicipio/{idMunicipio}")]
        public IEnumerable<CodigoPostalDto> ConsultarPorMunicipio(int idMunicipio)
        {
            return codigoPostalService.ConsultarPorMunicipio(idMunicipio);
        }

        [HttpGet]
        [Route("consultarPorPaisBusqueda/{codigoPostal}/{idPais}")]
        public IEnumerable<CodigoPostalDto> ConsultarPorPaisBusqueda(string codigoPostal, int idPais)
        {
            return codigoPostalService.ConsultarPorPaisBusqueda(codigoPostal, idPais);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(CodigoPostal codigoPostal)
        {
            codigoPostalService.Agregar(codigoPostal);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(CodigoPostal codigoPostal)
        {
            codigoPostalService.Editar(codigoPostal);
        }

        [HttpDelete]
        [Route("eliminar/{idCodigoPostal}")]
        public void Eliminar(int idCodigoPostal)
        {
            codigoPostalService.Eliminar(idCodigoPostal);
        }

    }
}
