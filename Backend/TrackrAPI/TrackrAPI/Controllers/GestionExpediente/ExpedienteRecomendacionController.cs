using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionExpediente;

namespace Trackr.Controllers.GestionExpediente
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteRecomendacionController : ControllerBase
    {

        private readonly ExpedienteRecomendacionService expedienteRecomendacionService;

        public ExpedienteRecomendacionController(ExpedienteRecomendacionService expedienteRecomendacionService)
        {
            this.expedienteRecomendacionService = expedienteRecomendacionService;
        }

        [HttpGet]
        [Route("consultar/{idUsuario}")]
        public IEnumerable<ExpedienteRecomendacionGridDTO> Consultar(int idUsuario)
        {
            return expedienteRecomendacionService.Consultar(idUsuario);
        }

        [HttpPost]
        public void Agregar(ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
        {
            int idDoctor = Utileria.ObtenerIdUsuarioSesion(this);
            expedienteRecomendacionDTO.DoctorId = idDoctor;
            expedienteRecomendacionService.Agregar(expedienteRecomendacionDTO);
        }

        [HttpPut]
        [Route("{idExpedienteRecomendacion}")]
        public void Editar(int idExpedienteRecomendacion, ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
        {
            expedienteRecomendacionService.Editar(idExpedienteRecomendacion, expedienteRecomendacionDTO);
        }

        [HttpDelete]
        [Route("{idExpedienteRecomendacion}")]
        public void Eliminar(int idExpedienteRecomendacion)
        {
            expedienteRecomendacionService.Eliminar(idExpedienteRecomendacion);
        }
    }
}