using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DominioHospitalController : ControllerBase
    {
        private readonly DominioHospitalService _dominioHospitalService;
        private readonly UsuarioService _usuarioService;

        public DominioHospitalController(DominioHospitalService dominioHospitalService,
                                         UsuarioService usuarioService)
        {
            _dominioHospitalService = dominioHospitalService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("{idHospital}/{idDominio}")]
        public DominioHospitalDto Consultar(int idHospital, int idDominio)
        {
            if(idHospital == 0)
            {
                var idUsuario = Utileria.TryObtenerIdUsuarioSesion(this);
                idHospital = _usuarioService.Consultar(idUsuario).IdHospital;
                Console.WriteLine(idHospital);
            }
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
