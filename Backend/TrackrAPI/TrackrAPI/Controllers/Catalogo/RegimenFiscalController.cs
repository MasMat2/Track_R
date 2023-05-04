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
    public class RegimenFiscalController : ControllerBase
    {
        private RegimenFiscalService regimenFiscalService;

        public RegimenFiscalController(RegimenFiscalService regimenFiscalService)
        {
            this.regimenFiscalService = regimenFiscalService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<RegimenFiscalDto> ConsultarTodosParaSelector()
        {
            return regimenFiscalService.ConsultarTodosParaSelector();
        }
    }
}
