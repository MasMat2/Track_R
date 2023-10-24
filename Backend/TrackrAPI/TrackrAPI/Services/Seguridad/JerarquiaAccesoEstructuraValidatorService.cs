using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class JerarquiaAccesoEstructuraValidatorService
    {
        private readonly IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository;
        private readonly IAccesoRepository accesoRepository;

        private readonly string MensajeExistencia = "La jerarquía de acceso estructura no existe";

        public JerarquiaAccesoEstructuraValidatorService
        (
            IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository,
            IAccesoRepository accesoRepository
        )
        {
            this.jerarquiaAccesoEstructuraRepository = jerarquiaAccesoEstructuraRepository;
            this.accesoRepository = accesoRepository;
        }

        public void ValidarAgregar(JerarquiaAccesoEstructura jerarquiaAccesoEstructura)
        {
            ValidarDuplicados(jerarquiaAccesoEstructura);

            if (jerarquiaAccesoEstructura.IdAcceso > 0)
            {
                ValidarNivelComponentes(jerarquiaAccesoEstructura);
            }
        }

        public void ValidarEliminar(int idJerarquiaAccesoEstructura)
        {
            JerarquiaAccesoEstructura jerarquiaAccesoEstructura = jerarquiaAccesoEstructuraRepository.Consultar(idJerarquiaAccesoEstructura);

            if (jerarquiaAccesoEstructura == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        private void ValidarDuplicados(JerarquiaAccesoEstructura jerarquiaAccesoEstructura)
        {
            if (jerarquiaAccesoEstructura.IdJerarquiaAccesoEstructuraPadre > 0)
            {
                JerarquiaAccesoEstructura jerarquiaPadre = jerarquiaAccesoEstructuraRepository.Consultar((int)jerarquiaAccesoEstructura.IdJerarquiaAccesoEstructuraPadre);

                if (jerarquiaAccesoEstructura.IdAcceso == jerarquiaPadre.IdAcceso)
                {
                    throw new CdisException("No puede asignar la misma estructura: " + jerarquiaPadre.IdAccesoNavigation.Nombre + " como hijo.");
                }
            }
        }

        private void ValidarNivelComponentes(JerarquiaAccesoEstructura jerarquiaAccesoEstructura)
        {
            Acceso acceso = accesoRepository.Consultar((int)jerarquiaAccesoEstructura.IdAcceso);
            /* 
            if (jerarquiaAccesoEstructura.IdJerarquiaAccesoEstructuraPadre == null && acceso.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoComponente)
            {
                throw new CdisException("No debe de asignar componentes en el primer nivel");
            } */
        }
    }
}
