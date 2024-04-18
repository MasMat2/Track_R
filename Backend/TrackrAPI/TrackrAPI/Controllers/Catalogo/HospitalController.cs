using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private HospitalService hospitalService;
        private UsuarioService usuarioService;

        public HospitalController(HospitalService hospitalService, UsuarioService usuarioService)
        {
            this.hospitalService = hospitalService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarPorCompaniaParaGrid")]
        public IEnumerable<HospitalGridDto> ConsultarPorCompaniaParaGrid()
        {
            var idUsuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this)).IdCompania;

            return hospitalService.ConsultarPorCompaniaParaGrid((int)idUsuario);
        }

        [HttpGet]
        [Route("consultar/{idHospital}")]
        public HospitalDto Consultar(int idHospital)
        {
            return hospitalService.ConsultarDto(idHospital);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<HospitalGridDto> ConsultarGeneral()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return hospitalService.ConsultarGeneral((int) usuario.IdCompania);
        }

        [HttpGet]
        [Route("consultarPorCompania/{idHotel}")]
        public IEnumerable<HospitalDto> ConsultarPorCompania(int idHotel)
        {
            return hospitalService.ConsultarPorCompania(idHotel);
        }

        [HttpGet]
        [Route("consultarPorCompaniaSesion")]
        public IEnumerable<HospitalDto> ConsultarPorCompaniaSesion()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return hospitalService.ConsultarPorCompania((int)usuario.IdCompania);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(Hospital hospital)
        {
            return hospitalService.Agregar(hospital);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Hospital hospital)
        {
            hospitalService.Editar(hospital);
        }

        [HttpDelete]
        [Route("eliminar/{idHotel}")]
        public void Eliminar(int idHotel)
        {
            hospitalService.Eliminar(idHotel);
        }

        [Route("consultarPorUsuarioSesion")]
        public HospitalDto ConsultarPorUsuarioSesion()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return hospitalService.ConsultarDto((int)usuario.IdHospital);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("consultarTodosParaSelector/{idDominio}")]
        public IEnumerable<HospitalDto> ConsultarTodosParaSelector(int idDominio)
        {
            return hospitalService.ConsultarTodosParaSelector(idDominio);
        }

        [HttpGet]
        [Route("consultarDisponiblesParaListaPrecio/{idListaPrecioSeleccionada}")]
        public IEnumerable<HospitalDto> ConsultarDisponiblesParaListaPrecio(int? idListaPrecioSeleccionada)
        {
            return hospitalService.ConsultarDisponiblesParaListaPrecio(idListaPrecioSeleccionada);
        }
    }
}