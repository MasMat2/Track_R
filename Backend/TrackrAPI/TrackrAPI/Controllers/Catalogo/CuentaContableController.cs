using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaContableController : ControllerBase
    {
        private CuentaContableService cuentaContableService;
        private UsuarioService usuarioService;

        public CuentaContableController(CuentaContableService cuentaContableService, UsuarioService usuarioService)
        {
            this.cuentaContableService = cuentaContableService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarConPartidasAbiertas")]
        public IEnumerable<CuentaPartidaVivaGridDto> ConsultarConPartidasAbiertas(int idCompania)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return cuentaContableService.ConsultarConPartidasAbiertas((int)usuario.IdCompania);
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<CuentaContableDto> ConsultarTodosParaSelector()
        {
            int idCompania = ObtenerIdCompaniaSesion();

            return cuentaContableService.ConsultarTodosParaSelector(idCompania);
        }

        [HttpGet]
        [Route("consultarPorAgrupadorParaSelector/{idAgrupador}")]
        public IEnumerable<CuentaContableDto> ConsultarPorAgrupadorParaSelector(int idAgrupador)
        {
            return cuentaContableService.ConsultarPorAgrupadorParaSelector(idAgrupador);
        }
        [HttpGet]
        [Route("consultarPorAgrupadorParaGrid/{idAgrupador}")]
        public IEnumerable<CuentaContableGridDto> ConsultarPorAgrupadorParaGrid(int idAgrupador)
        {
            return cuentaContableService.ConsultarPorAgrupadorParaGrid(idAgrupador);
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<CuentaContableGridDto> ConsultarTodosParaGrid()
        {
            int idCompania = ObtenerIdCompaniaSesion();

            return cuentaContableService.ConsultarTodosParaGrid(idCompania);
        }

        [HttpPost]
        [Route("consultarPorFiltroParaGrid")]
        public IEnumerable<CuentaContableGridDto> ConsultarPorFiltroParaGrid(CuentaContableFiltroDto filtro)
        {
            int idCompania = ObtenerIdCompaniaSesion();

            return cuentaContableService.ConsultarPorFiltroParaGrid(idCompania,filtro);
        }

        [HttpGet]
        [Route("consultar/{idCuentaContable}")]
        public CuentaContableDto Consultar(int idCuentaContable)
        {
            return cuentaContableService.ConsultarDto(idCuentaContable);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(CuentaContable cuentaContable)
        {
            cuentaContable.IdCompania = ObtenerIdCompaniaSesion(); ;

            cuentaContableService.Agregar(cuentaContable);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(CuentaContable cuentaContable)
        {
            cuentaContable.IdCompania = ObtenerIdCompaniaSesion();

            cuentaContableService.Editar(cuentaContable);
        }

        [HttpDelete]
        [Route("eliminar/{idCuentaContable}")]
        public void Eliminar(int idCuentaContable)
        {
            cuentaContableService.Eliminar(idCuentaContable);
        }

        [HttpGet]
        [Route("consultarParaJerarquiaGrid/{idJerarquia}")]
        public IEnumerable<CuentaContable> ConsultarParaJerarquiaGrid(int idJerarquia)
        {
            return cuentaContableService.ConsultarParaJerarquiaGrid(idJerarquia);
        }

        [HttpPost]
        [Route("cargarCuentas")]
        public void CargarCuentas(ArchivoExcelDto archivo)
        {
            int idCompania = ObtenerIdCompaniaSesion();

            cuentaContableService.CargarCuentas(archivo, idCompania);
        }

        private int ObtenerIdCompaniaSesion()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            return (int)usuario.IdCompania;
        }
    }
}
