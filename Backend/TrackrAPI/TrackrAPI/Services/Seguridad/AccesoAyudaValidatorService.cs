using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class AccesoAyudaValidatorService
    {
        private IAccesoAyudaRepository accesoAyudaRepository;

        public AccesoAyudaValidatorService(IAccesoAyudaRepository accesoAyudaRepository)
        {
            this.accesoAyudaRepository = accesoAyudaRepository;
        }

        private static readonly string MensajeExistencia = "El AccesoAyuda no existe.";
        private static readonly string MensajeNombreDuplicado = "El nombre de archivo ya existe.";
        private static readonly string MensajeNombreOrden = "El orden ya existe.";
        private static readonly string MensajeDependencia = "El AccesoAyuda esta siendo utilizado por al menos un Perfil y no se puede eliminar.";
        private static readonly string MensajeDescripcionRequerido = "La descripción de la ayuda es requerida.";


        private static int LongitudDescripcion = 500;
        private static int LongitudEtiqueta = 100;
        private static int LongitudNombreImagen = 500;
        private static int LongitudTipoMime = 200;

        private static int LongitudMinimaOrden = 1;
        private static int LongitudMaximaOrden = 999;


        private static string MensajeDescripcionLongitud = "La longitud máxima de la descripción son " + LongitudDescripcion + " caracteres.";
        private static string MensajeEtiquetaLongitud = "La longitud máxima de la etiqueta son " + LongitudEtiqueta + " caracteres.";
        private static string MensajeImagenLongitud = "La longitud máxima de la imagen son " + LongitudNombreImagen + " caracteres.";
        private static string MensajeOrdenLongitud = "Orden Menú debe tener un valor entre " + LongitudMinimaOrden + " y " + LongitudMaximaOrden;

        public void ValidarAgregar(AccesoAyuda accesoAyuda)
        {
            ValidarRequerido(accesoAyuda);
            ValidarRango(accesoAyuda);
            ValidarDuplicado(accesoAyuda);
        }

        public void ValidarEditar(AccesoAyuda accesoAyuda)
        {
            ValidarRequerido(accesoAyuda);
            ValidarRango(accesoAyuda);
            ValidarExistencia(accesoAyuda.IdAccesoAyuda);
            ValidarDuplicado(accesoAyuda);
        }

        public void ValidarEliminar(int idAcceso)
        {
            ValidarExistencia(idAcceso);
        }

        public void ValidarRequerido(AccesoAyuda accesoAyuda)
        {
            Validator.ValidarRequerido(accesoAyuda.DescripcionAyuda, MensajeDescripcionRequerido);
        }

        public void ValidarRango(AccesoAyuda accesoAyuda)
        {
            Validator.ValidarLongitudMaximaString(accesoAyuda.DescripcionAyuda, LongitudDescripcion, MensajeDescripcionLongitud);
            Validator.ValidarLongitudMaximaString(accesoAyuda.EtiquetaCampo, LongitudEtiqueta, MensajeEtiquetaLongitud);
            Validator.ValidarLongitudMaximaString(accesoAyuda.NombreArchivo, LongitudNombreImagen, MensajeImagenLongitud);
            Validator.ValidarRangoEntero(accesoAyuda.Orden, LongitudMinimaOrden, LongitudMaximaOrden, MensajeOrdenLongitud);
        }

        public void ValidarExistencia(int idAccesoAyuda)
        {
            AccesoAyuda accesoAyuda = accesoAyudaRepository.Consultar(idAccesoAyuda);
            if (accesoAyuda == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(AccesoAyuda accesoAyuda)
        {
            AccesoAyuda accesoAyudaConsultadoPorNombre = accesoAyudaRepository.ConsultarPorNombre(accesoAyuda.NombreArchivo);
            AccesoAyuda accesoAyudaConsultadoPorOrden = accesoAyudaRepository.ConsultarPorOrden(accesoAyuda.Orden, accesoAyuda.IdAcceso);

            if (accesoAyudaConsultadoPorNombre != null && accesoAyudaConsultadoPorNombre.IdAccesoAyuda != accesoAyuda.IdAccesoAyuda && accesoAyudaConsultadoPorNombre.NombreArchivo != null)
            {
                throw new CdisException(MensajeNombreDuplicado);
            }
            if (accesoAyudaConsultadoPorOrden != null && accesoAyudaConsultadoPorOrden.IdAccesoAyuda != accesoAyuda.IdAccesoAyuda && accesoAyudaConsultadoPorOrden.IdAyudaSeccion != accesoAyuda.IdAyudaSeccion)
            {
                throw new CdisException(MensajeNombreOrden);
            }
        }
    }
}
