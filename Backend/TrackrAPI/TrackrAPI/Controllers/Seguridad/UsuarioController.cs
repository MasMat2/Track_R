using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Dtos.Perfil;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService usuarioService;

        public object idUsuario { get; private set; }

        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarEncabezado")]
        public UsuarioEncabezadoDto ConsultarEncabezado()
        {
            return usuarioService.ConsultarEncabezado(Utileria.ObtenerIdUsuarioSesion(this));
        }

        //[HttpGet]
        //[Route("consultarEncabezadoPedidoEnLinea/{empresa}")]
        //public UsuarioEncabezadoDto ConsultarEncabezadoPedidoEnLinea(string empresa)
        //{
        //    int idUsuario = Utileria.TryObtenerIdUsuarioSesion(this);
        //    string token = Utileria.ObtenerToken(this);
        //    return usuarioService.ConsultarEncabezadoPedidoEnLinea(idUsuario, empresa, token);
        //}

        [HttpGet]
        [Route("existeUsuarioLogeado")]
        public bool ExisteUsuarioLogeado()
        {
            return Utileria.TryObtenerIdUsuarioSesion(this) > 0;
        }

        [HttpGet]
        [Route("consultarOpenpayId")]
        public object ConsultarOpenpayId()
        {
            string id = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this)).OpenpayIdCustomer;
            return new { id };
        }

        [HttpGet]
        [Route("consultarPorRol/{claveRol}")]
        public IEnumerable<UsuarioDto> ConsultarPorRol(string claveRol)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarPorRol(claveRol, (int)usuario.IdCompania);
        }

        //[HttpGet]
        //[Route("consultarClinicosActivos/{claveTipoUsuario}")]
        //public IEnumerable<UsuarioDto> ConsultarClinicosActivos(string claveTipoUsuario)
        //{
        //    var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
        //    return usuarioService.ConsultarClinicosActivos(claveTipoUsuario, usuario);
        //}

        [HttpGet]
        [Route("consultarTipoDeUsuarioEnSesion")]
        public string ConsultarTipoDeUsuarioEnSesion()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuario?.IdTipoUsuarioNavigation.Clave;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("consultarExistenciaAdministrador")]
        public bool ConsultarExistenciaAdministrador([FromBody] dynamic valor)
        {
            var usuario = usuarioService.ConsultarPorUsuario(Convert.ToString(valor.correo));
            return usuario != null;
        }

        [HttpGet]
        [Route("consultar/{idUsuario}")]
        public UsuarioDto Consultar(int idUsuario)
        {
            return usuarioService.ConsultarDto(idUsuario);
        }

        [HttpGet]
        [Route("consultarPorRFC/{rfc}")]
        public Usuario ConsultarPorRFC(string rfc)
        {
            return usuarioService.ConsultarPorRFC(rfc);
        }

        [HttpGet]
        [Route("consultarPorTipoUsuario/{claveTipoUsuario}")]
        public IEnumerable<UsuarioDto> ConsultarPorTipoUsuario(string claveTipoUsuario)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarPorTipoUsuario(claveTipoUsuario, (int)usuario.IdCompania);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(UsuarioDto usuarioDto)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            usuarioDto.IdHospital = (int)usuario.IdHospital;
            usuarioDto.IdCompania = usuario.IdCompania;
            return usuarioService.Agregar(usuarioDto, (int)usuario.IdHospital, usuario.IdUsuario);
        }

        [AllowAnonymous]
        [HttpPost("agregarTrackr")]
        public int AgregarTrackr(UsuarioNuevoTrackrDto usuarioDto)
        {
            return usuarioService.AgregarTrackr(usuarioDto);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Usuario usuario)
        {
            usuarioService.Editar(usuario);
        }

        [HttpDelete]
        [Route("eliminar/{idUsuario}")]
        public void Eliminar(int idUsuario)
        {
            usuarioService.Eliminar(idUsuario);
        }

        [Route("consultarMiUsuario")]
        public UsuarioDto ConsultarMiUsuario()
        {
            return usuarioService.ConsultarDto(Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpPut]
        [Route("editarAdministrador")]
        public void EditarAdministrador(UsuarioDto usuarioDto)
        {
            usuarioService.EditarAdministrador(usuarioDto);
        }

        [HttpPut]
        [Route("editarLocacionAdministrador")]
        public void EditarLocacionAdministrador(Usuario usuario)
        {
            usuarioService.EditarLocacionAdministrador(usuario);
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<UsuarioGridDto> ConsultarGeneral()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarGeneral((int)usuario.IdCompania);
        }

        //[HttpPost]
        //[Route("ConsultarPorRolActivosParaSelector")]
        //public IEnumerable<UsuarioDto> ConsultarPorRolParaSelector(List<int> roles)
        //{
        //    var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
        //    return usuarioService.ConsultarPorRolActivosParaSelector(roles, (int)usuario.IdCompania, (int)usuario.IdHospital);
        //}

        [HttpPost]
        [Route("ConsultarPorRolCompaniaParaSelector")]
        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelector(List<int> roles)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarPorRolCompaniaParaSelector(roles, (int)usuario.IdCompania);
        }

        [HttpPost]
        [Route("ConsultarPorRolCompaniaParaSelectorDomicilio")]
        public IEnumerable<UsuarioDto> ConsultarPorRolCompaniaParaSelectorDomicilio(List<int> roles)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarPorRolCompaniaParaSelectorDomicilio(roles, (int)usuario.IdCompania);
        }

        //[HttpGet]
        //[Route("consultarUsuariosParaRegistrarEntrada")]
        //public IEnumerable<UsuarioDto> ConsultarUsuariosParaRegistrarEntrada()
        //{
        //    var idHospital = (int)usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this)).IdHospital;
        //    return usuarioService.ConsultarUsuariosParaRegistrarEntrada(idHospital);
        //}
        [HttpGet]
        [Route("consultarParaPuntoVenta")]
        public IEnumerable<UsuarioDto> ConsultarParaPuntoVenta()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarParaPuntoVenta((int)usuario.IdCompania);
        }

        [HttpGet]
        [Route("consultarMedico/{cedula}")]
        public UsuarioDto ConsultarMedico(string cedula)
        {
            return usuarioService.ConsultarMedico(cedula);
        }
        [HttpGet]
        [Route("consultarMedicoId/{idUsuario}")]
        public UsuarioDto ConsultarMedico(int idUsuario)
        {
            return usuarioService.ConsultarMedico(idUsuario);
        }

        [HttpPost]
        [Route("agregarMedico")]
        public int AgregarMedico(UsuarioDto usuarioDto)
        {
            return usuarioService.AgregarMedico(usuarioDto);
        }

        [HttpPost]
        [Route("consultarBusquedaGridFiltro")]
        public IEnumerable<UsuarioGridDto> ConsultarBusquedaGridFiltro(UsuarioDto filtro)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            filtro.IdCompania = usuario.IdCompania;
            return usuarioService.ConsultarBusquedaGridFiltro(filtro);
        }

        [HttpGet]
        [Route("consultarPorLocacionParaSelector")]
        public IEnumerable<UsuarioDto> ConsultarPorLocacionParaSelector()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarPorLocacionParaSelector((int)usuario.IdHospital);
        }

        [HttpGet]
        [Route("consultarPorLocacionSeleccionadaParaSelector/{idLocacion}")]
        public IEnumerable<UsuarioDto> ConsultarPorLocacionSeleccionadaParaSelector(int idLocacion)
        {
            return usuarioService.ConsultarPorLocacionParaSelector(idLocacion);
        }

        [HttpGet]
        [Route("consultarParaSelector")]
        public IEnumerable<UsuarioSelectorDto> ConsultarParaSelector()
        {
            return usuarioService.ConsultarParaSelector();
        }

        [HttpGet("consultarPorNombre/{nombre}")]
        public IEnumerable<UsuarioDto> ConsultarPorNombre(string nombre)
        {
            return usuarioService.ConsultarPorNombre(nombre);
        }

        [HttpGet("consultarInformacionGeneral")]
        public InformacionGeneralDTO ConsultarInformacionGeneral()
        {
            return usuarioService.ConsultarInformacionGeneralTrackr(Utileria.ObtenerIdUsuarioSesion(this));
        }
        [HttpGet("consultarInformacionDomicilio")]
        public InformacionDomicilioDTO ConsultarInformacionDomicilio()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return usuarioService.ConsultarInformacionDomicilioTrackr(idUsuario);
        }

        [HttpGet("consultarInformacionPerfil")]
        public InformacionPerfilTrackrDTO ConsultarInformacionPerfil()
        {
            return usuarioService.ConsultarInformacionPerfilTrackr(Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpGet("consultarInfoAntecedentes")]
        public IEnumerable<ExpedientePadecimientoDTO> ConsultarInformacionAntecedentes()
        {
            return usuarioService.ConsultarAntecedentesUsuarioTrackr(Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpGet("consultarInfoDiagnosticos")]
        public IEnumerable<ExpedientePadecimientoDTO> ConsultarInformacionDiagnosticos()
        {
            return usuarioService.ConsultarDiagnosticosUsuarioTrackr(Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpPut("actualizarInformacionGeneral")]
        public void ActualizarInformacionGeneralTrackr(InformacionGeneralDTO informacion)
        {
            usuarioService.ActualizarInformacionGeneralTrackr(informacion, Utileria.ObtenerIdUsuarioSesion(this)); 
        }

        [HttpPut("actualizarInformacionDomicilio")]
        public void ActualizarInformacionDomicilioTrackr(InformacionDomicilioDTO informacion)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            usuarioService.ActualizarInformacionDomicilioTrackr(informacion, idUsuario);
        }

        [HttpGet("consultaDomicilioPorId/{idUsuario}")]
        
        public UsuarioDomicilioDto ConsultaDomicilioPorId(int idUsuario)
        {
            return usuarioService.ConsultaDomicilioPorId(idUsuario);
        }
        [HttpGet("consultarAsistentes")]
        public IEnumerable<UsuarioDto> ConsultarAsistentes()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.ConsultarAsistentes((int)usuario.IdCompania , usuario.IdUsuario);
        }

        [HttpGet("asistentesPorDoctor")]
        public IEnumerable<AsistenteDoctorDto> ConsultarAsistentesPorDoctor()
        {
            int idDoctor  = Utileria.ObtenerIdUsuarioSesion(this);
            return usuarioService.ConsultarAsistentePorDoctor(idDoctor);
        }

        [HttpGet("misDoctores")]
        public IEnumerable<AsistenteDoctorDto> ConsultarDoctoresPorAsistente()
        {
            int idAsistente = Utileria.ObtenerIdUsuarioSesion(this);
            return usuarioService.ConsultarDoctoresPorAsistente(idAsistente);
        }

        [HttpPost("asistente/{idAsistente}")]
        public void AgregarAsistente(int idAsistente)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            usuarioService.AgregarAsistente(idUsuario , idAsistente);
        }

        [HttpDelete("asistente/{idAsistenteDoctor}")]
        public void EliminarAsistente(int idAsistenteDoctor)
        {
            usuarioService.EliminarAsistente(idAsistenteDoctor);
        }

        [HttpGet("esMedico")]
        public bool EsMedico()
        {
            var usuario = usuarioService.ConsultarDto(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.EsMedico(usuario.IdCompania , usuario.IdUsuario);
        }

        [HttpGet("esAsistente")]
        public bool EsAsistente()
        {
            var usuario = usuarioService.ConsultarDto(Utileria.ObtenerIdUsuarioSesion(this));
            return usuarioService.EsAsistente(usuario.IdCompania , usuario.IdUsuario);
        }
    }
}
