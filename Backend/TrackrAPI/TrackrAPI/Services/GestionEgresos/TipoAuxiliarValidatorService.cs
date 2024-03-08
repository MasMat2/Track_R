using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEgresos;

namespace TrackrAPI.Services.GestionEgresos
{
    public class TipoAuxiliarValidatorService
    {
        private ITipoAuxiliarRepository tipoAuxiliarRepository;

        public TipoAuxiliarValidatorService(ITipoAuxiliarRepository tipoAuxiliarRepository)
        {
            this.tipoAuxiliarRepository = tipoAuxiliarRepository;
        }

        private readonly string MensajeDescripcionRequerido = "La descripción es requerida";
        private readonly string MensajeClaveRequerido = "La clave es requerida";
        private readonly string MensajeTipoCuentaRequerido = "El tipo de cuenta contable es requerido";
        private readonly string MensajeExistencia = "El tipo de auxiliar no existe";
        private readonly string MensajeDuplicado = "El tipo de auxiliar ya existe";

        private readonly string MensajeDependencia = "El estado esta asociado al menos a un municipio y no se puede eliminar";

        private static readonly int LongitudDescripcion = 50;

        private readonly string MensajeDescripcionLongitud = $"La longitud máxima de la descripcion son {LongitudDescripcion } caracteres";

        public void ValidarAgregar(TipoAuxiliar tipoAuxiliar)
        {
            ValidarRequerido(tipoAuxiliar);
            ValidarRango(tipoAuxiliar);
            ValidarDuplicado(tipoAuxiliar);
        }

        public void ValidarEditar(TipoAuxiliar tipoAuxiliar)
        {
            ValidarRequerido(tipoAuxiliar);
            ValidarRango(tipoAuxiliar);
            ValidarExistencia(tipoAuxiliar.IdTipoAuxiliar);
            ValidarDuplicado(tipoAuxiliar);
        }

        public void ValidarEliminar(int idTipoAuxiliar)
        {
            var tipoAuxiliar = tipoAuxiliarRepository.Consultar(idTipoAuxiliar);

            ValidarExistencia(idTipoAuxiliar);
            ValidarDependencia(tipoAuxiliar);
        }

        public void ValidarRequerido(TipoAuxiliar tipoAuxiliar)
        {
            Validator.ValidarRequerido(tipoAuxiliar.Descripcion, MensajeDescripcionRequerido);
            Validator.ValidarRequerido(tipoAuxiliar.Clave, MensajeClaveRequerido);
            Validator.ValidarRequerido(tipoAuxiliar.IdTipoCuentaContable, MensajeTipoCuentaRequerido);
        }

        public void ValidarRango(TipoAuxiliar tipoAuxiliar)
        {
            Validator.ValidarLongitudRangoString(tipoAuxiliar.Descripcion, LongitudDescripcion, MensajeDescripcionLongitud);
        }

        public void ValidarExistencia(TipoAuxiliar tipoAuxiliar)
        {
            if (tipoAuxiliar == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idTipoAuxiliar)
        {
            var tipoAuxiliar = tipoAuxiliarRepository.Consultar(idTipoAuxiliar);

            if (tipoAuxiliar == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(TipoAuxiliar tipoAuxiliar)
        {
            var tipoAuxiliarDuplicado = tipoAuxiliarRepository.Consultar(tipoAuxiliar.IdTipoAuxiliar);

            if (tipoAuxiliarDuplicado != null && tipoAuxiliar.IdTipoAuxiliar != tipoAuxiliarDuplicado.IdTipoAuxiliar)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(TipoAuxiliar tipoAuxiliar)
        {
            var tipoAuxiliarDep = tipoAuxiliarRepository.ConsultarDependencias(tipoAuxiliar.IdTipoAuxiliar);

            if (tipoAuxiliarDep.Auxiliar.Any())
            {
                throw new CdisException(MensajeDependencia);
            }
        }
    }
}
