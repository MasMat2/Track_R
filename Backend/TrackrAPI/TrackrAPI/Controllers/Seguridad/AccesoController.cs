using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services;

namespace TrackrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private AccesoService accesoService;

        public AccesoController(AccesoService accesoService)
        {
            this.accesoService = accesoService;
        }

        [HttpGet]
        [Route("consultar/{idAcceso}")]
        public Acceso Consultar(int idAcceso)
        {
            return accesoService.Consultar(idAcceso);
        }
    }
}