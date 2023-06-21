using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class AyudaSeccionController : ControllerBase
    {
        private AyudaSeccionService ayudaSeccionService;
        public AyudaSeccionController(AyudaSeccionService ayudaSeccionService)
        {
            this.ayudaSeccionService = ayudaSeccionService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<AyudaSeccionSelectorDto> ConsultarParaSelector()
        {
            return ayudaSeccionService.ConsultarParaSelector();
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(AyudaSeccion ayudaSeccion)
        {
            ayudaSeccionService.Agregar(ayudaSeccion);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(AyudaSeccion ayudaSeccion)
        {
            ayudaSeccionService.Editar(ayudaSeccion);
        }

        [HttpDelete]
        [Route("eliminar/{idAyudaSeccion}")]
        public void Eliminar(int idAyudaSeccion)
        {
            ayudaSeccionService.Eliminar(idAyudaSeccion);
        }
    }
}
