using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class CuentaContableValidatorService
    {

        private ICuentaContableRepository cuentaContableRepository;

        public CuentaContableValidatorService(ICuentaContableRepository cuentaContableRepository)
        {
            this.cuentaContableRepository = cuentaContableRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeNumeroRequerida = "El número  es requerido";
        private readonly string MensajeExistencia = "La cuenta contable no existe";

        private readonly string MensajeDuplicadoNombre = "El nombre de la cuenta ya existe";
        private readonly string MensajeDuplicadoNumero = "El número de cuenta ya existe";

        private static readonly int LongitudNombre = 100;
        private static readonly int LongitudNumero = 20;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son { LongitudNombre } caracteres";
        private readonly string MensajeNumeroLongitud = $"La longitud máxima del número son { LongitudNumero } caracteres";

        private readonly string MensajeAlmacenDependencia = "La cuenta contable está asociada al menos a un almacén y no se puede eliminar";
        private readonly string MensajeBalanceDependencia = "La cuenta contable está asociada al menos a un balance y no se puede eliminar";
        private readonly string MensajeCajaDependencia = "La cuenta contable está asociada al menos a una caja y no se puede eliminar";
        private readonly string MensajeConceptoDependencia = "La cuenta contable está asociada al menos a un concepto y no se puede eliminar";
        private readonly string MensajeImpuestoDependencia = "La cuenta contable está asociada al menos a un impuesto y no se puede eliminar";
        private readonly string MensajeImpuestoDetalleAbonoDependencia = "La cuenta contable está asociada al menos a una cuenta contable de abono y no se puede eliminar";
        private readonly string MensajeImpuestoDetalleCargoDependencia = "La cuenta contable está asociada al menos a una cuenta contable de cargo y no se puede eliminar";
        private readonly string MensajeCuentaContableAgrupadorDependencia = "La cuenta contable está siendo utilizada en un agrupador y no se puede eliminar";
        private readonly string MensajeCuentaContablePadreDependencia = "La cuenta contable es padre de al menos otra cuenta y no se puede eliminar";
        private readonly string MensajeJerarquiaEstructuraDependencia = "La cuenta contable está asociada a una jerarquía y no se puede eliminar";
        private readonly string MensajePolizaDependencia = "La cuenta contable está asociada al menos a una poliza y no se puede eliminar";
        private readonly string MensajeTipoActivoDependencia = "La cuenta contable está asociada al menos a un tipo activo y no se puede eliminar";
        private readonly string MensajeUsuarioRolDependencia = "La cuenta contable está asociada al menos a un rol de usuario y no se puede eliminar";

        private readonly string MensajeNombreFormato = "El nombre es de formato alfanumérico";
        private readonly string MensajeNumeroFormato = "El número es de formato alfanumérico";
        private readonly string MensajeDescripciónFormato = "La descripción es de formato alfanumérico";

        public void ValidarAgregar(CuentaContable cuentaContable)
        {
            ValidarRequerido(cuentaContable);
            ValidarRango(cuentaContable);
            ValidarDuplicado(cuentaContable);
        }

        public void ValidarEditar(CuentaContable cuentaContable)
        {
            ValidarRequerido(cuentaContable);
            ValidarRango(cuentaContable);
            ValidarExistencia(cuentaContable.IdCuentaContable);
            ValidarDuplicado(cuentaContable);
        }

        public void ValidarEliminar(int idCuentaContable)
        {
            ValidarExistencia(idCuentaContable);
            ValidarDependencia(idCuentaContable);
        }

        public void ValidarRequerido(CuentaContable cuentaContable)
        {
            Validator.ValidarRequerido(cuentaContable.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(cuentaContable.Numero, MensajeNumeroRequerida);
        }

        public void ValidarRango(CuentaContable cuentaContable)
        {
            Validator.ValidarLongitudRangoString(cuentaContable.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(cuentaContable.Numero, LongitudNumero, MensajeNumeroLongitud);
        }

        public void ValidarFormato(CuentaContable cuentaContable)
        {
            Validator.ValidarAlfanumerico(cuentaContable.Nombre, MensajeNombreFormato);
            Validator.ValidarAlfanumerico(cuentaContable.Numero, MensajeNumeroFormato);
        }

        public void ValidarExistencia(CuentaContableDto cuentaContable)
        {
            if (cuentaContable == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idCuentaContable)
        {
            var cuentaContable = cuentaContableRepository.Consultar(idCuentaContable);

            if (cuentaContable == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(CuentaContable cuentaContable)
        {
            CuentaContable cuentaDuplicadoNumero = cuentaContableRepository.ConsultarPorNumero((int)cuentaContable.IdCompania, cuentaContable.Numero);

            if (cuentaDuplicadoNumero != null && cuentaContable.IdCuentaContable != cuentaDuplicadoNumero.IdCuentaContable
                && cuentaContable.IdAgrupadorCuentaContable == cuentaDuplicadoNumero.IdAgrupadorCuentaContable)
            {
                throw new CdisException(MensajeDuplicadoNumero);
            }
        }

        public void ValidarDependencia(int idCuentaContable)
        {
            var cuentaContable = cuentaContableRepository.ConsultarDependencias(idCuentaContable);

            var agrupador = cuentaContable.IdAgrupadorCuentaContableNavigation;

            if (agrupador != null && (agrupador.IdCuentaContableCapital == idCuentaContable || agrupador.IdCuentaContableResultado == idCuentaContable))
            {
                throw new CdisException(MensajeCuentaContableAgrupadorDependencia);
            }

            if (cuentaContable.Almacen.Any())
            {
                throw new CdisException(MensajeAlmacenDependencia);
            }

            if (cuentaContable.BalanceCuentaContable.Any())
            {
                throw new CdisException(MensajeBalanceDependencia);
            }

            if (cuentaContable.CajaIdCuentaContableNavigation.Any())
            {
                throw new CdisException(MensajeCajaDependencia);
            }

            if (cuentaContable.CajaIdCuentaContableAutomaticaNavigation.Any())
            {
                throw new CdisException(MensajeCajaDependencia);
            }

            if (cuentaContable.Concepto.Any())
            {
                throw new CdisException(MensajeConceptoDependencia);
            }

            if (cuentaContable.Impuesto.Any())
            {
                throw new CdisException(MensajeImpuestoDependencia);
            }

            if (cuentaContable.ImpuestoDetalleIdCuentaContableAbonoNavigation.Any())
            {
                throw new CdisException(MensajeImpuestoDetalleAbonoDependencia);
            }

            if (cuentaContable.ImpuestoDetalleIdCuentaContableCargoNavigation.Any())
            {
                throw new CdisException(MensajeImpuestoDetalleCargoDependencia);
            }

            if (cuentaContable.InverseIdCuentaContablePadreNavigation.Any())
            {
                throw new CdisException(MensajeCuentaContablePadreDependencia);
            }

            if (cuentaContable.JerarquiaEstructura.Any())
            {
                throw new CdisException(MensajeJerarquiaEstructuraDependencia);
            }

            if (cuentaContable.PolizaAplicadaDetalle.Any())
            {
                throw new CdisException(MensajePolizaDependencia);
            }

            if (cuentaContable.PolizaDetalle.Any())
            {
                throw new CdisException(MensajePolizaDependencia);
            }

            if (cuentaContable.TipoActivo.Any())
            {
                throw new CdisException(MensajeTipoActivoDependencia);
            }

            if (cuentaContable.UsuarioRol.Any())
            {
                throw new CdisException(MensajeUsuarioRolDependencia);
            }
        }
    }
}
