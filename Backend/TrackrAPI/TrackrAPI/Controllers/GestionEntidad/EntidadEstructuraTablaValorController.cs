﻿using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Services.GestionEntidad;
using System.Collections.Generic;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.GestionExpediente;

namespace TrackrAPI.Controllers.GestionEntidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadEstructuraTablaValorController : ControllerBase
    {
        private readonly EntidadEstructuraTablaValorService entidadEstructuraTablaValorService;

        public EntidadEstructuraTablaValorController(EntidadEstructuraTablaValorService entidadEstructuraTablaValorService)
        {
            this.entidadEstructuraTablaValorService = entidadEstructuraTablaValorService;
        }

        [HttpGet]
        [Route("consultarRegistroTablaPorTabulacion/{idEntidadEstructura},{idTabla}")]
        public IEnumerable<RegistroTablaDto> ConsultarRegistroTablaPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            return entidadEstructuraTablaValorService.ConsultarRegistroTablaPorTabulacion(idEntidadEstructura, idTabla);
        }

        [HttpGet("pestanaSeccion/{idEntidadEstructura},{idTabla}")]
        public List<RegistroTablaDto> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla)
        {
            return entidadEstructuraTablaValorService.ConsultarPorPestanaSeccion(idEntidadEstructura, idTabla);
        }

        [HttpPost]
        public void Agregar(EntidadTablaRegistroDto registro)
        {
            entidadEstructuraTablaValorService.Agregar(registro);
        }

        [HttpPost("agregarMuestra")]
        public async Task AgregarMuestra(TablaValorMuestraDTO[] muestra)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            await entidadEstructuraTablaValorService.AgregarMuestra(muestra, idUsuario);
        }

        [HttpPut]
        public void Editar(EntidadTablaRegistroDto registro)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            entidadEstructuraTablaValorService.Editar(registro , idUsuario);
        }

        [HttpPost("eliminar")]
        public void Eliminar(EntidadTablaRegistroDto registro)
        {
            entidadEstructuraTablaValorService.Eliminar(registro);
        }

        [HttpGet("valoresFueraRango/{idPadecimiento}/{idUsuario}")]
        public IEnumerable<ValoresFueraRangoGridDTO> ConsultarValoresFueraRango(int idPadecimiento, int idUsuario)
        {
            return entidadEstructuraTablaValorService.ConsultarValores(idPadecimiento, idUsuario, true);
        }

        [HttpGet("valoresFueraRangoGeneral/usuarioSesion")]
        public IEnumerable<ValoresFueraRangoGridDTO> ConsultarValoresFueraRangoUsuarioSesion()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return entidadEstructuraTablaValorService.ConsultarValoresFueraRangoGeneral(idUsuario);
        }

        [HttpGet("valoresFueraRangoGeneral/usuario/{idUsuario}")]
        public IEnumerable<ValoresFueraRangoGridDTO> ConsultarValoresFueraRangoUsuario(int idUsuario)
        {
            return entidadEstructuraTablaValorService.ConsultarValoresFueraRangoGeneral(idUsuario);
        }

        [HttpGet("valoresTodasVariables/{idPadecimiento}/{idUsuario}")]
        public IEnumerable<ValoresFueraRangoGridDTO> ConsultarValoresTodasVariables(int idPadecimiento, int idUsuario)
        {
            return entidadEstructuraTablaValorService.ConsultarValores(idPadecimiento, idUsuario, null);
        }

        [HttpGet("valoresPorClaveCampo/{idSeccionVariable}/{idUsuario}/{filtroTiempo}")]
        public Dictionary<string, List<ValoresHistogramaDTO>> ConsultarValoresPorClaveCampoFiltro(int idSeccionVariable, int idUsuario, string filtroTiempo)
        {
            return entidadEstructuraTablaValorService.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, filtroTiempo);
        }

        [HttpGet("valoresPorClaveCampo/usuarioSesion/{idSeccionVariable}/{filtroTiempo}")]
        public Dictionary<string, List<ValoresHistogramaDTO>> ConsultarValoresPorClaveCampoFiltroUsuarioSesion(int idSeccionVariable, string filtroTiempo)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return entidadEstructuraTablaValorService.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, filtroTiempo);
        }

        [HttpGet("valoresPorClaveCampoParaGrid/usuarioSesion/{idSeccionVariable}/{filtroTiempo}")]
        public ValoresPorCampoGridDTO ConsultarValoresPorClaveCampoParaGridUsuarioSesion(int idSeccionVariable, string filtroTiempo)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return entidadEstructuraTablaValorService.ConsultarValoresPorClaveCampoParaGrid(idSeccionVariable, idUsuario, filtroTiempo);
        }

        [HttpGet("grid/{idUsuario}")]
        public IEnumerable<ExpedienteMuestrasGridDTO> ConsultarGridMuestras(int idUsuario)
        {
            return entidadEstructuraTablaValorService.ConsultarGridMuestras(idUsuario);
        }
    }
}