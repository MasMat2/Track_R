using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Contabilidad;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Contabilidad
{
    public class JerarquiaService
    {
        private IJerarquiaRepository jerarquiaRepository;
        private ICuentaContableRepository cuentaContableRepository;
        private JerarquiaColumnaService jerarquiaColumnaService;
        private JerarquiaEstructuraService jerarquiaEstructuraService;
        private JerarquiaConfiguracionService jerarquiaConfiguracionService;
        private JerarquiaValidatorService jerarquiaValidatorService;

        public JerarquiaService(IJerarquiaRepository jerarquiaRepository,
            ICuentaContableRepository cuentaContableRepository,
            JerarquiaColumnaService jerarquiaColumnaService,
            JerarquiaEstructuraService jerarquiaEstructuraService,
            JerarquiaConfiguracionService jerarquiaConfiguracionService,
            JerarquiaValidatorService jerarquiaValidatorService)
        {
            this.jerarquiaRepository = jerarquiaRepository;
            this.cuentaContableRepository = cuentaContableRepository;
            this.jerarquiaColumnaService = jerarquiaColumnaService;
            this.jerarquiaEstructuraService = jerarquiaEstructuraService;
            this.jerarquiaConfiguracionService = jerarquiaConfiguracionService;
            this.jerarquiaValidatorService = jerarquiaValidatorService;
        }

        public IEnumerable<Jerarquia> GetByAccountGroupingDefault(int accountGroupingId)
        {
            return jerarquiaRepository.GetByAccountGroupingDefault(accountGroupingId);
        }

        public IEnumerable<JerarquiaGridDto> ConsultarParaGrid(int idCompania, string claveTipoAxuiliar)
        {
            return jerarquiaRepository.ConsultarParaGrid(idCompania, claveTipoAxuiliar);
        }

        public IEnumerable<JerarquiaGridDto> ConsultarParaGrid(int idCompania)
        {
            return jerarquiaRepository.ConsultarParaGrid(idCompania);
        }


        public IEnumerable<JerarquiaSelectorDto> ConsultarParaSelector(int idCompania)
        {
            return jerarquiaRepository.ConsultarParaSelector(idCompania);
        }

        public IEnumerable<JerarquiaSelectorDto> ConsultarParaSelector(int idCompania, bool obtenerTipoCuenta)
        {
            return jerarquiaRepository.ConsultarParaSelector(idCompania, obtenerTipoCuenta);
        }

        public int Agregar(Jerarquia hierarchy)
        {
            hierarchy.IdJerarquia = 0;
            jerarquiaValidatorService.ValidarAgregar(hierarchy);
            int idJerarquia = jerarquiaRepository.Agregar(hierarchy).IdJerarquia;
            // Se agrega a nivel sistema la columna saldo final a la configuracion de la jerarquia
            // recien creada.
            JerarquiaColumna hierarchyColumn = new JerarquiaColumna();
            hierarchyColumn.Nombre = "Saldo Inicial";
            hierarchyColumn.Acumula = GeneralConstant.AcumulaSaldoInicial;
            hierarchyColumn.AgregadoPorSistema = true;
            hierarchyColumn.IdJerarquia = hierarchy.IdJerarquia;
            jerarquiaColumnaService.Agregar(hierarchyColumn);

            hierarchyColumn = new JerarquiaColumna();
            hierarchyColumn.Nombre = "Cargo";
            hierarchyColumn.Acumula = GeneralConstant.AcumulaCargo;
            hierarchyColumn.AgregadoPorSistema = true;
            hierarchyColumn.IdJerarquia = hierarchy.IdJerarquia;
            jerarquiaColumnaService.Agregar(hierarchyColumn);

            hierarchyColumn = new JerarquiaColumna();
            hierarchyColumn.Nombre = "Abono";
            hierarchyColumn.Acumula = GeneralConstant.AcumulaAbono;
            hierarchyColumn.AgregadoPorSistema = true;
            hierarchyColumn.IdJerarquia = hierarchy.IdJerarquia;
            jerarquiaColumnaService.Agregar(hierarchyColumn);

            hierarchyColumn = new JerarquiaColumna();
            hierarchyColumn.Nombre = "Saldo Final";
            hierarchyColumn.Acumula = GeneralConstant.AcumulaSaldoFinal;
            hierarchyColumn.AgregadoPorSistema = true;
            hierarchyColumn.IdJerarquia = hierarchy.IdJerarquia;
            jerarquiaColumnaService.Agregar(hierarchyColumn);

            return idJerarquia;
        }

        public void AddByHierarchyBase(Jerarquia hierarchy, int hierarchyBaseId)
        {
            Agregar(hierarchy);

            List<JerarquiaEstructura> hierarchyStructureList = jerarquiaEstructuraService.ConsultarPorJerarquia(hierarchyBaseId).ToList();

            hierarchyStructureList = hierarchyStructureList.OrderBy(ha => ha.Ruta).ToList();

            hierarchyStructureList.ForEach(ha => {
                JerarquiaEstructura hierarchyStructure = new JerarquiaEstructura();
                hierarchyStructure.IdJerarquia = hierarchy.IdJerarquia;

                CuentaContable account = null;
                if (ha.IdCuentaContable > 0)
                {
                    account = cuentaContableRepository.Consultar((int)ha.IdCuentaContable);

                    if (account.IdCompania == 2)
                    {
                        CuentaContable accountSystem = cuentaContableRepository.ConsultarPorNumero((int)hierarchy.IdCompania, account.Numero);
                        hierarchyStructure.IdCuentaContable = accountSystem?.IdCuentaContable;
                    }
                    else
                    {
                        hierarchyStructure.IdCuentaContable = ha.IdCuentaContable;
                    }
                }

                hierarchyStructure.IdAuxiliar = ha.IdAuxiliar;

                if (ha.IdJerarquiaEstructuraPadre == null)
                {
                    hierarchyStructure.IdJerarquiaEstructuraPadre = null;
                }
                else
                {
                    JerarquiaEstructura haBaseAccountParent = jerarquiaEstructuraService.Consultar((int)ha.IdJerarquiaEstructuraPadre);
                    JerarquiaEstructura haParent;
                    if (haBaseAccountParent.IdCuentaContable != null)
                    {

                        int accountParentId = 0;

                        if (account != null && account.IdCompania == 2)
                        {
                            CuentaContable accountSystem = cuentaContableRepository.ConsultarPorNumero((int)hierarchy.IdCompania, haBaseAccountParent.IdCuentaContableNavigation.Numero);
                            accountParentId = accountSystem.IdCuentaContable;
                        }
                        else
                        {
                            accountParentId = (int)haBaseAccountParent.IdCuentaContable;
                        }

                        haParent = jerarquiaEstructuraService.GetByAccount(hierarchy.IdJerarquia, accountParentId);
                    }
                    else if (haBaseAccountParent.IdAuxiliar != null)
                    {
                        haParent = jerarquiaEstructuraService.GetByAuxiliary(hierarchy.IdJerarquia, (int)haBaseAccountParent.IdAuxiliar);
                    }
                    else
                    {
                        haParent = jerarquiaEstructuraService.GetByDescription(hierarchy.IdJerarquia, haBaseAccountParent.Descripcion, haBaseAccountParent.Numero);
                    }
                    hierarchyStructure.IdJerarquiaEstructuraPadre = haParent.IdJerarquiaEstructura;
                }

                hierarchyStructure.Numero = ha.Numero;
                hierarchyStructure.Descripcion = ha.Descripcion;
                hierarchyStructure.Ruta = ha.Ruta;
                hierarchyStructure.Nivel = ha.Nivel;
                jerarquiaEstructuraService.Agregar(hierarchyStructure);
            });

            jerarquiaEstructuraService.RecalcularClaves(hierarchy.IdJerarquia);
        }


        public Jerarquia Consultar(int idJerarquia)
        {
            return jerarquiaRepository.Consultar(idJerarquia);
        }

        public void Editar(Jerarquia jerarquia)
        {
            jerarquiaValidatorService.ValidarEditar(jerarquia);
            jerarquiaRepository.Editar(jerarquia);
        }

        public void Eliminar(int idJerarquia)
        {
            Jerarquia jerarquia = jerarquiaRepository.Consultar(idJerarquia);

            if (jerarquia.Estandar)
            {
                throw new CdisException("No se puede eliminar la Jerarquía estándar.");
            }

            jerarquiaConfiguracionService.DeleteByHierarchy(idJerarquia);
            jerarquiaColumnaService.DeleteByHierarchy(idJerarquia);
            jerarquiaEstructuraService.DeleteByHierarchy(idJerarquia);
            jerarquiaValidatorService.ValidarEliminar(idJerarquia);
            jerarquiaRepository.Eliminar(jerarquia);
        }
    }
}
