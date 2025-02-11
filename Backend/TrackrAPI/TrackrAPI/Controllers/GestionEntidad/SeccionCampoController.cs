﻿using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionEntidad;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;

namespace TrackrAPI.Controllers.GestionEntidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionCampoController : ControllerBase
    {
        private readonly SeccionCampoService seccionCampoService;

        public SeccionCampoController(SeccionCampoService seccionCampoService)
        {
            this.seccionCampoService = seccionCampoService;
        }

        [HttpGet]
        [Route("consultar/{idSeccionCampo}")]
        public SeccionCampo Consultar(int idSeccionCampo)
        {
            return seccionCampoService.Consultar(idSeccionCampo);
        }

        [HttpGet]
        [Route("consultarPorSeccion/{idSeccion}")]
        public IEnumerable<SeccionCampoDto> ConsultarPorSeccion(int idSeccion)
        {
            return seccionCampoService.ConsultarPorSeccion(idSeccion);
        }

        [HttpGet]
        [Route("seccionesPadecimientos/{idPadecimiento}")]
        public IEnumerable<ExpedienteColumnaSelectorDTO> ConsultarSeccionesPadecimientos(int idPadecimiento)
        {
            return seccionCampoService.ConsultarSeccionesPadecimientos(idPadecimiento);
        }

        [HttpGet]
        [Route("seccionesPadecimientosGeneral")]
        public IEnumerable<PadecimientoMuestraDTO> ConsultarSeccionesPadecimientosGeneral()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return seccionCampoService.ConsultarSeccionesPadecimientosGeneral(idUsuario);
        }


        [HttpPost]
        [Route("agregar")]
        public void Agregar(SeccionCampo seccionCampo)
        {
            seccionCampoService.Agregar(seccionCampo);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(SeccionCampo seccionCampo)
        {
            seccionCampoService.Editar(seccionCampo);
        }

        [HttpDelete]
        [Route("eliminar/{idSeccionCampo}")]
        public void Eliminar(int idSeccionCampo)
        {
            seccionCampoService.Eliminar(idSeccionCampo);
        }
    }
}