using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;
using TrackrAPI.Helpers;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoController : ControllerBase
    {
        private ConceptoService conceptoService;
        private UsuarioService usuarioService;

        public ConceptoController(ConceptoService conceptoService, UsuarioService usuarioService)
        {
            this.conceptoService = conceptoService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<ConceptoGridDto> ConsultarTodosParaGrid()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            return conceptoService.ConsultarTodosParaGrid(idCompania);
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<ConceptoGridDto> ConsultarTodosParaSelector()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            return conceptoService.ConsultarTodosParaSelector(idCompania);
        }

        [HttpGet]
        [Route("consultarSelectorParaPresentacion")]
        public IEnumerable<ConceptoSelectorDto> ConsultarSelectorParaPresentacion()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            return conceptoService.ConsultarSelectorParaPresentacion(idCompania);
        }


        [HttpGet]
        [Route("consultarParaDesgloseSelector")]
        public IEnumerable<ConceptoGridDto> ConsultarParaDesgloseSelector()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            return conceptoService.ConsultarParaDesgloseSelector(idCompania);
        }

        [HttpGet]
        [Route("consultarOperativosParaSelector")]
        public IEnumerable<ConceptoGridDto> ConsultarOperativosParaSelector()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            return conceptoService.ConsultarOperativosParaSelector(idCompania);
        }

        [HttpGet]
        [Route("consultarPorTipo/{claveTipo}")]
        public IEnumerable<Concepto> ConsultarPorTipo(string claveTipo)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            return conceptoService.ConsultarPorTipo(claveTipo, idCompania);
        }

        [HttpGet]
        [Route("consultar/{idConcepto}")]
        public ConceptoDto Consultar(int idConcepto)
        {
            return conceptoService.ConsultarDto(idConcepto);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(ConceptoFormularioDto concepto)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            concepto.IdCompania = idCompania;
            conceptoService.Agregar(concepto);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(ConceptoFormularioDto concepto)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            concepto.IdCompania = idCompania;
            conceptoService.Editar(concepto);
        }

        [HttpDelete]
        [Route("eliminar/{idConcepto}")]
        public void Eliminar(int idConcepto)
        {
            conceptoService.Eliminar(idConcepto);
        }
    }
}
