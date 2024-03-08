using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services.Inventario;

namespace TrackrAPI.Controllers.Inventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomicilioController : ControllerBase
    {
        private DomicilioService domicilioService;

        public DomicilioController(DomicilioService domicilioService)
        {
            this.domicilioService = domicilioService;
        }

        [HttpGet("consultar/{idDomicilio}")]
        public Domicilio Consultar(int idDomicilio)
        {
            return domicilioService.Consultar(idDomicilio);
        }
    }
}
