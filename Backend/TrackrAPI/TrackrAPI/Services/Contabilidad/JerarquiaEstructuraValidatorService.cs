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
    public class JerarquiaEstructuraValidatorService
    {
        private IJerarquiaEstructuraRepository jerarquiaEstructuraRepository;

        public JerarquiaEstructuraValidatorService(IJerarquiaEstructuraRepository jerarquiaEstructuraRepository)
        {
            this.jerarquiaEstructuraRepository = jerarquiaEstructuraRepository;
        }
        private readonly string MensajeNivelRequerido = "El nivel es requerido";
        private readonly string MensajeJerarquiaRequerido = "La jerarquía es requerida";
        private readonly string MensajeExistencia = "La estructura de jerarquía no existe";


        private static readonly int LongitudMaximaNumero = 20;
        private static readonly int LongitudMaximaDescripcion = 200;
        private static readonly int LongitudMaximaRuta = 500;

        private readonly string MensajeLongitudNumero = $"La longitud máxima del número exterior son { LongitudMaximaNumero } caracteres";
        private readonly string MensajeLongitudDescripcion = $"La longitud máxima del número exterior son { LongitudMaximaDescripcion } caracteres";
        private readonly string MensajeLongitudRuta = $"La longitud máxima del número exterior son { LongitudMaximaRuta } caracteres";

        public void ValidarAgregar(JerarquiaEstructura jerarquiaEstructura)
        {
            ValidarRequerido(jerarquiaEstructura);
            ValidarRango(jerarquiaEstructura);
        }

        public void ValidarEditar(JerarquiaEstructura jerarquiaEstructura)
        {
            ValidarRequerido(jerarquiaEstructura);
            ValidarRango(jerarquiaEstructura);
            ValidarExistencia(jerarquiaEstructura.IdJerarquiaEstructura);
        }
        public void ValidarEliminar(int idJerarquia)
        {
            ValidarExistencia(idJerarquia);
        }

        public void ValidarRequerido(JerarquiaEstructura jerarquiaEstructura)
        {
            Validator.ValidarRequerido(jerarquiaEstructura.IdJerarquia, MensajeJerarquiaRequerido);
        }
        public void ValidarExistencia(int  idJerarquiaEstructura)
        {
            JerarquiaEstructura jerarquiaEstructura = jerarquiaEstructuraRepository.Consultar(idJerarquiaEstructura);
            if (jerarquiaEstructura == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }
        public void ValidarRango(JerarquiaEstructura jerarquiaEstructura)
        {
            Validator.ValidarLongitudMaximaString(jerarquiaEstructura.Numero, LongitudMaximaNumero, MensajeLongitudNumero);
            Validator.ValidarLongitudMaximaString(jerarquiaEstructura.Descripcion, LongitudMaximaDescripcion, MensajeLongitudDescripcion);
            Validator.ValidarLongitudMaximaString(jerarquiaEstructura.Ruta, LongitudMaximaRuta, MensajeLongitudRuta);
        }

    }
}
