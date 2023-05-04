using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class TipoCompaniaValidatorService
    {
        private ITipoCompaniaRepository tipoCompaniaRepository;

        public TipoCompaniaValidatorService(ITipoCompaniaRepository tipoCompaniaRepository)
        {
            this.tipoCompaniaRepository = tipoCompaniaRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeExistencia = "El tipo de compañía no existe";

        private readonly string MensajeDuplicadoNombre = "El nombre del tipo de compañía ya existe";
        private readonly string MensajeDuplicadoClave = "La clave del tipo de compañía ya existe";

        private static readonly int LongitudNombre = 500;
        private static readonly int LongitudClave = 20;

        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son { LongitudClave } caracteres";
        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son { LongitudNombre } caracteres";

        private readonly string MensajeDependenciaPerfil = "El tipo de compañía esta asociado al menos a un perfil y no se puede eliminar";
        private readonly string MensajeDependenciaCompania = "El tipo de compañía esta asociado al menos a una compañía y no se puede eliminar";

        public void ValidarAgregar(TipoCompania tipoCompania)
        {
            ValidarRequerido(tipoCompania);
            ValidarRango(tipoCompania);
            ValidarDuplicado(tipoCompania);
        }

        public void ValidarEditar(TipoCompania tipoCompania)
        {
            ValidarRequerido(tipoCompania);
            ValidarRango(tipoCompania);
            ValidarExistencia(tipoCompania.IdTipoCompania);
            ValidarDuplicado(tipoCompania);
        }

        public void ValidarEliminar(int idTipoCompania)
        {
            ValidarExistencia(idTipoCompania);
        }

        public void ValidarRequerido(TipoCompania tipoCompania)
        {
            Validator.ValidarRequerido(tipoCompania.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(tipoCompania.Clave, MensajeClaveRequerida);
        }

        public void ValidarRango(TipoCompania tipoCompania)
        {
            Validator.ValidarLongitudRangoString(tipoCompania.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(tipoCompania.Clave, LongitudClave, MensajeClaveLongitud);
        }

        public void ValidarExistencia(TipoCompania tipoCompania)
        {
            if (tipoCompania == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idTipoCompania)
        {
            var tipoCompania = tipoCompaniaRepository.Consultar(idTipoCompania);

            if (tipoCompania == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(TipoCompania tipoCompania)
        {
            TipoCompania tipoCompaniaDuplicadoPorNombre = tipoCompaniaRepository.ConsultarPorNombre(tipoCompania.Nombre);
            TipoCompania tipoCompaniaDuplicadoPorClave = tipoCompaniaRepository.ConsultarPorClave(tipoCompania.Clave);
            if (tipoCompaniaDuplicadoPorNombre != null && tipoCompania.IdTipoCompania != tipoCompaniaDuplicadoPorNombre.IdTipoCompania)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }

            if (tipoCompaniaDuplicadoPorClave != null && tipoCompania.IdTipoCompania != tipoCompaniaDuplicadoPorClave.IdTipoCompania)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }
        }
        public void ValidarDependencia(TipoCompania tipoCompania)
        {
            var tipoCompaniaDependencia = tipoCompaniaRepository.ConsultarDependencias(tipoCompania.IdTipoCompania);

            if(tipoCompaniaDependencia.Perfil.Any())
            {
                throw new CdisException(MensajeDependenciaPerfil);
            }
            if (tipoCompaniaDependencia.Compania.Any())
            {
                throw new CdisException(MensajeDependenciaCompania);
            }
        }
    }
}
