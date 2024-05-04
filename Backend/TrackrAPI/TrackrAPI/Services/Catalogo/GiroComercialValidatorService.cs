using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class GiroComercialValidatorService
    {
        private IGiroComercialRepository giroComercialRepository;

        public GiroComercialValidatorService(IGiroComercialRepository giroComercialRepository)
        {
            this.giroComercialRepository = giroComercialRepository;
        }

        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeNombreRequerido = "El nombre es requerido";

        private readonly string MensajeClaveFormato = "La clave es de formato alfanumérico";
        private readonly string MensajeNombreFormato = "El nombre es de formato alfanumérico";

        private static readonly int LongitudClave = 20;
        private static readonly int LongitudNombre = 30;

        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son { LongitudClave } caracteres";
        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre de la giroComercial son { LongitudNombre } caracteres";

        private readonly string MensajeExistencia = "El giro comercial no existe";
        private readonly string MensajeDuplicadoClave = "La clave del giro comercial ya existe";
        private readonly string MensajeDuplicadoNombre = "El nombre del giro comercial ya existe";

        private readonly string MensajeDependenciaCompania = "El giro comercial está asociado a al menos una compañía y no se puede eliminar";
        private readonly string MensajeDependenciaMercado = "El giro comercial está asociado a al menos un mercado y no se puede eliminar";

        public void ValidarAgregar(GiroComercial giroComercial)
        {
            ValidarRequerido(giroComercial);
            ValidarFormato(giroComercial);
            ValidarRango(giroComercial);
            ValidarDuplicado(giroComercial);
        }

        public void ValidarEditar(GiroComercial giroComercial)
        {
            ValidarRequerido(giroComercial);
            ValidarFormato(giroComercial);
            ValidarRango(giroComercial);
            ValidarExistencia(giroComercial.IdGiroComercial);
            ValidarDuplicado(giroComercial);
        }

        public void ValidarEliminar(int idGiroComercial)
        {
            ValidarExistencia(idGiroComercial);
            ValidarDependencia(idGiroComercial);
        }

        public void ValidarRequerido(GiroComercial giroComercial)
        {
            Validator.ValidarRequerido(giroComercial.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(giroComercial.Clave, MensajeClaveRequerida);
        }

        public void ValidarFormato(GiroComercial giroComercial)
        {
            Validator.ValidarAlfanumerico(giroComercial.Clave, MensajeClaveFormato);
            Validator.ValidarAlfanumerico(giroComercial.Nombre, MensajeNombreFormato);
        }

        public void ValidarRango(GiroComercial giroComercial)
        {
            Validator.ValidarLongitudRangoString(giroComercial.Clave, LongitudClave, MensajeClaveLongitud);
            Validator.ValidarLongitudRangoString(giroComercial.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(int idGiroComercial)
        {
            var giroComercial = giroComercialRepository.Consultar(idGiroComercial);

            if (giroComercial == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(GiroComercial giroComercial)
        {
            GiroComercial giroComercialDuplicadoPorClave = giroComercialRepository.ConsultarPorClave(giroComercial.Clave);

            if (giroComercialDuplicadoPorClave != null
                && giroComercial.IdGiroComercial != giroComercialDuplicadoPorClave.IdGiroComercial)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }

            GiroComercial giroComercialDuplicadoPorNombre = giroComercialRepository.ConsultarPorNombre(giroComercial.Nombre);

            if (giroComercialDuplicadoPorNombre != null
                && giroComercial.IdGiroComercial != giroComercialDuplicadoPorNombre.IdGiroComercial)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }
        }

        public void ValidarDependencia(int idGiroComercial)
        {
            GiroComercial giroComercial = giroComercialRepository.ConsultarDependencias(idGiroComercial);

            if (giroComercial.Compania.Any())
            {
                throw new CdisException(MensajeDependenciaCompania);
            }

            //if (giroComercial.Mercado.Any())
            //{
            //    throw new CdisException(MensajeDependenciaMercado);
            //}
        }
    }
}
