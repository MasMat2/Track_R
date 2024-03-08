using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class TipoCuentaContableValidatorService
    {
        private ITipoCuentaContableRepository tipoCuentaContableRepository;
        public TipoCuentaContableValidatorService(ITipoCuentaContableRepository tipoCuentaContableRepository)
        {
            this.tipoCuentaContableRepository = tipoCuentaContableRepository;
        }
        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerido = "La clave es requerida";

        private readonly string MensajeTpAuxDependencia = "El tipo de cuenta contable esta asociado al menos a un tipo de auxiliar y no se puede eliminar.";
        private readonly string MensajeCuentaDependencia = "El tipo de cuenta contable esta asociado al menos a una cuenta contable y no se puede eliminar.";
        private readonly string MensajeSubtipoDependencia = "El tipo de cuenta contable esta asociado al menos a un subtipo y no se puede eliminar.";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 3;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

        private readonly string MensajeExistencia = "El tipo de cuenta contable no existe";
        private readonly string MensajeNombreDuplicado = "El nombre del tipo de cuenta contable ya existe";
        private readonly string MensajeClaveDuplicado = "La clave del tipo de cuenta contable ya existe";

        public void ValidarAgregar(TipoCuentaContable tipoCuentaContable)
        {
            ValidarRequerido(tipoCuentaContable);
            ValidarRango(tipoCuentaContable);
            ValidarDuplicado(tipoCuentaContable);
        }

        public void ValidarEditar(TipoCuentaContable tipoCuentaContable)
        {
            ValidarRequerido(tipoCuentaContable);
            ValidarRango(tipoCuentaContable);
            ValidarExistencia(tipoCuentaContable.IdTipoCuentaContable);
            ValidarDuplicado(tipoCuentaContable);
        }

        public void ValidarEliminar(int idTipoCuentaContable)
        {
            var tipoCuentaContable = tipoCuentaContableRepository.Consultar(idTipoCuentaContable);

            ValidarExistencia(idTipoCuentaContable);
            ValidarDependencia(tipoCuentaContable);
        }

        public void ValidarRequerido(TipoCuentaContable tipoCuentaContable)
        {
            Validator.ValidarRequerido(tipoCuentaContable.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(tipoCuentaContable.Clave, MensajeClaveRequerido);
        }

        public void ValidarRango(TipoCuentaContable tipoCuentaContable)
        {
            Validator.ValidarLongitudRangoString(tipoCuentaContable.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(tipoCuentaContable.Clave, LongitudClave, MensajeClaveLongitud);
        }

        public void ValidarExistencia(int idTipoCuentaContable)
        {
            var tipoCuentaContable = tipoCuentaContableRepository.Consultar(idTipoCuentaContable);

            if (tipoCuentaContable == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(TipoCuentaContable tipoCuentaContable)
        {
            var tipoCuentaContableNombreDuplicado = tipoCuentaContableRepository.ConsultarPorNombre(tipoCuentaContable.Nombre);

            if (tipoCuentaContableNombreDuplicado != null && tipoCuentaContable.IdTipoCuentaContable != tipoCuentaContableNombreDuplicado.IdTipoCuentaContable)
            {
                throw new CdisException(MensajeNombreDuplicado);
            }

            var tipoCuentaContableClaveDuplicado = tipoCuentaContableRepository.ConsultarPorClave(tipoCuentaContable.Clave);

            if (tipoCuentaContableClaveDuplicado != null && tipoCuentaContable.IdTipoCuentaContable != tipoCuentaContableClaveDuplicado.IdTipoCuentaContable)
            {
                throw new CdisException(MensajeClaveDuplicado);
            }
        }

        public void ValidarDependencia(TipoCuentaContable tipoCuentaContable)
        {
            var tipoCuentaContableDep = tipoCuentaContableRepository.ConsultarDependencias(tipoCuentaContable.IdTipoCuentaContable);

            if (tipoCuentaContableDep.CuentaContable.Any())
            {
                throw new CdisException(MensajeCuentaDependencia);
            }

            if (tipoCuentaContableDep.TipoAuxiliar.Any())
            {
                throw new CdisException(MensajeTpAuxDependencia);
            }

            if (tipoCuentaContableDep.SubtipoCuentaContable.Any())
            {
                throw new CdisException(MensajeSubtipoDependencia);
            }
        }
    }
}
