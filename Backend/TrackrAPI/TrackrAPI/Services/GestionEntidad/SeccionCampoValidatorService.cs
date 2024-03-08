using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;

namespace TrackrAPI.Services.GestionEntidad
{
    public class SeccionCampoValidatorService
    {
        private readonly ISeccionCampoRepository seccionCampoRepository;

        public SeccionCampoValidatorService(ISeccionCampoRepository seccionCampoRepository)
        {
            this.seccionCampoRepository = seccionCampoRepository;
        }

        private readonly string MensajeDescripcionRequerido = "La descripción es requerida";
        private readonly string MensajeExistencia = "El campo no existe";
        private readonly string MensajeDuplicado = "El campo ya existe";
        private readonly string MensajeDependecia = "No se puede eliminar, ya que existen valores relacionados con este campo";
        private readonly string MensajeDescripcionLongitud = $"La longitud máxima de la descripción son {LongitudDescripcion } caracteres";
        private static readonly int LongitudDescripcion = 200;

        public void ValidarAgregar(SeccionCampo seccionCampo)
        {
            ValidarRequerido(seccionCampo);
            ValidarRango(seccionCampo);
            ValidarDuplicado(seccionCampo);
        }
        public void ValidarEditar(SeccionCampo seccionCampo)
        {
            ValidarRequerido(seccionCampo);
            ValidarRango(seccionCampo);
            ValidarExistencia(seccionCampo.IdSeccionCampo);
            ValidarDuplicado(seccionCampo);
        }

        public void ValidarEliminar(int idSeccionCampo)
        {
            ValidarExistencia(idSeccionCampo);
        }

        public void ValidarRequerido(SeccionCampo seccionCampo)
        {
            Validator.ValidarRequerido(seccionCampo.Descripcion, MensajeDescripcionRequerido);
        }

        public void ValidarRango(SeccionCampo seccionCampo)
        {
            Validator.ValidarLongitudRangoString(seccionCampo.Descripcion, LongitudDescripcion, MensajeDescripcionLongitud);
        }

        public void ValidarExistencia(SeccionCampo seccionCampo)
        {
            if (seccionCampo == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idSeccionCampo)
        {
            var seccionCampo = seccionCampoRepository.Consultar(idSeccionCampo);

            if (seccionCampo == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(SeccionCampo seccionCampo)
        {
            var seccionCampoDuplicado = seccionCampoRepository.ConsultarDuplicado(
                seccionCampo.Orden,
                seccionCampo.Grupo,
                seccionCampo.Clave,
                seccionCampo.IdSeccion);

            if (seccionCampoDuplicado != null && seccionCampoDuplicado.IdSeccionCampo != seccionCampo.IdSeccionCampo)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }
    }
}
