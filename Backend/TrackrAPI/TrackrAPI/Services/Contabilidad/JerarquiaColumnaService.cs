using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Contabilidad;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;

namespace TrackrAPI.Services.Contabilidad
{
    public class JerarquiaColumnaService
    {
        private IJerarquiaColumnaRepository jerarquiaColumnaRepository;
        private IJerarquiaEstructuraRepository jerarquiaEstructuraRepository;
        private JerarquiaConfiguracionService jerarquiaConfiguracionService;

        public JerarquiaColumnaService(IJerarquiaColumnaRepository jerarquiaColumnaRepository,
            IJerarquiaEstructuraRepository jerarquiaEstructuraRepository,
            JerarquiaConfiguracionService jerarquiaConfiguracionService)
        {
            this.jerarquiaColumnaRepository = jerarquiaColumnaRepository;
            this.jerarquiaEstructuraRepository = jerarquiaEstructuraRepository;
            this.jerarquiaConfiguracionService = jerarquiaConfiguracionService;
        }

        public JerarquiaColumnaDto Consultar(int idJerarquiaColumna)
        {
            return jerarquiaColumnaRepository.ConsultarDto(idJerarquiaColumna);
        }

        public IEnumerable<JerarquiaColumnaDto> ConsultarPorJerarquia(int idJerarquia)
        {
            return jerarquiaColumnaRepository.ConsultarPorJerarquiaDto(idJerarquia);
        }

        public IEnumerable<JerarquiaColumnaDto> ConsultarPorJerarquiaNoUsada(int hierarchyStructureId)
        {
            return jerarquiaColumnaRepository.ConsultarPorJerarquiaNoUsada(hierarchyStructureId);
        }

        public void Agregar(JerarquiaColumna hierarchyColumn)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                JerarquiaColumna hierarchyColumnExist = jerarquiaColumnaRepository.ConsultarPorNombre(hierarchyColumn.Nombre, hierarchyColumn.IdJerarquia);

                if (hierarchyColumnExist != null)
                {
                    throw new CdisException("Ya existe una columna con ese nombre");
                }

                hierarchyColumn.Letra = GetNextLetterColumn(hierarchyColumn.IdJerarquia).ToString();
                jerarquiaColumnaRepository.Agregar(hierarchyColumn);

                if (hierarchyColumn.IdJerarquiaColumnaRelacionada != null)
                {
                    List<JerarquiaEstructura> hierarchyStructureList = jerarquiaEstructuraRepository.ConsultarPorJerarquia(hierarchyColumn.IdJerarquia).ToList();
                    hierarchyStructureList = hierarchyStructureList.OrderBy(ha => ha.Ruta).ToList();

                    for (int i = 0; i < hierarchyStructureList.Count; i++)
                    {
                        JerarquiaEstructura hierarchyStructure = hierarchyStructureList[i];
                        JerarquiaConfiguracion hierarchyConfiguration = new JerarquiaConfiguracion();
                        hierarchyConfiguration.Clave = jerarquiaConfiguracionService.GetNextCode(hierarchyColumn.IdJerarquiaColumna, i + 1);
                        hierarchyConfiguration.AgregadoPorSistema = hierarchyStructure.IdCuentaContable != null || hierarchyStructure.IdAuxiliar != null;
                        hierarchyConfiguration.IdJerarquiaEstructura = hierarchyStructure.IdJerarquiaEstructura;
                        hierarchyConfiguration.IdJerarquiaColumna = hierarchyColumn.IdJerarquiaColumna;
                        jerarquiaConfiguracionService.Agregar(hierarchyConfiguration);
                    }
                }

                scope.Complete();
            }
        }

        public void Editar(JerarquiaColumna jerarquiaColumna)
        {
            jerarquiaColumnaRepository.Editar(jerarquiaColumna);
        }

        public void DeleteByHierarchy(int hierarchyId)
        {
            var columnas = jerarquiaColumnaRepository.ConsultarPorJerarquia(hierarchyId);
            jerarquiaColumnaRepository.Eliminar(columnas);
        }

        public void Eliminar(int hierarchyColumnId)
        {
            var jerarquiaColumna = jerarquiaColumnaRepository.Consultar(hierarchyColumnId);
            jerarquiaColumnaRepository.Eliminar(jerarquiaColumna);
        }

        private char GetNextLetterColumn(int hierarchyId)
        {
            List<JerarquiaColumna> hierarchyColumnList = jerarquiaColumnaRepository.ConsultarPorJerarquia(hierarchyId).ToList();
            JerarquiaColumna hierarchyColumn = hierarchyColumnList.OrderByDescending(hc => hc.Letra).FirstOrDefault();

            if (hierarchyColumn == null)
            {
                return 'A';
            }

            string lastLetter = hierarchyColumn.Letra;

            if (lastLetter[0] == 'Z')
            {
                return 'Z';
            }
            else
            {
                return (char)(((int)lastLetter[0]) + 1);
            }
        }

    }
}
