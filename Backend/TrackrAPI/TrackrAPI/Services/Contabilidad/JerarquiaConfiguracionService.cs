using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Contabilidad;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrackrAPI.Services.Contabilidad
{
    public class JerarquiaConfiguracionService
    {
        private IJerarquiaConfiguracionRepository jerarquiaConfiguracionRepository;
        private IJerarquiaColumnaRepository jerarquiaColumnaRepository;
        private IJerarquiaEstructuraRepository jerarquiaEstructuraRepository;

        public JerarquiaConfiguracionService(
            IJerarquiaConfiguracionRepository jerarquiaConfiguracionRepository,
            IJerarquiaColumnaRepository jerarquiaColumnaRepository,
            IJerarquiaEstructuraRepository jerarquiaEstructuraRepository)
        {
            this.jerarquiaConfiguracionRepository = jerarquiaConfiguracionRepository;
            this.jerarquiaColumnaRepository = jerarquiaColumnaRepository;
            this.jerarquiaEstructuraRepository = jerarquiaEstructuraRepository;
        }

        public List<JerarquiaConfiguracion> GetByHierarchyStructure(int idJerarquiaEstructura)
        {
            return jerarquiaConfiguracionRepository.GetByHierarchyStructure(idJerarquiaEstructura);
        }

        public IEnumerable<JerarquiaConfiguracionDto> ConsultarPorJerarquiaEstructura(int idJerarquiaEstructura)
        {
            var ghcList = jerarquiaConfiguracionRepository.ConsultarPorJerarquiaEstructuraDto(idJerarquiaEstructura);

            foreach (var ghc in ghcList)
            {
                ghc.Formula = ghc.Formula != null ? ConvertFormula(ghc.Formula) : null;
            }

            return ghcList;
        }

        public JerarquiaConfiguracionDto Consultar(int idJerarquiaConfiguracion)
        {
            return jerarquiaConfiguracionRepository.ConsultarDto(idJerarquiaConfiguracion);
        }

        public void Agregar(JerarquiaConfiguracion jerarquiaConfiguracion)
        {
            var jerarquiaEstructura = jerarquiaEstructuraRepository.Consultar(jerarquiaConfiguracion.IdJerarquiaEstructura);
            int numberNextCode = GetNumberCode(jerarquiaEstructura.IdJerarquia, jerarquiaConfiguracion.IdJerarquiaEstructura);
            jerarquiaConfiguracion.Clave = GetNextCode(jerarquiaConfiguracion.IdJerarquiaColumna, numberNextCode);
            jerarquiaConfiguracionRepository.Agregar(jerarquiaConfiguracion);
        }

        public void DeleteByHierarchy(int hierarchyId)
        {
            var configuraciones = jerarquiaConfiguracionRepository.ConsultarPorJerarquia(hierarchyId);
            jerarquiaConfiguracionRepository.Eliminar(configuraciones);
        }

        public void DeleteByHierarchyStructure(int hierarchyStructureId)
        {
            var configuraciones = jerarquiaConfiguracionRepository.GetByHierarchyStructure(hierarchyStructureId);
            jerarquiaConfiguracionRepository.Eliminar(configuraciones);
        }

        public void Editar(JerarquiaConfiguracion jerarquiaConfiguracion)
        {
            jerarquiaConfiguracionRepository.Editar(jerarquiaConfiguracion);
        }

        public void Eliminar(int idJerarquiaConfiguracion)
        {
            var configuracion = jerarquiaConfiguracionRepository.Consultar(idJerarquiaConfiguracion);
            jerarquiaConfiguracionRepository.Eliminar(configuracion);
        }

        public string GetNextCode(int hierarchyColumnId, int numberNextCode)
        {
            string letter = jerarquiaColumnaRepository.Consultar(hierarchyColumnId).Letra;
            return letter + numberNextCode;
        }

        public DataTable GetConfiguration(int hierarchyId)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("Descripcion");
            dataTable.Columns.Add("IdJerarquiaEstructura");
            dataTable.Columns.Add("IdJerarquiaEstructuraPadre");
            dataTable.Columns.Add("IdCuentaContable");
            dataTable.Columns.Add("IdAuxiliar");
            dataTable.Columns.Add("TotalHijos");

            List<JerarquiaColumna> hierarchyColumnList = jerarquiaColumnaRepository.ConsultarPorJerarquia(hierarchyId).ToList();
            hierarchyColumnList.ForEach(hierarchyColumn =>
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = hierarchyColumn.Nombre;
                dataColumn.ExtendedProperties["IdJerarquiaColumna"] = hierarchyColumn.IdJerarquiaColumna;
                dataColumn.ExtendedProperties["AgregadoPorSistema"] = hierarchyColumn.AgregadoPorSistema;
                dataColumn.ExtendedProperties["Letra"] = hierarchyColumn.Letra;
                dataTable.Columns.Add(dataColumn);
            });

            List<JerarquiaEstructuraDto> hierarchyStructureWrapperList = jerarquiaEstructuraRepository.GetConfiguration(hierarchyId).ToList();
            List<JerarquiaConfiguracion> list = jerarquiaConfiguracionRepository.ConsultarPorJerarquia(hierarchyId).ToList();

            hierarchyStructureWrapperList.ForEach(hierarchyStructureWrapper =>
            {
                DataRow row = dataTable.NewRow();
                row["Descripcion"] = hierarchyStructureWrapper.Descripcion;
                row["IdJerarquiaEstructura"] = hierarchyStructureWrapper.IdJerarquiaEstructura;
                row["IdJerarquiaEstructuraPadre"] = hierarchyStructureWrapper.IdJerarquiaEstructuraPadre;
                row["IdAuxiliar"] = hierarchyStructureWrapper.IdAuxiliar;
                row["IdCuentaContable"] = hierarchyStructureWrapper.IdCuentaContable;
                row["TotalHijos"] = hierarchyStructureWrapper.TotalHijos;

                hierarchyColumnList.ForEach(hierarchyColumn =>
                {
                    JerarquiaConfiguracion hierarchyConfiguration = list.FirstOrDefault(l => l.IdJerarquiaEstructura == hierarchyStructureWrapper.IdJerarquiaEstructura && l.IdJerarquiaColumna == hierarchyColumn.IdJerarquiaColumna);
                    if (hierarchyConfiguration != null)
                    {
                        if (hierarchyConfiguration.IdJerarquiaConfiguracionRelacionado != null)
                        {
                            row[hierarchyColumn.Nombre] = "VR" + hierarchyConfiguration.IdJerarquiaConfiguracionRelacionadoNavigation.Clave;
                        }
                        else if (!string.IsNullOrWhiteSpace(hierarchyConfiguration.Formula))
                        {
                            row[hierarchyColumn.Nombre] = ConvertFormula(hierarchyConfiguration.Formula);
                        }
                        else
                        {
                            row[hierarchyColumn.Nombre] = hierarchyConfiguration.Clave;
                        }
                    }
                });

                dataTable.Rows.Add(row);
            });

            return dataTable;
        }

        public string ConvertFormula(string formula)
        {

            string convertedFormula = Regex.Replace(formula, "ID[0-9]+[$_]*[$_]*", match =>
            {

                string code = Regex.Replace(match.Value, "ID[0-9]+", matchId =>
                {
                    int hierarchyConfigurationId = int.Parse(Regex.Match(matchId.Value, @"\d+").Value);
                    return jerarquiaConfiguracionRepository.Consultar(hierarchyConfigurationId).Clave;
                });

                code = code.Replace("$", "");
                code = code.Replace("_", "");

                int numberSign = match.Value.Count(f => f == '$');
                string value = Regex.Match(code, @"\d+").Value;
                string letter = Regex.Match(code, "[A-Za-z]").Value;

                if (numberSign == 1)
                {
                    if (Regex.IsMatch(match.Value, "[$][_]"))
                    {
                        code = "$" + letter + value;
                    }
                    else if (Regex.IsMatch(match.Value, "[_][$]"))
                    {
                        code = letter + "$" + value;
                    }
                }
                else if (numberSign == 2)
                {
                    code = "$" + letter + "$" + value;
                }

                return code.ToUpper();
            });

            return convertedFormula.ToUpper();
        }

        public int GetNumberCode(int hierarchyId, int hierarchyStructureId)
        {
            List<JerarquiaEstructura> hsList = jerarquiaEstructuraRepository.ConsultarPorJerarquia(hierarchyId).ToList();
            int numberCode = 0;

            for (int i = 0; i < hsList.Count; i++)
            {
                if (hsList[i].IdJerarquiaEstructura == hierarchyStructureId)
                {
                    numberCode = i + 1;
                    break;
                }
            }

            return numberCode;
        }

    }
}
