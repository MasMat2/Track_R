using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class JerarquiaAccesoController : ControllerBase
    {
        private readonly JerarquiaAccesoService jerarquiaAccesoService;
        private readonly UsuarioService usuarioService;
        private readonly CompaniaService companiaService;
        public JerarquiaAccesoController
        (
            JerarquiaAccesoService jerarquiaAccesoService,
            UsuarioService usuarioService,
            CompaniaService companiaService
        )
        {
            this.jerarquiaAccesoService = jerarquiaAccesoService;
            this.usuarioService = usuarioService;
            this.companiaService = companiaService;
        }

        [HttpGet]
        [Route("consultar/{idJerarquiaAcceso}")]
        public JerarquiaAccesoDto Consultar(int idJerarquiaAcceso)
        {
            return jerarquiaAccesoService.Consultar(idJerarquiaAcceso);
        }

        [HttpGet]
        [Route("consultarParaGrid")]
        public IEnumerable<JerarquiaAccesoDto> ConsultarParaGrid()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return jerarquiaAccesoService.ConsultarParaGrid((int)usuario.IdCompania);
        }

        [HttpGet]
        [Route("consultarParaSelector")]
        public IEnumerable<JerarquiaAccesoDto> ConsultarParaSelector()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            CompaniaDto compania = companiaService.ConsultarDto((int)usuario.IdCompania);
            string claveTipoCompania = "";

            if (compania.Clave != GeneralConstant.ClaveCompaniaBase)
            {
                claveTipoCompania = compania.ClaveTipoCompania;
            }

            return jerarquiaAccesoService.ConsultarParaSelector(claveTipoCompania);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(JerarquiaAccesoDto jerarquiaAccesoDto)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            jerarquiaAccesoDto.IdCompania = (int)usuario.IdCompania;
            jerarquiaAccesoService.Agregar(jerarquiaAccesoDto);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(JerarquiaAccesoDto jerarquiaAccesoDto)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            jerarquiaAccesoDto.IdCompania = (int)usuario.IdCompania;
            jerarquiaAccesoService.Editar(jerarquiaAccesoDto);
        }

        [HttpDelete]
        [Route("eliminar/{idJerarquiaAcceso}")]
        public void Eliminar(int idJerarquiaAcceso)
        {
            jerarquiaAccesoService.Eliminar(idJerarquiaAcceso);
        }
    }
}
