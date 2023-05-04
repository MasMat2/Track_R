using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class MunicipioValidatorService
    {
        private IMunicipioRepository municipioRepository;

        public MunicipioValidatorService(IMunicipioRepository municipioRepository) {
            this.municipioRepository = municipioRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeEstadoRequerido = "El estado es requerido";
        private readonly string MensajeExistencia = "El municipio no existe";
        private readonly string MensajeDuplicado = "El municipio ya existe";
        private readonly string MensajeClaveDuplicado = "La clave ya existe";

        private readonly string MensajeDependencia = "El municipio esta asociado al menos a una localidad y no se puede eliminar";

        private static readonly int LongitudNombre = 50;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";

        public void ValidarAgregar(Municipio municipio)
        {
            ValidarRequerido(municipio);
            ValidarRango(municipio);
            ValidarDuplicado(municipio);
        }

        public void ValidarEditar(Municipio municipio)
        {
            ValidarRequerido(municipio);
            ValidarRango(municipio);
            ValidarExistencia(municipio.IdMunicipio);
            ValidarDuplicado(municipio);
        }

        public void ValidarEliminar(int idMunicipio)
        {
            var municipio = municipioRepository.Consultar(idMunicipio);

            ValidarExistencia(idMunicipio);
            ValidarDependencia(idMunicipio);
        }

        public void ValidarRequerido(Municipio municipio)
        {
            Validator.ValidarRequerido(municipio.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(municipio.IdEstado, MensajeEstadoRequerido);
        }

        public void ValidarRango(Municipio municipio)
        {
            Validator.ValidarLongitudRangoString(municipio.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(MunicipioDto municipio)
        {
            if (municipio == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idMunicipio)
        {
            var municipio = municipioRepository.Consultar(idMunicipio);

            if (municipio == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Municipio municipio)
        {
            var municipioDuplicado = municipioRepository.Consultar(municipio.Nombre, municipio.IdEstado);

            if (municipioDuplicado != null && municipio.IdMunicipio != municipioDuplicado.IdMunicipio)
            {
                throw new CdisException(MensajeDuplicado);
            }

            var municipioClaveDuplicada = municipioRepository.ConsultarPorClave(municipio.Clave);

            if (municipioClaveDuplicada != null && municipio.IdMunicipio != municipioClaveDuplicada.IdMunicipio)
            {
                throw new CdisException(MensajeClaveDuplicado);
            }

        }

        public void ValidarDependencia(int idMunicipio)
        {
            bool dependencia = municipioRepository.ConsultarDependencias(idMunicipio);

            if (dependencia)
                throw new CdisException(MensajeDependencia);
        }
    }
}
