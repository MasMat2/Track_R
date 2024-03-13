using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedienteTrackrValidatorService
    {
        private IExpedienteTrackrRepository expedienteTrackrRepository;

        public ExpedienteTrackrValidatorService(IExpedienteTrackrRepository expedienteTrackrRepository)
        {
            this.expedienteTrackrRepository = expedienteTrackrRepository;
        }

        private readonly string MensajeExistencia = "El expediente no existe";

        private readonly string MensajeNumeroRequerido = "El número de expediente es requerido";
        private readonly string MensajeUsuarioRequerido = "El usuario es requerido";
        private readonly string MensajeFechaNacimientoRequerida = "La fecha de nacimiento es requerida";
        private readonly string MensajePesoRequerido = "El peso es requerido";
        private readonly string MensajeEstaturaRequerida = "La estatura es requerida";
        private readonly string MensajeGeneroRequerido = "El género es requerido";
        private readonly string MensajeCinturaRequerida = "La cintura es requerida";


        private readonly string MensajeNumeroLongitud = $"La longitud máxima del número de expediente son {LongitudNumero} caracteres";
        private readonly string MensajeNumeroDuplicado = "El número de expediente ya existe";

        private static readonly int LongitudNumero = 10;

        public void ValidarAgregar(ExpedienteTrackr expedienteTrackr)
        {
            ValidarRequerido(expedienteTrackr);
            ValidarRango(expedienteTrackr);
            ValidarDuplicado(expedienteTrackr);
        }

        public void ValidarEditar(ExpedienteTrackr expedienteTrackr)
        {

        }

        public void ValidarEliminar(int idExpedienteTrackr)
        {

        }

        public void ValidarRequerido(ExpedienteTrackr expedienteTrackr)
        {
            Validator.ValidarRequerido(expedienteTrackr.Numero, MensajeNumeroRequerido);
            Validator.ValidarRequerido(expedienteTrackr.IdUsuario, MensajeUsuarioRequerido);
            Validator.ValidarRequerido(expedienteTrackr.FechaNacimiento, MensajeFechaNacimientoRequerida);
            Validator.ValidarRequerido(expedienteTrackr.Peso, MensajePesoRequerido);
            Validator.ValidarRequerido(expedienteTrackr.Estatura, MensajeEstaturaRequerida);
            Validator.ValidarRequerido(expedienteTrackr.IdGenero, MensajeGeneroRequerido);
            Validator.ValidarRequerido(expedienteTrackr.Cintura, MensajeCinturaRequerida);
        }

        public void ValidarRango(ExpedienteTrackr expedienteTrackr)
        {
            Validator.ValidarLongitudRangoString(expedienteTrackr.Numero, LongitudNumero, MensajeNumeroLongitud);
        }

        public void ValidarExistencia(int idExpediente)
        {
            var expedienteTrackr = expedienteTrackrRepository.Consultar(idExpediente);
            if (expedienteTrackr == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(ExpedienteTrackr expedienteTrackr)
        {
            var expedienteDuplicadoNumero = expedienteTrackrRepository.ConsultarPorNumero(expedienteTrackr.Numero);

            if (expedienteDuplicadoNumero != null)
            {
                throw new CdisException(MensajeNumeroDuplicado);
            }
        }
    }
}
