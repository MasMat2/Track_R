using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class TipoConceptoValidatorService
    {
        private ITipoConceptoRepository tipoConceptoRepository;

        public TipoConceptoValidatorService(ITipoConceptoRepository tipoConceptoRepository)
        {
            this.tipoConceptoRepository = tipoConceptoRepository;
        }

        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeNombreRequerido = "El nombre es requerido";

        private readonly string MensajeClaveFormato = "La clave es de formato alfanumérico";
        private readonly string MensajeNombreFormato = "El nombre es de formato alfanumérico";

        private static readonly int LongitudClave = 3;
        private static readonly int LongitudNombre = 50;

        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son { LongitudClave } caracteres";
        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son { LongitudNombre } caracteres";

        private readonly string MensajeExistencia = "El tipo de concepto no existe";
        private readonly string MensajeDuplicadoClave = "La clave del tipo de concepto ya existe";
        private readonly string MensajeDuplicadoNombre = "El nombre del tipo de concepto ya existe";

        private readonly string MensajeDependenciaConcepto = "La tipo de concepto está asociado al menos a un concepto y no se puede eliminar";

        public void ValidarAgregar(TipoConcepto tipoConcepto)
        {
            ValidarRequerido(tipoConcepto);
            ValidarFormato(tipoConcepto);
            ValidarRango(tipoConcepto);
            ValidarDuplicado(tipoConcepto);
        }

        public void ValidarEditar(TipoConcepto tipoConcepto)
        {
            ValidarRequerido(tipoConcepto);
            ValidarFormato(tipoConcepto);
            ValidarRango(tipoConcepto);
            ValidarExistencia(tipoConcepto.IdTipoConcepto);
            ValidarDuplicado(tipoConcepto);
        }

        public void ValidarEliminar(int idTipoConcepto)
        {
            ValidarExistencia(idTipoConcepto);
            ValidarDependencia(idTipoConcepto);
        }

        public void ValidarRequerido(TipoConcepto tipoConcepto)
        {
            Validator.ValidarRequerido(tipoConcepto.Clave, MensajeClaveRequerida);
            Validator.ValidarRequerido(tipoConcepto.Nombre, MensajeNombreRequerido);
        }

        public void ValidarFormato(TipoConcepto tipoConcepto)
        {
            Validator.ValidarAlfanumerico(tipoConcepto.Clave, MensajeClaveFormato);
            Validator.ValidarAlfanumerico(tipoConcepto.Nombre, MensajeNombreFormato);
        }

        public void ValidarRango(TipoConcepto tipoConcepto)
        {
            Validator.ValidarLongitudRangoString(tipoConcepto.Clave, LongitudClave, MensajeClaveLongitud);
            Validator.ValidarLongitudRangoString(tipoConcepto.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(int idTipoConcepto)
        {
            var tipoConcepto = tipoConceptoRepository.Consultar(idTipoConcepto);

            if (tipoConcepto == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(TipoConcepto tipoConcepto)
        {
            TipoConcepto tipoConceptoDuplicadoPorClave = tipoConceptoRepository.ConsultarPorClave(tipoConcepto.Clave);
            if (tipoConceptoDuplicadoPorClave != null
                && tipoConcepto.IdTipoConcepto != tipoConceptoDuplicadoPorClave.IdTipoConcepto)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }

            TipoConcepto tipoConceptoDuplicadoPorNombre = tipoConceptoRepository.ConsultarPorNombre(tipoConcepto.Nombre);
            if (tipoConceptoDuplicadoPorNombre != null
                && tipoConcepto.IdTipoConcepto != tipoConceptoDuplicadoPorNombre.IdTipoConcepto)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }
        }

        public void ValidarDependencia(int idTipoConcepto)
        {
            TipoConcepto tipoConcepto = tipoConceptoRepository.ConsultarDependencias(idTipoConcepto);

            if (tipoConcepto.ConfiguracionConcepto.Any()) {
                throw new CdisException(MensajeDependenciaConcepto);
            }
        }
    }
}
