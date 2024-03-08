using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class LadaController : ControllerBase
    {
        private LadaService ladaService;

        public LadaController(LadaService ladaService)
        {
            this.ladaService = ladaService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<LadaDto> ConsultarTodosParaSelector()
        {
            return ladaService.ConsultarTodosParaSelector();
        }
    }
}

