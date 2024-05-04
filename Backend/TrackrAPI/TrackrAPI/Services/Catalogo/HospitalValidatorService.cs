using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class HospitalValidatorService
    {

        private readonly IHospitalRepository hospitalRepository;

        public HospitalValidatorService(IHospitalRepository hospitalRepository)
        {
            this.hospitalRepository = hospitalRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeRFCRequerido = "El RFC es requerido";
        private readonly string MensajeCalleRequerido = "La calle es requerida";
        private readonly string MensajeNumExteriorRequerido = "El número exterior es requerido";
        private readonly string MensajeColoniaRequerido = "La colonia es requerida";
        private readonly string MensajeMunicipioRequerido = "El municipio es requerido";
        private readonly string MensajeEstadoRequerido = "El estado es requerido";
        private readonly string MensajeCodigoPostalRequerido = "El codigo postal es requerido";
        private readonly string MensajeCorreoElectronicoRequerido = "El correo electrónico es requerido";
        private readonly string MensajeRegimenFiscalRequerido = "El regimen fiscal es requerido";
        private readonly string MensajeCuentaRequerido = "La cuenta es requerida";
        private readonly string MensajeCableRequerido = "La CLABE es requerida";
        private readonly string MensajePredeterminadaRequerido = "El campo predeterminada es requerido";
        private readonly string MensajeExistencia = "La locación no existe";

        private readonly string MensajeDuplicado = "Ya existe una locación con el mismo rfc";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudRFC = 13;
        private static readonly int LongitudCalle = 50;
        private static readonly int LongitudNumInterior = 6;
        private static readonly int LongitudExterior = 6;
        private static readonly int LongitudColonia = 50;
        private static readonly int LongitudCiudad = 50;
        private static readonly int LongitudCodigoPostal = 6;
        private static readonly int LongitudTelefono = 10;
        private static readonly int LongitudCorreoElectronico = 50;
        private static readonly int LongitudCuenta = 50;
        private static readonly int LongitudCable = 50;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre} caracteres";
        private readonly string MensajeRFCLongitud = $"La longitud máxima del RFC son {LongitudRFC} caracteres";
        private readonly string MensajeCalleLongitud = $"La longitud máxima de la calle son {LongitudCalle} caracteres";
        private readonly string MensajeNumInteriorLongitud = $"La longitud máxima del núm. interior son {LongitudNumInterior} caracteres";
        private readonly string MensajeExteriorLongitud = $"La longitud máxima del núm. exterior son {LongitudExterior} caracteres";
        private readonly string MensajeColoniaLongitud = $"La longitud máxima de la colonia son {LongitudColonia} caracteres";
        private readonly string MensajeCiudadLongitud = $"La longitud máxima del la cuidad son {LongitudCiudad} caracteres";
        private readonly string MensajeCodigoPostalLongitud = $"La longitud máxima del código postal son {LongitudCodigoPostal} caracteres";
        private readonly string MensajeTelefonoLongitud = $"La longitud máxima del teléfono son {LongitudTelefono} caracteres";
        private readonly string MensajeCorreoElectronicoLongitud = $"La longitud máxima del correo electrónico son {LongitudCorreoElectronico} caracteres";
        private readonly string MensajeCuentaLongitud = $"La longitud máxima de la cuenta son {LongitudCuenta} caracteres";
        private readonly string MensajeCableLongitud = $"La longitud máxima de la CLABE son {LongitudCable} caracteres";

        public void ValidarAgregar(Hospital hospital)
        {
            ValidarRequerido(hospital);
            ValidarDuplicado(hospital);
            ValidarRango(hospital);
            ValidarConfiguracionAlmacen(hospital);
        }

        public void ValidarEditar(Hospital hospital)
        {
            ValidarDuplicado(hospital);
            ValidarRequerido(hospital);
            ValidarRango(hospital);
            ValidarExistencia(hospital.IdHospital);
            ValidarConfiguracionAlmacen(hospital);
        }

        public void ValidarEliminar(int idHospital)
        {
            Hospital hospital = hospitalRepository.ConsultarConDependencias(idHospital);
            ValidarExistencia(idHospital);
            ValidarDependencia(hospital);
            ValidarPredeterminada(hospital, true);
        }

        public void ValidarRequerido(Hospital hospital)
        {
            Validator.ValidarRequerido(hospital.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(hospital.Rfc, MensajeRFCRequerido);
            Validator.ValidarRequerido(hospital.Calle, MensajeCalleRequerido);
            Validator.ValidarRequerido(hospital.NumeroExterior, MensajeNumExteriorRequerido);
            Validator.ValidarRequerido(hospital.Colonia, MensajeColoniaRequerido);
            Validator.ValidarRequerido(hospital.IdEstado, MensajeEstadoRequerido);
            Validator.ValidarRequerido(hospital.IdMunicipio, MensajeMunicipioRequerido);
            Validator.ValidarRequerido(hospital.CodigoPostal, MensajeCodigoPostalRequerido);
            Validator.ValidarRequerido(hospital.Correo, MensajeCorreoElectronicoRequerido);
            Validator.ValidarRequerido(hospital.IdRegimenFiscal, MensajeRegimenFiscalRequerido);
            Validator.ValidarRequerido(hospital.Cuenta, MensajeCuentaRequerido);
            Validator.ValidarRequerido(hospital.Clabe, MensajeCableRequerido);
            Validator.ValidarRequerido(hospital.Predeterminada, MensajePredeterminadaRequerido);
        }

        public void ValidarConfiguracionesGenerales(Hospital hospital)
        {
            ValidarExistencia(hospital.IdHospital);
        }

        public void ValidarRango(Hospital hospital)
        {
            Validator.ValidarLongitudRangoString(hospital.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(hospital.Rfc, LongitudRFC, MensajeRFCLongitud);
            Validator.ValidarLongitudRangoString(hospital.Calle, LongitudCalle, MensajeCalleLongitud);
            Validator.ValidarLongitudRangoString(hospital.NumeroInterior, LongitudNumInterior, MensajeNumInteriorLongitud);
            Validator.ValidarLongitudRangoString(hospital.NumeroExterior, LongitudExterior, MensajeExteriorLongitud);
            Validator.ValidarLongitudRangoString(hospital.Colonia, LongitudColonia, MensajeColoniaLongitud);
            Validator.ValidarLongitudRangoString(hospital.Ciudad, LongitudCiudad, MensajeCiudadLongitud);
            Validator.ValidarLongitudRangoString(hospital.CodigoPostal, LongitudCodigoPostal, MensajeCodigoPostalLongitud);
            Validator.ValidarLongitudRangoString(hospital.Telefono, LongitudTelefono, MensajeTelefonoLongitud);
            Validator.ValidarLongitudRangoString(hospital.Correo, LongitudCorreoElectronico, MensajeCorreoElectronicoLongitud);
            Validator.ValidarLongitudRangoString(hospital.Cuenta, LongitudCuenta, MensajeCuentaLongitud);
            Validator.ValidarLongitudRangoString(hospital.Clabe, LongitudCable, MensajeCableLongitud);
        }

        public void ValidarExistencia(int idHospital)
        {
            Hospital hospital = hospitalRepository.Consultar(idHospital);

            if (hospital == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Hospital hospital)
        {
            Hospital hospitalDuplicado = hospitalRepository.Consultar(hospital.Rfc);

            if (hospitalDuplicado != null &&
                hospitalDuplicado.IdHospital != hospital.IdHospital &&
                hospitalDuplicado.IdCompania != hospital.IdCompania)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(Hospital hospital)
        {
            //if (hospital.Cita.Any())
            //{
            //    throw new CdisException("El hospital tiene asociada al menos una cita y no se puede eliminar");
            //}

            //if (hospital.ListaPrecioClinica.Any())
            //{
            //    throw new CdisException("El hospital tiene asociada al menos una lista de precio y no se puede eliminar");
            //}

            if (hospital.Usuario.Any())
            {
                throw new CdisException("El hospital tiene asociado al menos un usuario administrador y no se puede eliminar");
            }

            //if (hospital.EntradaPersonal.Any())
            //{
            //    throw new CdisException("El hospital tiene asociado al menos una entrada de personal registrada y no se puede eliminar");
            //}
        }

        public void ValidarConfiguracionAlmacen(Hospital locacion)
        {
            if ((locacion.IdAlmacenProduccion > 0 && locacion.IdAlmacenCaduco > 0) && (locacion.IdAlmacenProduccion == locacion.IdAlmacenCaduco))
            {
                throw new CdisException("El almacén producción y almacén caduco deben de ser almacenes distintos");
            }
        }

        /* Valida la existencia de una locación predeterminada en la compañia
         * En caso de reemplazar la locación predeterminada actual por una nueva, retorna el Id de la locación antigua.
         * Retorna null cuando no es necesario reemplazar la locación predeterminada.
        */
        public int? ValidarPredeterminada(Hospital locacion, bool esEliminar = false)
        {
            HospitalDto locacionPredeterminada = hospitalRepository.ConsultarDefaultPorCompania((int)locacion.IdCompania);

            // Se intenta configurar un flujo estandár distinto al actual
            if (locacionPredeterminada != null && locacion.IdHospital != locacionPredeterminada.IdHospital && locacion.Predeterminada)
            {
                return locacionPredeterminada.IdHospital;
            }
            // No se encuentra un flujo estándar existente y el flujo actual no esta configurado para ser estándar.
            //Se comentarizó esta parte temporalmente para que se agregue la compañía
            /* else if (locacion.Predeterminada == false)
            {
                throw new CdisException("Es necesario configurar la locación estándar de la compañía");
            } */

            // Se intenta Eliminar / Editar la locacion predeterminada actual
            if ((esEliminar || locacion.Predeterminada == false) && locacion.IdHospital == locacionPredeterminada.IdHospital)
            {
                throw new CdisException("Es necesario contar con una locación predeterminada en la compañía");
            }

            return null;
        }
    }
}
