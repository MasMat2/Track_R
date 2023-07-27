using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Contabilidad;
using TrackrAPI.Repositorys.GestionEgresos;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrackrAPI.Services.Contabilidad
{
    public class JerarquiaEstructuraService
    {
        private IJerarquiaConfiguracionRepository jerarquiaConfiguracionRepository;
        private IJerarquiaEstructuraRepository jerarquiaEstructuraRepository;
        private IJerarquiaColumnaRepository jerarquiaColumnaRepository;
        private IJerarquiaRepository jerarquiaRepository;
        private ICuentaContableRepository cuentaContableRepository;
        private IAuxiliarRepository auxiliarRepository;
        private JerarquiaConfiguracionService jerarquiaConfiguracionService;
        private JerarquiaEstructuraValidatorService jerarquiaEstructuraValidatorService;

        public JerarquiaEstructuraService(IJerarquiaConfiguracionRepository jerarquiaConfiguracionRepository,
            IJerarquiaEstructuraRepository jerarquiaEstructuraRepository,
            IJerarquiaColumnaRepository jerarquiaColumnaRepository,
            IJerarquiaRepository jerarquiaRepository,
            ICuentaContableRepository cuentaContableRepository,
            IAuxiliarRepository auxiliarRepository,
            JerarquiaConfiguracionService jerarquiaConfiguracionService,
            JerarquiaEstructuraValidatorService jerarquiaEstructuraValidatorService)
        {
            this.jerarquiaConfiguracionRepository = jerarquiaConfiguracionRepository;
            this.jerarquiaEstructuraRepository = jerarquiaEstructuraRepository;
            this.jerarquiaColumnaRepository = jerarquiaColumnaRepository;
            this.jerarquiaRepository = jerarquiaRepository;
            this.cuentaContableRepository = cuentaContableRepository;
            this.auxiliarRepository = auxiliarRepository;
            this.jerarquiaConfiguracionService = jerarquiaConfiguracionService;
            this.jerarquiaEstructuraValidatorService = jerarquiaEstructuraValidatorService;
        }

        public JerarquiaEstructura Consultar(int idJerarquiaEstructura)
        {
            return jerarquiaEstructuraRepository.Consultar(idJerarquiaEstructura);
        }

        public JerarquiaEstructura GetByAccount(int hierarchyId, int accountId)
        {
            return jerarquiaEstructuraRepository.ConsultarPorCuentaContable(hierarchyId, accountId);
        }

        public JerarquiaEstructura GetByAuxiliary(int hierarchyId, int auxiliaryId)
        {
            return jerarquiaEstructuraRepository.ConsultarPorAuxiliar(hierarchyId, auxiliaryId);
        }

        public JerarquiaEstructura GetByDescription(int hierarchyId, string description, string number)
        {
            return jerarquiaEstructuraRepository.GetByDescription(hierarchyId, description, number);
        }

        public IEnumerable<JerarquiaEstructura> ConsultarPorJerarquia(int hierarchyId)
        {
            return jerarquiaEstructuraRepository.ConsultarPorJerarquia(hierarchyId);
        }

        public void Agregar(JerarquiaEstructura hierarchyStructure)
        {
            JerarquiaEstructura hierarchyStructureExist = null;

            if (hierarchyStructure.IdCuentaContable != null)
            {
                hierarchyStructureExist = jerarquiaEstructuraRepository.ConsultarPorCuentaContable((int)hierarchyStructure.IdCuentaContable, hierarchyStructure.IdJerarquia);
            }
            else if (hierarchyStructure.IdAuxiliar != null)
            {
                hierarchyStructureExist = jerarquiaEstructuraRepository.ConsultarPorAuxiliar((int)hierarchyStructure.IdAuxiliar, hierarchyStructure.IdJerarquia);
            }

            if (hierarchyStructureExist != null) return;

            jerarquiaEstructuraValidatorService.ValidarAgregar(hierarchyStructure);
            jerarquiaEstructuraRepository.Agregar(hierarchyStructure);

            string number = "";

            if (hierarchyStructure.IdCuentaContable != null)
            {
                number = cuentaContableRepository.Consultar((int)hierarchyStructure.IdCuentaContable).Numero;
            }
            else if (hierarchyStructure.IdAuxiliar != null)
            {
                number = auxiliarRepository.Consultar((int)hierarchyStructure.IdAuxiliar).Numero;
            }
            else if (!string.IsNullOrWhiteSpace(hierarchyStructure.Descripcion))
            {
                number = hierarchyStructure.Numero;
            }

            if (hierarchyStructure.IdJerarquiaEstructuraPadre != null)
            {
                JerarquiaEstructura hierarchyStructureParent = jerarquiaEstructuraRepository.Consultar((int)hierarchyStructure.IdJerarquiaEstructuraPadre);
                hierarchyStructure.Ruta = hierarchyStructureParent.Ruta + "." + number;
            }
            else
            {
                hierarchyStructure.Ruta = number;
            }

            jerarquiaEstructuraValidatorService.ValidarEditar(hierarchyStructure);
            jerarquiaEstructuraRepository.Editar(hierarchyStructure);

            // int numberNextCode = RecalculateCodes(hierarchyStructure);
            int numberNextCode = 0;

            // Se agregan los registros de configuracion de formulas default.
            List<JerarquiaColumna> hierarchyColumnList = jerarquiaColumnaRepository.ConsultarPorJerarquia(hierarchyStructure.IdJerarquia).ToList();
            hierarchyColumnList.ForEach(hierarchyColumn =>
            {
                if (hierarchyColumn.AgregadoPorSistema || hierarchyColumn.IdJerarquiaColumnaRelacionada != null)
                {
                    JerarquiaConfiguracion hierarchyConfiguration = new JerarquiaConfiguracion();
                    hierarchyConfiguration.Clave = jerarquiaConfiguracionService.GetNextCode(hierarchyColumn.IdJerarquiaColumna, numberNextCode);
                    hierarchyConfiguration.AgregadoPorSistema = hierarchyStructure.IdCuentaContable != null || hierarchyStructure.IdAuxiliar != null;
                    hierarchyConfiguration.IdJerarquiaEstructura = hierarchyStructure.IdJerarquiaEstructura;
                    hierarchyConfiguration.IdJerarquiaColumna = hierarchyColumn.IdJerarquiaColumna;
                    jerarquiaConfiguracionService.Agregar(hierarchyConfiguration);
                }
            });

        }


        public List<JerarquiaEstructuraDto> ObtenerSaldoDto(int hierarchyId, int year, int month, int? initialYear, int? initialMonth,
            int? hierarchyStructureAuxiliaryId, int? hierarchyIdTypeAuxiliary, bool budgetary, int? versionId)
        {
            return jerarquiaEstructuraRepository.ObtenerSaldo(hierarchyId, year, month, initialYear, initialMonth,
                hierarchyStructureAuxiliaryId, hierarchyIdTypeAuxiliary, budgetary, versionId);
        }

        public DataTable ObtenerSaldo(int hierarchyId, int year, int month, int? initialYear, int? initialMonth,
            int? hierarchyStructureAuxiliaryId, int? hierarchyIdTypeAuxiliary, bool budgetary, int? versionId)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("Descripcion");
            dataTable.Columns.Add("IdJerarquiaEstructura");
            dataTable.Columns.Add("IdJerarquiaEstructuraPadre");
            dataTable.Columns.Add("IdCuentaContable");
            dataTable.Columns.Add("IdAuxiliar");
            dataTable.Columns.Add("TieneMovimientos");
            dataTable.Columns.Add("TotalHijos");

            Jerarquia jerarquia = jerarquiaRepository.Consultar(hierarchyId);

            if (jerarquia == null)
            {
                return dataTable;
            }

            List<SaldoDto> balanceWrapperList = new List<SaldoDto>();

            // string currencySymbol = hierarchy.IdCompaniaNavigation.IdMonedaNavigation.Simbolo;
            string simboloMoneda = "$";

            List<JerarquiaColumna> hierarchyColumnList = jerarquiaColumnaRepository.ConsultarPorJerarquia(hierarchyId).ToList();
            hierarchyColumnList.ForEach(hierarchyColumn =>
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = hierarchyColumn.Nombre;
                dataColumn.ExtendedProperties["IdJerarquiaColumna"] = hierarchyColumn.IdJerarquiaColumna;
                dataColumn.ExtendedProperties["AgregadoPorSistema"] = hierarchyColumn.AgregadoPorSistema;
                dataTable.Columns.Add(dataColumn);

                if (hierarchyColumn.IdJerarquiaColumnaRelacionada != null)
                {
                    int yearAux = hierarchyColumn.Anio != null ? (int)hierarchyColumn.Anio : year;
                    int monthAux = hierarchyColumn.Mes != null ? (int)hierarchyColumn.Mes : month;
                    int? versionIdAux = hierarchyColumn.IdVersionPoliza != null ? (int)hierarchyColumn.IdVersionPoliza : versionId;
                    bool budgetaryAux = versionIdAux != null ? true : budgetary;

                    List<JerarquiaEstructuraDto> list = jerarquiaEstructuraRepository
                        .ObtenerSaldo(hierarchyId,
                        yearAux,
                        monthAux,
                        initialYear,
                        initialMonth,
                        hierarchyStructureAuxiliaryId,
                        hierarchyIdTypeAuxiliary,
                        budgetaryAux, versionIdAux);

                    SaldoDto bw = new SaldoDto();
                    bw.Mes = monthAux;
                    bw.Anio = yearAux;
                    bw.IdJerarquia = hierarchyColumn.IdJerarquia;
                    bw.IdVersionPoliza = versionIdAux;
                    bw.EsPresupuesto = budgetaryAux;
                    bw.JerarquiaEstructuraDtoList = list;
                    balanceWrapperList.Add(bw);
                }
            });

            List<JerarquiaEstructuraDto> hierarchyStructureWrapperList = jerarquiaEstructuraRepository
                .ObtenerSaldo(hierarchyId, year, month, initialYear, initialMonth,
                hierarchyStructureAuxiliaryId, hierarchyIdTypeAuxiliary,
                budgetary, versionId);

            hierarchyStructureWrapperList.ForEach(hierarchyStructureWrapper =>
            {
                DataRow row = dataTable.NewRow();
                row["Descripcion"] = hierarchyStructureWrapper.Descripcion;
                row["IdJerarquiaEstructura"] = hierarchyStructureWrapper.IdJerarquiaEstructura;
                row["IdJerarquiaEstructuraPadre"] = hierarchyStructureWrapper.IdJerarquiaEstructuraPadre;
                row["IdAuxiliar"] = hierarchyStructureWrapper.IdCuentaContable;
                row["IdCuentaContable"] = hierarchyStructureWrapper.IdCuentaContable;
                row["TieneMovimientos"] = hierarchyStructureWrapper.TieneMovimientos;
                row["TotalHijos"] = hierarchyStructureWrapper.TotalHijos;

                hierarchyColumnList.ForEach(hierarchyColumn =>
                {
                    JerarquiaConfiguracion hierarchyConfiguration = jerarquiaConfiguracionRepository
                        .GetByHierarchyStructure(hierarchyStructureWrapper.IdJerarquiaEstructura, hierarchyColumn.IdJerarquiaColumna);

                    if (hierarchyConfiguration != null)
                    {
                        if (hierarchyConfiguration.IdJerarquiaConfiguracionRelacionado == null && string.IsNullOrWhiteSpace(hierarchyConfiguration.Formula))
                        {
                            switch (hierarchyColumn.Acumula)
                            {
                                case GeneralConstant.AcumulaSaldoInicial:
                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hierarchyStructureWrapper.SaldoInicial, simboloMoneda);
                                    break;
                                case GeneralConstant.AcumulaCargo:
                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hierarchyStructureWrapper.Cargo, simboloMoneda);
                                    break;
                                case GeneralConstant.AcumulaAbono:
                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hierarchyStructureWrapper.Abono, simboloMoneda);
                                    break;
                                case GeneralConstant.AcumulaSaldoFinal:
                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hierarchyStructureWrapper.SaldoFinal, simboloMoneda);
                                    break;
                                default:

                                    int yearAux = hierarchyColumn.Anio != null ? (int)hierarchyColumn.Anio : year;
                                    int monthAux = hierarchyColumn.Mes != null ? (int)hierarchyColumn.Mes : month;
                                    int? versionIdAux = hierarchyColumn.IdVersionPoliza != null ? (int)hierarchyColumn.IdVersionPoliza : versionId;
                                    bool budgetaryAux = versionIdAux != null ? true : budgetary;

                                    SaldoDto balanceWrapper = balanceWrapperList.FirstOrDefault(bw => bw.Mes == monthAux
                                        && bw.IdVersionPoliza == versionIdAux
                                        && bw.EsPresupuesto == budgetaryAux
                                        && bw.Anio == yearAux
                                        && bw.IdJerarquia == hierarchyColumn.IdJerarquia);

                                    if (balanceWrapper != null)
                                    {

                                        JerarquiaEstructuraDto hs = balanceWrapper.JerarquiaEstructuraDtoList
                                            .FirstOrDefault(h => h.IdJerarquiaEstructura == hierarchyStructureWrapper.IdJerarquiaEstructura);

                                        if (hs != null)
                                        {
                                            switch (hierarchyColumn.IdJerarquiaColumnaRelacionadaNavigation.Acumula)
                                            {
                                                case GeneralConstant.AcumulaSaldoInicial:
                                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hs.SaldoInicial, simboloMoneda);
                                                    break;
                                                case GeneralConstant.AcumulaCargo:
                                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hs.Cargo, simboloMoneda);
                                                    break;
                                                case GeneralConstant.AcumulaAbono:
                                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hs.Abono, simboloMoneda);
                                                    break;
                                                case GeneralConstant.AcumulaSaldoFinal:
                                                    row[hierarchyColumn.Nombre] = Utileria.FormatearMonto(hs.SaldoFinal, simboloMoneda);
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            try
                            {
                                List<JerarquiaClaveDto> searchedCodeList = new List<JerarquiaClaveDto>();
                                decimal monto = this.ObtenerValorPorClave(year, month, initialYear, initialMonth,
                                    hierarchyConfiguration, searchedCodeList, hierarchyStructureAuxiliaryId, hierarchyIdTypeAuxiliary, budgetary, versionId);
                                monto = Math.Round(monto, 2, MidpointRounding.AwayFromZero);

                                bool esPorcentaje = hierarchyConfiguration.IdJerarquiaColumnaNavigation.EsPorcentaje == true;

                                row[hierarchyColumn.Nombre] = esPorcentaje ? monto + " %" : Utileria.FormatearMonto(monto, simboloMoneda);
                            }
                            catch (CdisException ex)
                            {
                                row[hierarchyColumn.Nombre] = "<i class=\"fas fa-exclamation-circle\" title=\"" + ex.ErrorMessage + "\"></i>";
                            }
                        }
                    }
                });

                dataTable.Rows.Add(row);
            });

            return dataTable;
        }

        public decimal ObtenerValorPorClave(int year, int month, int? initialYear, int? initialMonth,
            JerarquiaConfiguracion hierarchyConfiguration, List<JerarquiaClaveDto> searchedCodeList,
            int? hierarchyStructureAuxiliaryId, int? hierarchyIdTypeAuxiliary, bool budgetary, int? versionId)
        {
            int hierarchyId = hierarchyConfiguration.IdJerarquiaEstructuraNavigation.IdJerarquia;

            // Si la configuracion fue agregada por el sistema quiere decir que es el saldo final.
            if (hierarchyConfiguration.Formula == null && hierarchyConfiguration.IdJerarquiaConfiguracionRelacionado == null)
            {

                JerarquiaClaveDto findedCode = searchedCodeList.Find(codeWrapper => codeWrapper.Clave == hierarchyConfiguration.Clave
                      && codeWrapper.IdJerarquia == hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquia
                      && codeWrapper.Valor != null);

                if (findedCode != null)
                {
                    return (decimal)findedCode.Valor;
                }

                List<JerarquiaEstructuraDto> hswList = this.ObtenerSaldoDto(hierarchyId, year,
                    month, initialYear, initialMonth,
                    hierarchyStructureAuxiliaryId, hierarchyIdTypeAuxiliary, budgetary, versionId);

                if (hierarchyConfiguration.IdJerarquiaColumnaNavigation.Acumula == GeneralConstant.AcumulaSaldoInicial)
                {
                    hswList.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveSaldoInicial, hsw.SaldoInicial)));
                    JerarquiaEstructuraDto initialBalance = hswList.Find(hsw => hsw.ClaveSaldoInicial == hierarchyConfiguration.Clave);
                    return initialBalance != null ? initialBalance.SaldoInicial : 0;
                }
                else if (hierarchyConfiguration.IdJerarquiaColumnaNavigation.Acumula == GeneralConstant.AcumulaCargo)
                {
                    hswList.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveCargo, hsw.Cargo)));
                    JerarquiaEstructuraDto charge = hswList.Find(hsw => hsw.ClaveCargo == hierarchyConfiguration.Clave);
                    return charge != null ? charge.Cargo : 0;
                }
                else if (hierarchyConfiguration.IdJerarquiaColumnaNavigation.Acumula == GeneralConstant.AcumulaAbono)
                {
                    hswList.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveAbono, hsw.Abono)));
                    JerarquiaEstructuraDto payment = hswList.Find(hsw => hsw.ClaveAbono == hierarchyConfiguration.Clave);
                    return payment != null ? payment.Abono : 0;
                }
                else if (hierarchyConfiguration.IdJerarquiaColumnaNavigation.Acumula == GeneralConstant.AcumulaSaldoFinal)
                {
                    hswList.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveSaldoFinal, hsw.SaldoFinal)));
                    JerarquiaEstructuraDto finalBalance = hswList.Find(hsw => hsw.ClaveSaldoFinal == hierarchyConfiguration.Clave);
                    return finalBalance != null ? finalBalance.SaldoFinal : 0;
                }
                else if (hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquiaColumnaRelacionada != null)
                {
                    int yearAux = hierarchyConfiguration.IdJerarquiaColumnaNavigation.Anio != null ? (int)hierarchyConfiguration.IdJerarquiaColumnaNavigation.Anio : year;
                    int monthAux = hierarchyConfiguration.IdJerarquiaColumnaNavigation.Mes != null ? (int)hierarchyConfiguration.IdJerarquiaColumnaNavigation.Mes : month;
                    int? versionIdAux = hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdVersionPoliza != null ? (int)hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdVersionPoliza : versionId;
                    bool budgetaryAux = versionIdAux != null ? true : budgetary;

                    List<JerarquiaEstructuraDto> hlist = this.ObtenerSaldoDto(
                        hierarchyId,
                        yearAux,
                        monthAux,
                        initialYear,
                        initialMonth,
                        hierarchyStructureAuxiliaryId,
                        hierarchyIdTypeAuxiliary,
                        budgetaryAux,
                        versionIdAux);

                    switch (hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquiaColumnaRelacionadaNavigation.Acumula)
                    {
                        case GeneralConstant.AcumulaSaldoInicial:
                            hlist.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveSaldoInicial, hsw.SaldoInicial)));
                            JerarquiaEstructuraDto initialBalance = hlist.Find(hsw => hsw.IdJerarquiaEstructura == hierarchyConfiguration.IdJerarquiaEstructura);
                            return initialBalance != null ? initialBalance.SaldoInicial : 0;
                        case GeneralConstant.AcumulaCargo:
                            hlist.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveCargo, hsw.Cargo)));
                            JerarquiaEstructuraDto charge = hlist.Find(hsw => hsw.IdJerarquiaEstructura == hierarchyConfiguration.IdJerarquiaEstructura);
                            return charge != null ? charge.Cargo : 0;
                        case GeneralConstant.AcumulaAbono:
                            hlist.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveAbono, hsw.Abono)));
                            JerarquiaEstructuraDto payment = hlist.Find(hsw => hsw.IdJerarquiaEstructura == hierarchyConfiguration.IdJerarquiaEstructura);
                            return payment != null ? payment.Abono : 0;
                        case GeneralConstant.AcumulaSaldoFinal:
                            hlist.ForEach(hsw => searchedCodeList.Add(new JerarquiaClaveDto(hierarchyId, hsw.ClaveSaldoFinal, hsw.SaldoFinal)));
                            JerarquiaEstructuraDto finalBalance = hlist.Find(hsw => hsw.IdJerarquiaEstructura == hierarchyConfiguration.IdJerarquiaEstructura);
                            return finalBalance != null ? finalBalance.SaldoFinal : 0;
                    }


                }
            }

            // Si la configuracion hace referencia al valor de otra configuracion.
            if (hierarchyConfiguration.IdJerarquiaConfiguracionRelacionado != null)
            {
                JerarquiaConfiguracion hc = jerarquiaConfiguracionRepository.Consultar((int)hierarchyConfiguration.IdJerarquiaConfiguracionRelacionado);

                JerarquiaClaveDto findedCode = searchedCodeList.Find(codeWrapper => codeWrapper.Clave == hierarchyConfiguration.Clave
                      && codeWrapper.IdJerarquia == hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquia
                      && codeWrapper.Valor != null);

                if (findedCode == null)
                {
                    // Error de dependencia ciclica, el valor hace referencia a otra celda
                    // la cual hace referencia a la primera.
                    if (searchedCodeList.Find(codeWrapper => codeWrapper.Clave == hc.Clave
                        && codeWrapper.IdJerarquia == hc.IdJerarquiaColumnaNavigation.IdJerarquia
                        && codeWrapper.Valor == null) != null)
                    {
                        throw new CdisException(GeneralConstant.MensajeErrorReferenciaCircular);
                    }

                    JerarquiaClaveDto cw = new JerarquiaClaveDto(hc.IdJerarquiaColumnaNavigation.IdJerarquia, hc.Clave);
                    searchedCodeList.Add(cw);
                    return ObtenerValorPorClave(year, month, initialYear, initialMonth, hc, searchedCodeList, hierarchyStructureAuxiliaryId, hierarchyIdTypeAuxiliary, budgetary, versionId);
                }
                else
                {
                    return (decimal)findedCode.Valor;
                }
            }

            // Si la configuracion hace referencia a una formula.
            if (hierarchyConfiguration.Formula != null)
            {
                string formula =  jerarquiaConfiguracionService.ConvertFormula(hierarchyConfiguration.Formula);
                formula = formula.Replace("$", "");
                formula = Regex.Replace(formula, GeneralConstant.RegexFormula, match =>
                {

                    string code = match.Value;

                    JerarquiaClaveDto findedCode = searchedCodeList.Find(codeWrapper => codeWrapper.Clave == match.Value
                        && codeWrapper.IdJerarquia == hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquia
                        && codeWrapper.Valor != null);

                    if (findedCode == null)
                    {

                        if (searchedCodeList.Find(codeWrapper => codeWrapper.Clave == code
                            && codeWrapper.IdJerarquia == hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquia
                            && codeWrapper.Valor == null) != null)
                        {
                            throw new CdisException(GeneralConstant.MensajeErrorReferenciaCircular);
                        }

                        JerarquiaConfiguracion hc = jerarquiaConfiguracionRepository.GetByCode(hierarchyId, code);
                        // Si el codigo no existe se lanza un error
                        if (hc == null)
                        {
                            throw new CdisException("Error en la fórmula " + hierarchyConfiguration.Formula + ", la clave " + code + " no existe.");
                        }
                        JerarquiaClaveDto cw = new JerarquiaClaveDto(hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquia, code);
                        searchedCodeList.Add(cw);
                        decimal valor = ObtenerValorPorClave(year, month, initialYear, initialMonth, hc, searchedCodeList, hierarchyStructureAuxiliaryId, hierarchyIdTypeAuxiliary, budgetary, versionId);

                        JerarquiaClaveDto cwWithValue = new JerarquiaClaveDto(hierarchyConfiguration.IdJerarquiaColumnaNavigation.IdJerarquia, code, valor);
                        searchedCodeList.Add(cwWithValue);

                        return valor.ToString();
                    }
                    else
                    {
                        return findedCode.Valor.ToString();
                    }
                });

                try
                {
                    formula = formula.Replace(" ", "");
                    formula = formula.Replace("+-", "-");
                    formula = formula.Replace("-+", "-");
                    formula = formula.Replace("--", "+");
                    formula = formula.Replace("++", "+");
                    var expression = new Expression(formula);
                    return (decimal)expression.calculate();
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            // Si no coincide la configuracion con ninguno de los casos anteriores, quiere decir
            // que no tiene una formula configurada, ni valor relacionado
            throw new CdisException("Formula no configurada");
        }

        public void RecalcularClaves(int hierarchyId)
        {
            List<JerarquiaEstructura> hsList = jerarquiaEstructuraRepository.ConsultarPorJerarquia(hierarchyId).ToList();

            for (int i = 0; i < hsList.Count; i++)
            {
                List<JerarquiaConfiguracion> hcList = jerarquiaConfiguracionRepository.GetByHierarchyStructure(hsList[i].IdJerarquiaEstructura);
                foreach (JerarquiaConfiguracion hc in hcList)
                {
                    int value = i + 1;
                    string letter = Regex.Match(hc.Clave, "[A-Za-z]").Value;
                    JerarquiaConfiguracion hierarchyConfiguration = new JerarquiaConfiguracion();
                    hierarchyConfiguration.IdJerarquiaConfiguracion = hc.IdJerarquiaConfiguracion;
                    hierarchyConfiguration.Clave = letter + value;
                    hierarchyConfiguration.Formula = hc.Formula;
                    hierarchyConfiguration.AgregadoPorSistema = hc.AgregadoPorSistema;
                    hierarchyConfiguration.IdJerarquiaConfiguracionRelacionado = hc.IdJerarquiaConfiguracionRelacionado;
                    hierarchyConfiguration.IdJerarquiaEstructura = hc.IdJerarquiaEstructura;
                    hierarchyConfiguration.IdJerarquiaColumna = hc.IdJerarquiaColumna;
                    jerarquiaConfiguracionRepository.Editar(hierarchyConfiguration);
                }
            }
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaArbol(int idJerarquia)
        {
            return jerarquiaEstructuraRepository.ConsultarPorJerarquiaArbol(idJerarquia);
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarArbol(int idJerarquia)
        {
            List<JerarquiaEstructuraArbolDto> jerarquiasPadre = jerarquiaEstructuraRepository.ConsultarPadres(idJerarquia).ToList();

            return ConsultarHijos(jerarquiasPadre);
        }

        private IEnumerable<JerarquiaEstructuraArbolDto> ConsultarHijos(IEnumerable<JerarquiaEstructuraArbolDto> jerarquiasPadre)
        {
            foreach (JerarquiaEstructuraArbolDto padre in jerarquiasPadre)
            {
                padre.Hijos = jerarquiaEstructuraRepository.ConsultarHijos(padre.IdJerarquiaEstructura).ToList();

                if (padre.Hijos.Any())
                {
                    ConsultarHijos(padre.Hijos);
                }
            }

            return jerarquiasPadre;
        }

        public void Agregar(List<JerarquiaEstructura> jerarquias)
        {
            foreach(JerarquiaEstructura jerarquia in jerarquias)
            {
                Agregar(jerarquia);
            }
        }

        public void DeleteByHierarchy(int hierarchyId)
        {
            var estructuras = jerarquiaEstructuraRepository.ConsultarPorJerarquia(hierarchyId);

            foreach (var estructura in estructuras)
            {
                estructura.IdJerarquiaEstructuraPadre = null;
                jerarquiaEstructuraValidatorService.ValidarEditar(estructura);
                jerarquiaEstructuraRepository.Editar(estructura);
            }

            jerarquiaEstructuraRepository.Eliminar(estructuras);
        }

        public void Eliminar(int idJerarquiaEstructura)
        {
            JerarquiaEstructura jerarquiaEstructura = jerarquiaEstructuraRepository.Consultar(idJerarquiaEstructura);

            jerarquiaConfiguracionService.DeleteByHierarchyStructure(jerarquiaEstructura.IdJerarquiaEstructura);

            List<JerarquiaEstructura> hsList = jerarquiaEstructuraRepository.ConsultarPorJerarquia(jerarquiaEstructura.IdJerarquia).ToList();
            int index = 0;

            for (int i = 0; i < hsList.Count; i++)
            {
                if (hsList[i].IdJerarquiaEstructura == jerarquiaEstructura.IdJerarquiaEstructura)
                {
                    index = i;
                    break;
                }
            }

            int recordsDeleted = 0;

            var hijos = jerarquiaEstructuraRepository.ConsultarHijosDeEstructura(jerarquiaEstructura.IdJerarquiaEstructura);

            EliminarHijos(hijos, ref recordsDeleted);

            jerarquiaEstructuraRepository.Eliminar(jerarquiaEstructura);

            hsList = jerarquiaEstructuraRepository.ConsultarPorJerarquia(jerarquiaEstructura.IdJerarquia).ToList();

            for (int i = index; i < hsList.Count; i++)
            {
                RemoverPosicion(hsList[i].IdJerarquiaEstructura, recordsDeleted + 1);
            }
        }

        private void EliminarHijos(List<JerarquiaEstructura> hierarchyStructureList, ref int recordsDeleted)
        {
            foreach (JerarquiaEstructura hierarchyStructure in hierarchyStructureList)
            {
                var hijos = jerarquiaEstructuraRepository.ConsultarHijosDeEstructura(hierarchyStructure.IdJerarquiaEstructura);

                if (hijos.Any())
                {
                    EliminarHijos(hijos, ref recordsDeleted);
                }

                jerarquiaConfiguracionService.DeleteByHierarchyStructure(hierarchyStructure.IdJerarquiaEstructura);
                jerarquiaEstructuraRepository.Eliminar(hierarchyStructure);
                recordsDeleted++;
            };
        }

        public void RemoverPosicion(int hierarchyStructureId, int numberPosition)
        {
            List<JerarquiaConfiguracion> hcList = jerarquiaConfiguracionService.GetByHierarchyStructure(hierarchyStructureId).ToList();
            foreach (JerarquiaConfiguracion hc in hcList)
            {
                string value = Regex.Match(hc.Clave, @"\d+").Value;
                string letter = Regex.Match(hc.Clave, "[A-Za-z]").Value;
                JerarquiaConfiguracion hierarchyConfiguration = new JerarquiaConfiguracion();
                hierarchyConfiguration.IdJerarquiaConfiguracion = hc.IdJerarquiaConfiguracion;
                hierarchyConfiguration.Clave = letter + (int.Parse(value) - numberPosition);
                hierarchyConfiguration.Formula = hc.Formula;
                hierarchyConfiguration.AgregadoPorSistema = hc.AgregadoPorSistema;
                hierarchyConfiguration.IdJerarquiaConfiguracionRelacionado = hc.IdJerarquiaConfiguracionRelacionado;
                hierarchyConfiguration.IdJerarquiaEstructura = hc.IdJerarquiaEstructura;
                hierarchyConfiguration.IdJerarquiaColumna = hc.IdJerarquiaColumna;
                jerarquiaConfiguracionRepository.Editar(hierarchyConfiguration);
            }
        }

    }
}
