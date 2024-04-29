using TrackrAPI.Repositorys;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class CompaniaValidatorService
    {
        private ICompaniaRepository companiaRepository;

        public CompaniaValidatorService(ICompaniaRepository companiaRepository)
        {
            this.companiaRepository = companiaRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeRFCRequerido = "El RFC es requerido";
        private readonly string MensajeCalleRequerido = "La calle es requerida";
        private readonly string MensajeNumExteriorRequerido = "El número exterior es requerido";
        private readonly string MensajeColoniaRequerido = "La colonia es requerida";
        private readonly string MensajeMunicipioRequerido = "El municipio es requerido";
        private readonly string MensajeCodigoPostalRequerido = "El código postal es requerido";
        private readonly string MensajeCorreoElectronicoRequerido = "El correo electrónico es requerido";
        private readonly string MensajePortalWebRequerido = "El portal web es requerido";
        private readonly string MensajeGiroComercialRequerido = "El giro comercial es requerido";

        private readonly string MensajeExistencia = "La compañia no existe";
        private readonly string MensajeDuplicado = "La compañia ya existe";

        private static readonly int LongitudNombre = 100;
        private static readonly int LongitudRFC = 13;
        private static readonly int LongitudCalle = 100;
        private static readonly int LongitudNumInterior = 6;
        private static readonly int LongitudExterior = 6;
        private static readonly int LongitudColonia = 100;
        private static readonly int LongitudCiudad = 100;
        private static readonly int LongitudCodigoPostal = 6;
        private static readonly int LongitudTelefono = 10;
        private static readonly int LongitudCorreoElectronico = 100;
        private static readonly int LongitudPortalWeb = 500;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeRFCLongitud = $"La longitud máxima del RFC son { LongitudRFC } caracteres";
        private readonly string MensajeCalleLongitud = $"La longitud máxima de la calle son { LongitudCalle } caracteres";
        private readonly string MensajeNumInteriorLongitud = $"La longitud máxima del núm. interior son { LongitudNumInterior } caracteres";
        private readonly string MensajeExteriorLongitud = $"La longitud máxima del núm exterior son { LongitudExterior } caracteres";
        private readonly string MensajeColoniaLongitud = $"La longitud máxima de la colonia son { LongitudColonia } caracteres";
        private readonly string MensajeCiudadLongitud = $"La longitud máxima del la cuidad son { LongitudCiudad } caracteres";
        private readonly string MensajeCodigoPostalLongitud = $"La longitud máxima del código postal son { LongitudCodigoPostal } caracteres";
        private readonly string MensajeTelefonoLongitud = $"La longitud máxima del teléfono son { LongitudTelefono } caracteres";
        private readonly string MensajeCorreoElectronicoLongitud = $"La longitud máxima del correo electrónico son { LongitudCorreoElectronico} caracteres";
        private readonly string MensajePortalWebLongitud = $"La longitud máxima del portal web son { LongitudPortalWeb } caracteres";

        public void ValidarAgregar(Compania compania)
        {
            ValidarRequerido(compania);
            ValidarFormato(compania);
            ValidarRango(compania);
        }

        public void ValidarEditar(Compania compania)
        {
            ValidarRequerido(compania);
            ValidarFormato(compania);
            ValidarRango(compania);
            ValidarExistencia(compania.IdCompania);
            //ValidarTienePolizasGeneradas(compania);
        }

        //public void ValidarTienePolizasGeneradas(Compania compania)
        //{
        //    var companiaConsultada = companiaRepository.Consultar(compania.IdCompania);

        //    if(compania.IdMoneda != companiaConsultada.IdMoneda)
        //    {
        //        var tienePolizasGeneradas = polizaRepository.TienePolizasGeneradas(compania.IdCompania);

        //        if(tienePolizasGeneradas)
        //        {
        //            throw new CdisException("No se puede cambiar la moneda porque hay polizas generadas");
        //        }
        //    }
        //}

        public void ValidarRequerido(Compania compania)
        {
            Validator.ValidarRequerido(compania.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(compania.Rfc, MensajeRFCRequerido);
            Validator.ValidarRequerido(compania.Calle, MensajeCalleRequerido);
            Validator.ValidarRequerido(compania.NumeroExterior, MensajeNumExteriorRequerido);
            Validator.ValidarRequerido(compania.Colonia, MensajeColoniaRequerido);
            Validator.ValidarRequerido(compania.IdMunicipio, MensajeMunicipioRequerido);
            Validator.ValidarRequerido(compania.CodigoPostal, MensajeCodigoPostalRequerido);
            Validator.ValidarRequerido(compania.Correo, MensajeCorreoElectronicoRequerido);
            Validator.ValidarRequerido(compania.PortalWeb, MensajePortalWebRequerido);
            Validator.ValidarRequerido(compania.IdGiroComercial, MensajeGiroComercialRequerido);
        }

        public void ValidarFormato(Compania compania)
        {

        }

        public void ValidarRango(Compania compania)
        {
            Validator.ValidarLongitudRangoString(compania.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(compania.Rfc, LongitudRFC, MensajeRFCLongitud);
            Validator.ValidarLongitudRangoString(compania.Calle, LongitudCalle, MensajeCalleLongitud);
            Validator.ValidarLongitudRangoString(compania.NumeroInterior, LongitudNumInterior, MensajeNumInteriorLongitud);
            Validator.ValidarLongitudRangoString(compania.NumeroExterior, LongitudExterior, MensajeExteriorLongitud);
            Validator.ValidarLongitudRangoString(compania.Colonia, LongitudColonia, MensajeColoniaLongitud);
            Validator.ValidarLongitudRangoString(compania.Ciudad, LongitudCiudad, MensajeCiudadLongitud);
            Validator.ValidarLongitudRangoString(compania.CodigoPostal, LongitudCodigoPostal, MensajeCodigoPostalLongitud);
            Validator.ValidarLongitudRangoString(compania.Telefono, LongitudTelefono, MensajeTelefonoLongitud);
            Validator.ValidarLongitudRangoString(compania.Correo, LongitudCorreoElectronico, MensajeCorreoElectronicoLongitud);
            Validator.ValidarLongitudRangoString(compania.PortalWeb, LongitudPortalWeb, MensajePortalWebLongitud);
        }

        public void ValidarExistencia(int idCompania)
        {
            Compania compania = companiaRepository.Consultar(idCompania);

            if (compania == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Compania compania)
        {
            Compania companiaDuplicado = companiaRepository.Consultar(compania.Rfc);

            if (companiaDuplicado != null && compania.IdCompania != companiaDuplicado.IdCompania)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }
    }
}
