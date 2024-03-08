using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.SAT;
using TrackrAPI.Services.SAT;

namespace TrackrAPI.Controllers.SAT
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadoLocacionController : ControllerBase
    {
        private CertificadoLocacionService certificadoLocacionService;
        public CertificadoLocacionController(CertificadoLocacionService certificadoLocacionService)
        {
            this.certificadoLocacionService = certificadoLocacionService;
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(CertificadoLocacionDto[] certificados)
        {
            certificadoLocacionService.AgregarCertificados(certificados);
        }
    }
}
