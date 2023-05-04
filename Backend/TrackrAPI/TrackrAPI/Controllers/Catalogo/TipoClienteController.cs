using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TrackrAPI.Services;
using TrackrAPI.Services.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Helpers;

namespace TrackrAPI.Controllers.Catalogo
{

    [Route("api/tipoCliente")]
    [ApiController]
    public class TipoClienteController : ControllerBase {

        private UsuarioService usuarioService;
        private TipoClienteLogic tipoClienteLogic;

        public TipoClienteController(UsuarioService usuarioService, TipoClienteLogic tipoClienteLogic)
        {
            this.usuarioService = usuarioService;
            this.tipoClienteLogic = tipoClienteLogic;
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public List<TipoCliente> ConsultarPorCompania() {
            var idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            var usuario = usuarioService.Consultar(idUsuario);

            return tipoClienteLogic.ConsultarPorCompania((int)usuario.IdCompania);
        }
    }

}
