using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    public class TipoAccesoController : ControllerBase
    {
        private TipoAccesoService tipoAccesoService;

        public TipoAccesoController(TipoAccesoService tipoAccesoService)
        {
            this.tipoAccesoService = tipoAccesoService;
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<TipoAcceso> ConsultarGeneral()
        {
            return tipoAccesoService.ConsultarGeneral();
        }

    }

}
