using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.PedidoEnLinea;

namespace TrackrAPI.Services
{
    public class FlujoValidatorService
    {
        private IFlujoRepository flujoRepository;
        private ICompaniaRepository companiaRepository;

        private static readonly string MensajeExistencia = "El flujo no existe";
        private static readonly string MensajeClaveDuplicada = "Ya existe un flujo con esa clave";

        private static readonly string MensajeClaveRequerida = "La clave es requerida";
        private static readonly string MensajeNombreRequerido = "El nombre es requerido";
        private static readonly string MensajeTipoFlujoRequerido = "El tipo de flujo es requerido";

        private static readonly string MensajeCompaniaExistencia = "La compañía no existe";

        private static readonly int LongitudMaximaClave = 20;
        private static readonly int LongitudMaximaNombre = 100;

        private static readonly string MensajeLongitudClave = $"La longitud máxima de la clave son {LongitudMaximaClave}";
        private static readonly string MensajeLongitudNombre = $"La longitud máxima del nombre son {LongitudMaximaNombre}";

        public FlujoValidatorService(
            IFlujoRepository flujoRepository,
            ICompaniaRepository companiaRepository
        )
        {
            this.flujoRepository = flujoRepository;
            this.companiaRepository = companiaRepository;
        }

        public void ValidarAgregar(Flujo flujo)
        {
            ValidarRequerido(flujo);
            ValidarRango(flujo);
            ValidarLlavesForaneas(flujo);
            ValidarClave(flujo);
        }

        public void ValidarEditar(Flujo flujo)
        {
            ValidarRequerido(flujo);
            ValidarRango(flujo);
            ValidarExistencia(flujo.IdFlujo);
            ValidarLlavesForaneas(flujo);
            ValidarClave(flujo);
        }

        public void ValidarEliminar(int idFlujo)
        {
            Flujo flujo = flujoRepository.Consultar(idFlujo);

            ValidarExistencia(idFlujo);
            ValidarDependencias(idFlujo);
            ValidarFlujoEstandar(flujo, true);
        }

        private void ValidarRequerido(Flujo flujo)
        {
            Validator.ValidarRequerido(flujo.Clave, MensajeClaveRequerida);
            Validator.ValidarRequerido(flujo.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(flujo.IdTipoFlujo, MensajeTipoFlujoRequerido);
        }

        private void ValidarRango(Flujo flujo)
        {
            Validator.ValidarLongitudRangoString(flujo.Clave, 1, LongitudMaximaClave, MensajeLongitudClave);
            Validator.ValidarLongitudRangoString(flujo.Nombre, 1, LongitudMaximaClave, MensajeLongitudNombre);
        }

        private void ValidarLlavesForaneas(Flujo flujo)
        {
            Compania compania = companiaRepository.Consultar((int)flujo.IdCompania);

            if (compania == null)
                throw new CdisException(MensajeCompaniaExistencia);
        }

        private void ValidarExistencia(int idFlujo)
        {
            Flujo flujo = flujoRepository.Consultar(idFlujo);

            if (flujo == null)
                throw new CdisException(MensajeExistencia);
        }

        private void ValidarClave(Flujo flujo)
        {
            Flujo flujoClave = flujoRepository.ConsultarPorClave((int)flujo.IdCompania, flujo.Clave);

            if (flujoClave != null && flujo.IdFlujo != flujoClave.IdFlujo && flujo.Clave == flujoClave.Clave)
                throw new CdisException(MensajeClaveDuplicada);
        }

        private void ValidarDependencias(int idFlujo)
        {
            Flujo flujo = flujoRepository.ConsultarDependencias(idFlujo);

            if (flujo.Presentacion.Any())
            {
                throw new CdisException($"El flujo con clave {flujo.Clave} está siendo utilizado por al menos una presentación y no se puede eliminar");
            }

            if (flujo.ConfiguracionOpcionVenta.Any())
            {
                throw new CdisException($"El flujo con clave {flujo.Clave} está siendo utilizado por al menos una configuración de opción de venta y no se puede eliminar");
            }
        }

        /* Valida la existencia de un flujo estándar en la compañia
           Su valor de retorno indica si el flujo estándar actual, será remplazado por uno nuevo.
        */
        public Flujo ValidarFlujoEstandar(Flujo flujo, bool esEliminar = false)
        {
            Flujo flujoEstandar = flujoRepository.ConsultarDefault((int)flujo.IdCompania);

            // Se intenta eliminar el flujo estándar actual
            if (esEliminar && flujo.IdFlujo == flujoEstandar.IdFlujo)
            {
                throw new CdisException("Es necesario contar con un flujo estándar en la compañía");
            }

            // Se encontró un flujo estándar existente
            if (flujoEstandar != null)
            {
                // Se intenta editar el flujo estándar
                if (flujo.IdFlujo == flujoEstandar.IdFlujo)
                {
                    if (flujo.EsDefault == false)
                    {
                        throw new CdisException("Es necesario contar con un flujo estándar en la compañía");
                    }
                }
                // Se intenta configurar un flujo estandár distinto al actual
                else if (flujo.EsDefault == true)
                {
                    return flujoEstandar;
                }
            }
            // No se encuentra un flujo estándar existente y el flujo actual no esta configurado para ser estándar.
            else if (flujo.EsDefault == false)
            {
                throw new CdisException("Es necesario configurar el flujo estándar de la compañía");
            }

            return null;
        }
    }
}
