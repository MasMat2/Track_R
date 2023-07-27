using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class AccesoValidatorService
    {
        private IAccesoRepository accesoRepository;

        public AccesoValidatorService(IAccesoRepository accesoRepository)
        {
            this.accesoRepository = accesoRepository;
        }

        private static readonly string MensajeExistencia = "El Acceso no existe.";
        private static readonly string MensajeClaveDuplicada = "La clave ya existe.";
        private static readonly string MensajeDependencia = "El Acceso esta siendo utilizado por al menos un Perfil y no se puede eliminar.";
        private static readonly string MensajeDependenciaAccesoAyuda = "El Acceso esta siendo utilizado por al menos un Acceso Ayuda y no se puede eliminar.";
        private static readonly string MensajeNombreRequerido = "El nombre es requerido.";
        private static readonly string MensajeClaveRequerida = "La clave es requerida.";
        private static readonly string MensajeTipoAccesoRequerido = "El Tipo de Acceso es requerido.";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 10;
        private static readonly int LongitudMaximaOrden = 999;
        private static readonly int LongitudMinimaOrden = 1;
        private static readonly int LongitudMaximaDescripcion = 800;

        private static readonly string MensajeNombreLongitud = "La longitud máxima del nombre son " + LongitudNombre + " caracteres.";
        private static readonly string MensajeClaveLongitud = "La longitud máxima de la clave son " + LongitudClave + " caracteres.";
        private static readonly string MensajeDescripcionLongitud = "La longitud máxima de la descripción son " + LongitudClave + " caracteres.";
        private static readonly string MensajeOrdenLongitud = "Orden Menú debe tener un valor entre " + LongitudMinimaOrden + " y " + LongitudMaximaOrden;

        public void ValidarAgregar(Acceso acceso)
        {
            ValidarRequerido(acceso);
            ValidarRango(acceso);
            ValidarDuplicado(acceso);
        }

        public void ValidarEditar(Acceso acceso)
        {
            ValidarRequerido(acceso);
            ValidarRango(acceso);
            ValidarExistencia(acceso.IdAcceso);
            ValidarDuplicado(acceso);
        }

        public void ValidarEliminar(int idAcceso)
        {
            Acceso acceso = accesoRepository.Consultar(idAcceso);
            ValidarExistencia(idAcceso);
            ValidarDependencia(acceso);
        }

        public void ValidarRequerido(Acceso acceso)
        {
            Validator.ValidarRequerido(acceso.Clave, MensajeNombreRequerido);
            Validator.ValidarRequerido(acceso.Nombre, MensajeClaveRequerida);
            Validator.ValidarRequerido(acceso.IdTipoAcceso, MensajeTipoAccesoRequerido);
        }

        public void ValidarRango(Acceso acceso)
        {
            Validator.ValidarLongitudMaximaString(acceso.Clave, LongitudClave, MensajeClaveLongitud);
            Validator.ValidarLongitudMaximaString(acceso.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudMaximaString(acceso.Descripcion, LongitudMaximaDescripcion, MensajeDescripcionLongitud);
            Validator.ValidarRangoEntero(acceso.OrdenMenu, LongitudMinimaOrden, LongitudMaximaOrden, MensajeOrdenLongitud);
        }

        public void ValidarExistencia(int idAcceso)
        {
            Acceso acceso = accesoRepository.Consultar(idAcceso);
            if (acceso == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Acceso acceso)
        {
            Acceso accesoConsultado = accesoRepository.ConsultarPorClave(acceso.Clave);

            if (accesoConsultado != null && accesoConsultado.IdAcceso != acceso.IdAcceso)
            {
                throw new CdisException(MensajeClaveDuplicada);
            }
        }

        public void ValidarDependencia(Acceso acceso)
        {
            Acceso accesoConsultado = accesoRepository.ConsultarDependencia(acceso.IdAcceso);

            if (accesoConsultado.AccesoPerfil.Count > 0)
            {
                throw new CdisException(MensajeDependencia);
            }
            if (accesoConsultado.AccesoAyuda.Count > 0)
            {
                throw new CdisException(MensajeDependenciaAccesoAyuda);
            }
        }



    }
}
