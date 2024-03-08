using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Contabilidad;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Contabilidad
{
    public class AgrupadorCuentaContableValidatorService
    {
        private IAgrupadorCuentaContableRepository agrupadorCuentaContableRepository;
        private ICuentaContableRepository cuentaContableRepository;

        private static readonly string MensajeExistencia = "El agrupador no existe";
        private static readonly string MensajeClaveDuplicada = "Ya existe un agrupador con esa clave";

        private static readonly string MensajeClaveRequerida = "La clave es requerida";
        private static readonly string MensajeDescripcionRequerida = "La descripción es requerida";

        private static readonly string MensajeCuentaCapitalExistencia = "La cuenta de capital no existe";
        private static readonly string MensajeCuentaResultadoExistencia = "La cuenta de resultado no existe";

        private readonly string MensajeDependenciaCuentaContable = "El agrupador está siendo utilizado por al menos una cuenta contable y no se puede eliminar";
        private readonly string MensajeDependenciaCompania = "El agrupador está siendo utilizado por al menos una compañía y no se puede eliminar";

        private static readonly int LongitudMaximaClave = 20;
        private static readonly int LongitudMaximaDescripcion = 500;

        private static readonly string MensajeLongitudClave = $"La longitud máxima de la clave son { LongitudMaximaClave } caracteres";
        private static readonly string MensajeLongitudDescripcion = $"La longitud máxima de la descripción son { LongitudMaximaDescripcion } caracteres";

        private readonly string MensajeDescripcionFormato = "La descripción es de formato alfanumérico";
        private readonly string MensajeClaveFormato = "La clave es de formato alfanumérico";

        public AgrupadorCuentaContableValidatorService(
            IAgrupadorCuentaContableRepository agrupadorCuentaContableRepository,
            ICuentaContableRepository cuentaContableRepository
        )
        {
            this.agrupadorCuentaContableRepository = agrupadorCuentaContableRepository;
            this.cuentaContableRepository = cuentaContableRepository;
        }

        public void ValidarAgregar(AgrupadorCuentaContable agrupador)
        {
            ValidarRequerido(agrupador);
            ValidarRango(agrupador);
            ValidarLlavesForaneas(agrupador);
            ValidarDuplicado(agrupador);
            ValidarFormato(agrupador);
        }

        public void ValidarEditar(AgrupadorCuentaContable agrupador)
        {
            ValidarRequerido(agrupador);
            ValidarRango(agrupador);
            ValidarExistencia(agrupador.IdAgrupadorCuentaContable);
            ValidarLlavesForaneas(agrupador);
            ValidarDuplicado(agrupador);
            ValidarFormato(agrupador);
        }

        public void ValidarEliminar(int idAgrupador)
        {
            ValidarExistencia(idAgrupador);
            ValidarDependencia(idAgrupador);
        }

        private void ValidarRequerido(AgrupadorCuentaContable agrupador)
        {
            Validator.ValidarRequerido(agrupador.Clave, MensajeClaveRequerida);
            Validator.ValidarRequerido(agrupador.Descripcion, MensajeDescripcionRequerida);
        }

        private void ValidarRango(AgrupadorCuentaContable agrupador)
        {
            Validator.ValidarLongitudMaximaString(agrupador.Clave, LongitudMaximaClave, MensajeLongitudClave);
            Validator.ValidarLongitudMaximaString(agrupador.Descripcion, LongitudMaximaDescripcion, MensajeLongitudDescripcion);
        }
        private void ValidarFormato(AgrupadorCuentaContable agrupador)
        {
            Validator.ValidarAlfanumerico(agrupador.Clave, MensajeClaveFormato);
            Validator.ValidarAlfanumerico(agrupador.Descripcion, MensajeDescripcionFormato);
        }

        private void ValidarLlavesForaneas(AgrupadorCuentaContable agrupador)
        {
            CuentaContable cuentaCapital;
            CuentaContable cuentaResultado;

            if (agrupador.IdCuentaContableCapital != null)
            {
                cuentaCapital = cuentaContableRepository.Consultar((int)agrupador.IdCuentaContableCapital);

                if (cuentaCapital == null)
                    throw new CdisException(MensajeCuentaCapitalExistencia);
            }

            if (agrupador.IdCuentaContableResultado != null)
            {
                cuentaResultado = cuentaContableRepository.Consultar((int)agrupador.IdCuentaContableResultado);

                if (cuentaResultado == null)
                    throw new CdisException(MensajeCuentaResultadoExistencia);
            }
        }

        private void ValidarExistencia(int idAgrupador)
        {
            AgrupadorCuentaContable agrupador = agrupadorCuentaContableRepository.Consultar(idAgrupador);

            if (agrupador == null)
                throw new CdisException(MensajeExistencia);
        }

        private void ValidarDuplicado(AgrupadorCuentaContable agrupador)
        {
            List<AgrupadorCuentaContableDto> agrupadores = agrupadorCuentaContableRepository.ConsultarParaGrid().ToList();

            bool hayDuplicaods = agrupadores.Any(d =>
                agrupador.IdAgrupadorCuentaContable != d.IdAgrupadorCuentaContable &&
                agrupador.Clave == d.Clave
            );

            if (hayDuplicaods)
                throw new CdisException(MensajeClaveDuplicada);
        }

        private void ValidarDependencia(int idAgrupador)
        {
            AgrupadorCuentaContable agrupador = agrupadorCuentaContableRepository.ConsultarDependencias(idAgrupador);

            if (agrupador.CuentaContable.Any())
                throw new CdisException(MensajeDependenciaCuentaContable);

            if (agrupador.Compania.Any())
                throw new CdisException(MensajeDependenciaCompania);
        }
    }
}
