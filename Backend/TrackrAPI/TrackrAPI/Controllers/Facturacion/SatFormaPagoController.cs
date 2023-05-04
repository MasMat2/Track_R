using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services.Facturacion;

namespace TrackrAPI.Controllers.Facturacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatFormaPagoController : ControllerBase
    {
        private SatFormaPagoService satFormaPagoService;

        public SatFormaPagoController(SatFormaPagoService satFormaPagoService)
        {
            this.satFormaPagoService = satFormaPagoService;
        }

        [HttpGet]
        [Route("consultarParaSelector")]
        public IEnumerable<SatFormaPago> ConsultarParaSelector()
        {
            return satFormaPagoService.ConsultarParaSelector();
        }
    }
}
