using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionCaja;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.GestionCaja
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private MetodoPagoService metodoPagoService;

        public MetodoPagoController(
            MetodoPagoService metodoPagoService
        )
        {
            this.metodoPagoService = metodoPagoService;
        }

        [HttpGet]
        [Route("consultarTodos")]
        public IEnumerable<MetodoPago> ConsultarTodos()
        {
            return metodoPagoService.ConsultarTodos();
        }

        [HttpGet]
        [Route("consultarPorClave/{clave}")]
        public MetodoPago ConsultarPorClave(string clave)
        {
            return metodoPagoService.ConsultarPorClave(clave);
        }
    }
}
