using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TituloAcademicoController : ControllerBase
    {
        private TituloAcademicoService tituloAcademicoService;

        public TituloAcademicoController(TituloAcademicoService tituloAcademicoService)
        {
            this.tituloAcademicoService = tituloAcademicoService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<TituloAcademicoSelectorDto> ConsultarTodosParaSelector()
        {
            return tituloAcademicoService.ConsultarTodosParaSelector();
        }

        [Route("consultarTodosParaGrid")]
        public IEnumerable<TituloAcademicoGridDto> ConsultarTodosParaGrid()
        {
            return tituloAcademicoService.ConsultarTodosParaGrid();
        }

        [HttpGet]
        [Route("consultar/{idTituloAcademico}")]
        public TituloAcademicoDto Consultar(int idTituloAcademico)
        {
            return tituloAcademicoService.ConsultarDto(idTituloAcademico);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(TituloAcademico tituloAcademico)
        {
            tituloAcademicoService.Agregar(tituloAcademico);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(TituloAcademico tituloAcademico)
        {
            tituloAcademicoService.Editar(tituloAcademico);
        }

        [HttpDelete]
        [Route("eliminar/{idTituloAcademico}")]
        public void Eliminar(int idTituloAcademico)
        {
            tituloAcademicoService.Eliminar(idTituloAcademico);
        }
    }
}
