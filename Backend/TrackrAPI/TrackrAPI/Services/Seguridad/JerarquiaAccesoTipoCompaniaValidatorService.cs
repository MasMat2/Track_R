using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class JerarquiaAccesoTipoCompaniaValidatorService
    {
        private readonly IJerarquiaAccesoTipoCompaniaRepository jerarquiaAccesoTipoCompaniaRepository;

        private readonly string MensajeJerarquiaAccesoRequerida = "La jerarquía de acceso es requerida";
        private readonly string MensajeTipoCompaniaRequerido = "El tipo de compañía es requerida";
        private readonly string MensajeExistencia = "La configuración jerarquía acceso - tipo compañía no existe";

        public JerarquiaAccesoTipoCompaniaValidatorService(IJerarquiaAccesoTipoCompaniaRepository jerarquiaAccesoTipoCompaniaRepository)
        {
            this.jerarquiaAccesoTipoCompaniaRepository = jerarquiaAccesoTipoCompaniaRepository;
        }

        public void ValidarAgregar(JerarquiaAccesoTipoCompania jerarquiaAccesoTipoCompania)
        {
            ValidarRequerido(jerarquiaAccesoTipoCompania);
        }

        public void ValidarEditar(JerarquiaAccesoTipoCompania jerarquiaAccesoTipoCompania)
        {
            ValidarRequerido(jerarquiaAccesoTipoCompania);
        }

        public void ValidarEliminar(int idJerarquiaAccesoTipoCompania)
        {
            ValidarExistencia(idJerarquiaAccesoTipoCompania);
        }

        private void ValidarRequerido(JerarquiaAccesoTipoCompania jerarquiaAccesoTipoCompania)
        {
            Validator.ValidarRequerido(jerarquiaAccesoTipoCompania.IdJerarquiaAcceso, MensajeJerarquiaAccesoRequerida);
            Validator.ValidarRequerido(jerarquiaAccesoTipoCompania.IdTipoCompania, MensajeTipoCompaniaRequerido);
        }

        private void ValidarExistencia(int idJerarquiaAccesoTipoCompania)
        {
            JerarquiaAccesoTipoCompania jerarquiaAccesoTipoCompania = jerarquiaAccesoTipoCompaniaRepository.Consultar(idJerarquiaAccesoTipoCompania);

            if (jerarquiaAccesoTipoCompania == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }
    }
}
