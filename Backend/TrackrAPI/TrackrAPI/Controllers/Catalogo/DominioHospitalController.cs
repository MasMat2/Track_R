using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DominioHospitalController : ControllerBase
    {
        private readonly DominioHospitalService _dominioHospitalService;

        public DominioHospitalController(DominioHospitalService dominioHospitalService)
        {
            _dominioHospitalService = dominioHospitalService;
        }

        [HttpGet]
        [Route("{idHospital}/{idDominio}")]
        public DominioHospitalDto Consultar(int idHospital, int idDominio)
        {
            Console.WriteLine(idDominio);
            Console.WriteLine(idHospital);
            return _dominioHospitalService.Consultar(idHospital,idDominio);
        }

        [HttpPost]
        public void Agregar(DominioHospitalDto dominioHospitalDto)
        {
            _dominioHospitalService.Agregar(dominioHospitalDto);
        }

        [HttpPut]
        public void Editar(DominioHospitalDto dominioHospitalDto)
        {
            _dominioHospitalService.Modificar(dominioHospitalDto);
        }

        [HttpDelete]
        public void Eliminar(DominioHospitalDto dominioHospitalDto)
        {
            _dominioHospitalService.Eliminar(dominioHospitalDto);
        }
    }
}
