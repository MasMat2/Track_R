using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private BancoService bancoService;

        public BancoController(BancoService bancoService)
        {
            this.bancoService = bancoService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<BancoDto> ConsultarTodosParaSelector()
        {
            return bancoService.ConsultarTodosParaSelector();
        }
    }
}
