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
    public class CompaniaController : ControllerBase
    {
        private readonly CompaniaService companiaService;
        private readonly UsuarioService usuarioService;

        public CompaniaController(CompaniaService companiaService, UsuarioService usuarioService)
        {
            this.companiaService = companiaService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<CompaniaDto> ConsultarGeneral()
        {
            return companiaService.ConsultarGeneral();
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<CompaniaSelectorDto> ConsultarTodosParaSelector()
        {
            return companiaService.ConsultarTodosParaSelector();
        }

        [HttpPost]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<CompaniaDto> ConsultarTodosParaGrid(CompaniaFiltroDto filtro)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return companiaService.ConsultarTodosParaGrid(filtro, usuario.IdCompaniaNavigation.Clave);
        }

        [HttpGet]
        [Route("consultarPorUsuarioPermiso")]
        public IEnumerable<CompaniaDto> ConsultarPorUsuarioPermiso()
        {
            return companiaService.ConsultarPorUsuarioPermiso(Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpGet]
        [Route("consultar/{idHospital}")]
        public CompaniaDto Consultar(int idHospital)
        {
            return companiaService.ConsultarDto(idHospital);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(CompaniaDto companiaDto)
        {
            return companiaService.Agregar(companiaDto);
        }


        [HttpPut]
        [Route("editar")]
        public void Editar(Compania compania)
        {
            companiaService.Editar(compania);
        }

        [HttpDelete]
        [Route("eliminar/{idCompania}")]
        public void Eliminar(int idCompania)
        {
            companiaService.Eliminar(idCompania);
        }

        [HttpGet]
        [Route("consultarPorUsuario")]
        public CompaniaDto ConsultarPorUsuario()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = usuario.IdCompania;

            if (idCompania >= 0)
            {
                return companiaService.ConsultarDto(idCompania); ;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("consultarLogotipo/{empresa}")]
        public CompaniaDto ConsultarLogotipo(string empresa)
        {
            return companiaService.ConsultarPorIdentificadorUrl(empresa);
        }


        [HttpGet]
        [Route("consultarClaveCompaniaUsuarioSesion/")]
        public string ConsultarCompaniaUsuarioSesion()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuario.IdCompaniaNavigation.Clave;
        }
    }
}