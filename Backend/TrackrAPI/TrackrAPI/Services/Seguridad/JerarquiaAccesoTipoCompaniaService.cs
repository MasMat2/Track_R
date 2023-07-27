using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.Seguridad
{
    public class JerarquiaAccesoTipoCompaniaService
    {
        private readonly IJerarquiaAccesoTipoCompaniaRepository jerarquiaAccesoTipoCompaniaRepository;
        private readonly JerarquiaAccesoTipoCompaniaValidatorService jerarquiaAccesoTipoCompaniaValidatorService;
        public JerarquiaAccesoTipoCompaniaService
        (
            IJerarquiaAccesoTipoCompaniaRepository jerarquiaAccesoTipoCompaniaRepository,
            JerarquiaAccesoTipoCompaniaValidatorService jerarquiaAccesoTipoCompaniaValidatorService
        )
        {
            this.jerarquiaAccesoTipoCompaniaRepository = jerarquiaAccesoTipoCompaniaRepository;
            this.jerarquiaAccesoTipoCompaniaValidatorService = jerarquiaAccesoTipoCompaniaValidatorService;
        }

        public void Agregar(JerarquiaAccesoTipoCompania jerarquiaAccesoTipoCompania)
        {
            jerarquiaAccesoTipoCompaniaValidatorService.ValidarAgregar(jerarquiaAccesoTipoCompania);
            jerarquiaAccesoTipoCompaniaRepository.Agregar(jerarquiaAccesoTipoCompania);
        }

        public void Eliminar(int idJerarquiaAccesoTipoCompania)
        {
            JerarquiaAccesoTipoCompania jerarquiaAccesoTipoCompania = jerarquiaAccesoTipoCompaniaRepository.Consultar(idJerarquiaAccesoTipoCompania);
            jerarquiaAccesoTipoCompaniaValidatorService.ValidarEliminar(idJerarquiaAccesoTipoCompania);
            jerarquiaAccesoTipoCompaniaRepository.Eliminar(jerarquiaAccesoTipoCompania);
        }

        public void Guardar(List<JerarquiaAccesoTipoCompania> jerarquiaAccesoTipoCompaniaList)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                int idJerarquiaAcceso = jerarquiaAccesoTipoCompaniaList[0].IdJerarquiaAcceso;
                var configuracionActual = jerarquiaAccesoTipoCompaniaRepository.ConsultarPorJerarquiaAcceso(idJerarquiaAcceso);

                var eliminados = configuracionActual.Except(jerarquiaAccesoTipoCompaniaList);
                var nuevos = jerarquiaAccesoTipoCompaniaList.Except(configuracionActual);

                foreach (JerarquiaAccesoTipoCompania configuracion in eliminados)
                {
                    Eliminar(configuracion.IdJerarquiaAccesoTipoCompania);
                }

                foreach (JerarquiaAccesoTipoCompania configuracion in nuevos)
                {
                    Agregar(configuracion);
                }

                scope.Complete();
            }
        }
    }
}
