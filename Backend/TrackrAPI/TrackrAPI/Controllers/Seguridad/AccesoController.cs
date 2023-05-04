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
    public class AccesoController : ControllerBase
    {
        private AccesoService accesoService;

        public AccesoController(AccesoService accesoService)
        {
            this.accesoService = accesoService;
        }

        [HttpGet]
        [Route("consultar/{idAcceso}")]
        public AccesoDto Consultar(int idAcceso)
        {
            return accesoService.ConsultarDto(idAcceso);
        }

        [HttpGet]
        [Route("consultarPorClave/{claveAcceso}")]
        public Acceso ConsultarPorClave(string claveAcceso)
        {
            return accesoService.ConsultarPorClave(claveAcceso);
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<AccesoGridDto> ConsultarGeneral()
        {
            return accesoService.ConsultarGeneral();
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Acceso acceso)
        {
            accesoService.Agregar(acceso);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Acceso acceso)
        {
            accesoService.Editar(acceso);
        }

        [HttpDelete]
        [Route("eliminar/{idAcceso}")]
        public void Eliminar(int idAcceso)
        {
            accesoService.Eliminar(idAcceso);
        }

        [HttpGet]
        [Route("consultarMenu")]
        public IEnumerable<AccesoMenuDto> ConsultarMenu()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return accesoService.ConsultarMenu(idUsuario);
        }

        [HttpGet]
        [Route("consultarMenuPorAccesoPadre/{claveAccesoPadre}")]
        public IEnumerable<AccesoMenuDto> ConsultarMenuPorAccesoPadre(string claveAccesoPadre)
        {
            return accesoService.ConsultarMenuPorAccesoPadre(claveAccesoPadre, Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpGet]
        [Route("consultarPorPerfil/{idPerfil}")]
        public IEnumerable<AccesoDto> ConsultarPorPerfil(int idPerfil)
        {
            return accesoService.ConsultarPorPerfil(idPerfil);
        }

        [HttpGet]
        [Route("consultarArbol")]
        public IEnumerable<AccesoMenuDto> ConsultarArbol()
        {
            return accesoService.ConsultarArbol(Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpGet]
        [Route("tieneAcceso/{codigoAcceso}")]
        public bool TieneAcceso(string codigoAcceso)
        {
            var idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return accesoService.TieneAcceso(idUsuario, codigoAcceso);
        }

        [HttpPost]
        [Route("tieneAccesoLista/")]
        public List<AccesoDto> TieneAccesoLista(string[] listaAccesos)
        {
            var accesoList = new List<AccesoDto>();
            var idUsuario = Utileria.ObtenerIdUsuarioSesion(this);

            for (var i = 0; i < listaAccesos.Length; i++)
            {
                AccesoDto accesoDto = new AccesoDto();
                accesoDto.TieneAcceso = accesoService.TieneAcceso(idUsuario, listaAccesos[i]);
                accesoDto.Clave = listaAccesos[i];
                accesoList.Add(accesoDto);
            }
            return accesoList;
        }

        [HttpGet]
        [Route("consultarPorRolAcceso/{idRolAcceso}")]
        public IEnumerable<AccesoGridDto> ConsultarPorRolAcceso(int idRolAcceso)
        {
            return accesoService.ConsultarPorRolAcceso(idRolAcceso);
        }

        [HttpGet]
        [Route("consultarParaReporteArbol/{idRolAcceso}")]
        public IEnumerable<AccesoGridDto> ConsultarParaReporteArbol(int idRolAcceso)
        {
            return accesoService.ConsultarParaReporteArbol(idRolAcceso);
        }

    }
}