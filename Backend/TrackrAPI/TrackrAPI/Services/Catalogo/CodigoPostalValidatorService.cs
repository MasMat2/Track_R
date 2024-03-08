using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class CodigoPostalValidatorService
    {

        private ICodigoPostalRepository codigoPostalRepository;

        public CodigoPostalValidatorService(ICodigoPostalRepository codigoPostalRepository)
        {
            this.codigoPostalRepository = codigoPostalRepository;
        }

        private readonly string MensajeCodigoPostalRequerido = "El código postal es requerido";
        private readonly string MensajeColoniaRequerida = "La colonia es requerida";
        private readonly string MensajeExistencia = "El código postal no existe";

        private readonly string MensajeDuplicadoCodigoPostal = "El código postal ya existe";
        private readonly string MensajeDuplicadoColonia = "La colonia ya existe";

        private static readonly int LongitudCodigoPostal = 5;
        private static readonly int LongitudColonia = 200;

        private readonly string MensajeCodigoPostalLongitud = $"La longitud máxima del código postal son {LongitudCodigoPostal } caracteres";
        private readonly string MensajeColoniaLongitud = $"La longitud máxima de la colonia son {LongitudColonia} caracteres";

        public void ValidarAgregar(CodigoPostal codigoPostal)
        {
            ValidarRequerido(codigoPostal);
            ValidarRango(codigoPostal);
            ValidarDuplicado(codigoPostal);
        }
        public void ValidarEditar(CodigoPostal codigoPostal)
        {
            ValidarRequerido(codigoPostal);
            ValidarRango(codigoPostal);
            ValidarExistencia(codigoPostal.IdCodigoPostal);
            ValidarDuplicado(codigoPostal);
        }

        public void ValidarEliminar(int idCodigoPostal)
        {
            var codigoPostal = codigoPostalRepository.Consultar(idCodigoPostal);

            ValidarExistencia(idCodigoPostal);
            ValidarDependencia(idCodigoPostal);
        }

        public void ValidarRequerido(CodigoPostal codigoPostal)
        {
            Validator.ValidarRequerido(codigoPostal.CodigoPostal1, MensajeCodigoPostalRequerido);
            Validator.ValidarRequerido(codigoPostal.Colonia, MensajeColoniaRequerida);
        }
        public void ValidarRango(CodigoPostal codigoPostal)
        {
            Validator.ValidarLongitudRangoString(codigoPostal.CodigoPostal1, LongitudCodigoPostal, MensajeCodigoPostalLongitud);
            Validator.ValidarLongitudRangoString(codigoPostal.Colonia, LongitudColonia, MensajeColoniaLongitud);
        }

        public void ValidarExistencia(CodigoPostalDto codigoPostal)
        {
            if (codigoPostal == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idCodigoPostal)
        {
            var codigoPostal = codigoPostalRepository.Consultar(idCodigoPostal);

            if (codigoPostal == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(CodigoPostal codigoPostal)
        {
            CodigoPostalDto parentescoDuplicadoCodigoPostal = codigoPostalRepository.ConsultarPorCodigoPostal(codigoPostal.CodigoPostal1).FirstOrDefault();
            CodigoPostal parentescoDuplicadoColonia = codigoPostalRepository.ConsultarPorColonia(codigoPostal.Colonia);

            if (parentescoDuplicadoCodigoPostal != null && codigoPostal.IdCodigoPostal != parentescoDuplicadoCodigoPostal.IdCodigoPostal)
            {
                throw new CdisException(MensajeDuplicadoCodigoPostal);
            }
            else if (parentescoDuplicadoColonia != null && codigoPostal.IdCodigoPostal != parentescoDuplicadoColonia.IdCodigoPostal)
            {
                throw new CdisException(MensajeDuplicadoColonia);
            }
        }

        public void ValidarDependencia(int IdCodigoPostal)
        {
        }
    }
}
