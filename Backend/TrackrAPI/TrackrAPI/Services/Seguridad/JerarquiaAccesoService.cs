using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.Seguridad
{
    public class JerarquiaAccesoService
    {
        private readonly IJerarquiaAccesoTipoCompaniaRepository jerarquiaAccesoTipoCompaniaRepository;
        private readonly IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository;
        private readonly IJerarquiaAccesoRepository jerarquiaAccesoRepository;
        private readonly JerarquiaAccesoValidatorService jerarquiaAccesoValidatorService;
        private readonly JerarquiaAccesoTipoCompaniaService jerarquiaAccesoTipoCompaniaService;
        private readonly JerarquiaAccesoEstructuraService jerarquiaAccesoEstructuraService;

        public JerarquiaAccesoService(
            IJerarquiaAccesoTipoCompaniaRepository jerarquiaAccesoTipoCompaniaRepository,
            IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository,
            IJerarquiaAccesoRepository jerarquiaAccesoRepository,
            JerarquiaAccesoValidatorService jerarquiaAccesoValidatorService,
            JerarquiaAccesoTipoCompaniaService jerarquiaAccesoTipoCompaniaService,
            JerarquiaAccesoEstructuraService jerarquiaAccesoEstructuraService
        )
        {
            this.jerarquiaAccesoTipoCompaniaRepository = jerarquiaAccesoTipoCompaniaRepository;
            this.jerarquiaAccesoEstructuraRepository = jerarquiaAccesoEstructuraRepository;
            this.jerarquiaAccesoRepository = jerarquiaAccesoRepository;
            this.jerarquiaAccesoValidatorService = jerarquiaAccesoValidatorService;
            this.jerarquiaAccesoTipoCompaniaService = jerarquiaAccesoTipoCompaniaService;
            this.jerarquiaAccesoEstructuraService = jerarquiaAccesoEstructuraService;
        }

        public JerarquiaAccesoDto Consultar(int idJerarquiaAcceso)
        {
            return jerarquiaAccesoRepository.ConsultarDto(idJerarquiaAcceso);
        }

        public IEnumerable<JerarquiaAccesoDto> ConsultarParaGrid(int idCompania)
        {
            return jerarquiaAccesoRepository.ConsultarParaGrid(idCompania);
        }

        public IEnumerable<JerarquiaAccesoDto> ConsultarParaSelector(string claveTipoCompania)
        {
            return jerarquiaAccesoRepository.ConsultarParaSelector(claveTipoCompania);
        }

        public void Agregar(JerarquiaAccesoDto jerarquiaAccesoDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (jerarquiaAccesoDto.IdsTipoCompania == null || jerarquiaAccesoDto.IdsTipoCompania.Count == 0)
                {
                    throw new CdisException("Se debe seleccionar al menos un tipo de compañía");
                }

                JerarquiaAcceso jerarquiaAcceso = new()
                {
                    IdCompania = jerarquiaAccesoDto.IdCompania,
                    Nombre = jerarquiaAccesoDto.Nombre
                };

                jerarquiaAccesoValidatorService.ValidarAgregar(jerarquiaAcceso);
                int idJerarquiaAcceso = jerarquiaAccesoRepository.Agregar(jerarquiaAcceso).IdJerarquiaAcceso;

                // Agregar configuraciones JerarquiaAccesoTipoCompania
                List<JerarquiaAccesoTipoCompania> jerarquiaAccesoTipoCompaniaList = jerarquiaAccesoDto.IdsTipoCompania
                    .Select(idTipoCompania =>
                    {
                        return new JerarquiaAccesoTipoCompania()
                        {
                            IdJerarquiaAcceso = idJerarquiaAcceso,
                            IdTipoCompania = idTipoCompania
                        };
                    })
                    .ToList();

                jerarquiaAccesoTipoCompaniaService.Guardar(jerarquiaAccesoTipoCompaniaList);
                scope.Complete();
            }
        }

        public void Editar(JerarquiaAccesoDto jerarquiaAccesoDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (jerarquiaAccesoDto.IdsTipoCompania == null || jerarquiaAccesoDto.IdsTipoCompania.Count == 0)
                {
                    throw new CdisException("Se debe seleccionar al menos un tipo de compañía");
                }

                JerarquiaAcceso jerarquiaAcceso = new()
                {
                    IdJerarquiaAcceso = jerarquiaAccesoDto.IdJerarquiaAcceso,
                    IdCompania = jerarquiaAccesoDto.IdCompania,
                    Nombre = jerarquiaAccesoDto.Nombre
                };

                jerarquiaAccesoValidatorService.ValidarEditar(jerarquiaAcceso);
                jerarquiaAccesoRepository.Editar(jerarquiaAcceso);

                // Agregar configuraciones JerarquiaAccesoTipoCompania
                List<JerarquiaAccesoTipoCompania> jerarquiaAccesoTipoCompaniaList = jerarquiaAccesoDto.IdsTipoCompania
                    .Select(idTipoCompania =>
                    {
                        return new JerarquiaAccesoTipoCompania()
                        {
                            IdJerarquiaAcceso = jerarquiaAccesoDto.IdJerarquiaAcceso,
                            IdTipoCompania = idTipoCompania
                        };
                    })
                    .ToList();

                jerarquiaAccesoTipoCompaniaService.Guardar(jerarquiaAccesoTipoCompaniaList);
                scope.Complete();
            }
        }

        public void Eliminar(int idJerarquiaAcceso)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                JerarquiaAcceso jerarquiaAcceso = jerarquiaAccesoRepository.Consultar(idJerarquiaAcceso);
                var jerarquiaAccesoEstructuras = jerarquiaAccesoEstructuraRepository.ConsultarPorJerarquiaAcceso(idJerarquiaAcceso);
                var jerarquiaAccesoTipoCompanias = jerarquiaAccesoTipoCompaniaRepository.ConsultarPorJerarquiaAcceso(idJerarquiaAcceso);

                // Eliminar JerarquiaAccesoEstructura
                foreach (JerarquiaAccesoEstructura jerarquiaAccesoEstructura in jerarquiaAccesoEstructuras)
                {
                    jerarquiaAccesoEstructuraService.Eliminar(jerarquiaAccesoEstructura.IdJerarquiaAccesoEstructura);
                }

                // Eliminar Configuraciones JerarquiaAccesoTipoCompania
                foreach (JerarquiaAccesoTipoCompania jerarquiaAccesoTipoCompania in jerarquiaAccesoTipoCompanias)
                {
                    jerarquiaAccesoTipoCompaniaService.Eliminar(jerarquiaAccesoTipoCompania.IdJerarquiaAccesoTipoCompania);
                }

                // Eliminar Jerarquia Acceso
                jerarquiaAccesoValidatorService.ValidarEliminar(jerarquiaAcceso.IdJerarquiaAcceso);
                jerarquiaAccesoRepository.Eliminar(jerarquiaAcceso);
                scope.Complete();
            }
        }
    }
}
