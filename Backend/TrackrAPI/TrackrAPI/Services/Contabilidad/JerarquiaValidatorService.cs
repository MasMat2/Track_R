using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Contabilidad;
using TrackrAPI.Repositorys.Distribucion;
using System;
using System.Collections.Generic;

namespace TrackrAPI.Services.Contabilidad
{
    public class JerarquiaValidatorService
    {
        private IJerarquiaRepository jerarquiaRepository;

        public JerarquiaValidatorService(IJerarquiaRepository jerarquiaRepository)
        {
            this.jerarquiaRepository = jerarquiaRepository;
        }
        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeExistencia = "La jerarquía no existe";

        private readonly string MensajeNombreFormato = "El nombre es de formato alfanumérico";


        private static readonly int LongitudMaximaNombre = 200;

        private readonly string MensajeLongitudNombre = $"La longitud máxima del número exterior son { LongitudMaximaNombre } caracteres";

        public void ValidarAgregar(Jerarquia jerarquia)
        {
            ValidarRequerido(jerarquia);
            ValidarRango(jerarquia);
            ValidarFormato(jerarquia);
        }

        public void ValidarEditar(Jerarquia jerarquia)
        {
            ValidarRequerido(jerarquia);
            ValidarRango(jerarquia);
            ValidarExistencia(jerarquia.IdJerarquia);
            ValidarFormato(jerarquia);
        }
        public void ValidarEliminar(int idJerarquia)
        {
            ValidarExistencia(idJerarquia);
        }
        public void ValidarFormato(Jerarquia jerarquia)
        {
            Validator.ValidarAlfanumerico(jerarquia.Nombre, MensajeNombreFormato);
        }

        public void ValidarRequerido(Jerarquia jerarquia)
        {
            Validator.ValidarRequerido(jerarquia.Nombre, MensajeNombreRequerido);
        }
        public void ValidarExistencia(int  idJerarquia)
        {
            Jerarquia jerarquia = jerarquiaRepository.Consultar(idJerarquia);
            if (jerarquia == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }
        public void ValidarRango(Jerarquia jerarquia)
        {
            Validator.ValidarLongitudMaximaString(jerarquia.Nombre, LongitudMaximaNombre, MensajeLongitudNombre);
        }

    }
}
