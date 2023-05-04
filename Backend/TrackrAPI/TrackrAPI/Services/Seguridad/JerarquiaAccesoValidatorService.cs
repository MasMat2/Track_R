using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Linq;

namespace TrackrAPI.Services.Seguridad
{
    public class JerarquiaAccesoValidatorService
    {
        private readonly IJerarquiaAccesoRepository jerarquiaAccesoRepository;

        private const int LongitudMaximaNombre = 200;

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeCompaniaRequerida = "La compañía es requerida";
        private readonly string MensajeNombreLongitudMaxima = "La longitud máxima del nombre es de: " + LongitudMaximaNombre;
        private readonly string MensajeExistencia = "La jerarquía de acceso no existe";

        private readonly string MensajeDependenciaJerarquiaEstructura = "La jerarquía esta asociada al menos a una estructura y no puede ser eliminada";
        private readonly string MensajeDependenciaTipoCompania = "La jerarquía esta asociada al menos a una configuración de tipo de compañía y no puede ser eliminada";
        private readonly string MensajeDependenciaPerfil = "La jerarquía esta asociada al menos a un perfil y no puede ser eliminada";

        public JerarquiaAccesoValidatorService(IJerarquiaAccesoRepository jerarquiaAccesoRepository)
        {
            this.jerarquiaAccesoRepository = jerarquiaAccesoRepository;
        }

        public void ValidarAgregar(JerarquiaAcceso jerarquiaAcceso)
        {
            ValidarRequerido(jerarquiaAcceso);
            ValidarLongitud(jerarquiaAcceso);
        }

        public void ValidarEditar(JerarquiaAcceso jerarquiaAcceso)
        {
            ValidarRequerido(jerarquiaAcceso);
            ValidarLongitud(jerarquiaAcceso);
        }

        public void ValidarEliminar(int idJerarquiaAcceso)
        {
            ValidarExistencia(idJerarquiaAcceso);
            ValidarDependencia(idJerarquiaAcceso);
        }

        private void ValidarRequerido(JerarquiaAcceso jerarquiaAcceso)
        {
            Validator.ValidarRequerido(jerarquiaAcceso.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(jerarquiaAcceso.IdCompania, MensajeCompaniaRequerida);
        }

        private void ValidarLongitud(JerarquiaAcceso jerarquiaAcceso)
        {
            Validator.ValidarLongitudMaximaString(jerarquiaAcceso.Nombre, LongitudMaximaNombre, MensajeNombreLongitudMaxima);
        }

        private void ValidarExistencia(int idJerarquiaAcceso)
        {
            JerarquiaAcceso jerarquiaAcceso = jerarquiaAccesoRepository.Consultar(idJerarquiaAcceso);

            if (jerarquiaAcceso == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        private void ValidarDependencia(int idJerarquiaAcceso)
        {
            JerarquiaAcceso jerarquiaAcceso = jerarquiaAccesoRepository.ConsultarDependencias(idJerarquiaAcceso);

            if (jerarquiaAcceso.JerarquiaAccesoEstructura.Any())
            {
                throw new CdisException(MensajeDependenciaJerarquiaEstructura);
            }
            if (jerarquiaAcceso.JerarquiaAccesoTipoCompania.Any())
            {
                throw new CdisException(MensajeDependenciaTipoCompania);
            }
            if (jerarquiaAcceso.Perfil.Any())
            {
                throw new CdisException(MensajeDependenciaPerfil);
            }
        }
    }
}
