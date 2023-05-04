using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolAccesoController: ControllerBase
    {
        private RolAccesoService rolAccesoService;
        public RolAccesoController(RolAccesoService rolAccesoService)
        {
            this.rolAccesoService = rolAccesoService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<RolAcceso> ConsultarTodosParaSelector()
        {
            return rolAccesoService.ConsultarTodosParaSelector();
        }
    }
}
