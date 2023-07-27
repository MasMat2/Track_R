using ClosedXML.Excel;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Contabilidad;
using TrackrAPI.Services.Contabilidad;
using TrackrAPI.Services.GestionEgresos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class CuentaContableService
    {
        private ICuentaContableRepository cuentaContableRepository;
        private IJerarquiaRepository jerarquiaRepository;
        private ISubtipoCuentaContableRepository subtipoCuentaContableRepository;

        private ConceptoService conceptoService;
        private CuentaContableValidatorService cuentaContableValidatorService;
        private JerarquiaEstructuraService jerarquiaEstructuraService;
        private SubtipoCuentaContableService subtipoCuentaContableService;
        private TipoAuxiliarService tipoAuxiliarService;
        private TipoConceptoService tipoConceptoService;
        private TipoCuentaContableService tipoCuentaContableService;

        public CuentaContableService(
            ICuentaContableRepository cuentaContableRepository,
            IJerarquiaRepository jerarquiaRepository,
            ISubtipoCuentaContableRepository subtipoCuentaContableRepository,
            ConceptoService conceptoService,
            CuentaContableValidatorService cuentaContableValidatorService,
            JerarquiaEstructuraService jerarquiaEstucturaService,
            SubtipoCuentaContableService subtipoCuentaContableService,
            TipoAuxiliarService tipoAuxiliarService,
            TipoConceptoService tipoConceptoService,
            TipoCuentaContableService tipoCuentaContableService
        )
        {
            this.cuentaContableRepository = cuentaContableRepository;
            this.jerarquiaRepository = jerarquiaRepository;
            this.subtipoCuentaContableRepository = subtipoCuentaContableRepository;

            this.conceptoService = conceptoService;
            this.cuentaContableValidatorService = cuentaContableValidatorService;
            this.jerarquiaEstructuraService = jerarquiaEstucturaService;
            this.subtipoCuentaContableService = subtipoCuentaContableService;
            this.tipoAuxiliarService = tipoAuxiliarService;
            this.tipoConceptoService = tipoConceptoService;
            this.tipoCuentaContableService = tipoCuentaContableService;
        }

        public IEnumerable<CuentaPartidaVivaGridDto> ConsultarConPartidasAbiertas(int idCompania)
        {
            var gridAccountList = cuentaContableRepository.ConsultarConPartidasAbiertas(idCompania).ToList();
            gridAccountList.ForEach(g => g.Periodo = GeneralConstant.GetMonthName(g.Mes + "") + " " + g.Anio);
            return gridAccountList;
        }

        public IEnumerable<CuentaContableDto> ConsultarTodosParaSelector(int idCompania)
        {
            return cuentaContableRepository.ConsultarTodosParaSelector(idCompania);
        }

        public IEnumerable<CuentaContableDto> ConsultarPorAgrupadorParaSelector(int idAgrupador)
        {
            return cuentaContableRepository.ConsultarTodosParaSelector(idAgrupador);
        }
        public IEnumerable<CuentaContableGridDto> ConsultarPorAgrupadorParaGrid(int idAgrupador)
        {
            return cuentaContableRepository.ConsultarPorAgrupadorParaGrid(idAgrupador);
        }

        public CuentaContable Consultar(int idCuentaContable)
        {
            return cuentaContableRepository.Consultar(idCuentaContable);
        }

        public CuentaContableDto ConsultarDto(int idCuentaContable)
        {
            var cuentaContable = cuentaContableRepository.ConsultarDto(idCuentaContable);
            cuentaContableValidatorService.ValidarExistencia(cuentaContable);
            return cuentaContable;
        }

        public IEnumerable<CuentaContableGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return cuentaContableRepository.ConsultarTodosParaGrid(idCompania);
        }

        public IEnumerable<CuentaContableGridDto> ConsultarPorFiltroParaGrid(int idCompania, CuentaContableFiltroDto filtro)
        {
            return cuentaContableRepository.ConsultarPorFiltroParaGrid(idCompania, filtro);
        }

        public void Agregar(CuentaContable cuentaContable)
        {
            using (TransactionScope scope  = new TransactionScope())
            {
                if (cuentaContable.Auxiliar && cuentaContable.IdSubtipoCuentaContable > 0)
                {
                    cuentaContable.IdTipoAuxiliar = tipoAuxiliarService.GetByAccountSubtype((int)cuentaContable.IdSubtipoCuentaContable)?.IdTipoAuxiliar;
                }

                cuentaContableValidatorService.ValidarAgregar(cuentaContable);
                var idCuentaContable = cuentaContableRepository.Agregar(cuentaContable).IdCuentaContable;

                if (cuentaContable.EsConcepto == true)
                {
                    string tipoMovimiento = "C";

                    if (cuentaContable.IdSubtipoCuentaContable > 0)
                    {
                        var subtipoCuenta = subtipoCuentaContableRepository.Consultar((int)cuentaContable.IdSubtipoCuentaContable);

                        if (subtipoCuenta.Clave == GeneralConstant.SubtypeAccountCodeIngreso)
                        {
                            tipoMovimiento = "A";
                        }
                    }

                    ConceptoFormularioDto concepto = new();
                    if(cuentaContable.IdCuentaContablePadre != null)
                    {
                        var nombreCuentaContablePadre = cuentaContableRepository.Consultar((int)cuentaContable.IdCuentaContablePadre).Nombre;
                        concepto.Nombre = nombreCuentaContablePadre + " - " + cuentaContable.Nombre;
                    }
                    else
                    {
                        concepto.Nombre = cuentaContable.Nombre;
                    }
                    concepto.IdCompania = (int)cuentaContable.IdCompania;
                    concepto.IdCuentaContable = idCuentaContable;
                    concepto.TipoMovimiento = tipoMovimiento;
                    concepto.Clave = cuentaContable.Numero;

                    TipoConcepto tipoConcepto = tipoConceptoService.ConsultarPorClave(GeneralConstant.ClaveTipoConceptoContable);
                    concepto.IdsTipoConcepto = new() { tipoConcepto.IdTipoConcepto };

                    conceptoService.Agregar(concepto);
                }

                // Se genera el nodo en la jerarquia estandar de contabildad
                var jerarquiaEstandar = jerarquiaRepository.ConsultarEstandar((int)cuentaContable.IdCompania, GeneralConstant.TypeAuxiliaryCodeAccount);

                if (jerarquiaEstandar != null)
                {
                    var jerarquiaEstructura = new JerarquiaEstructura()
                    {
                        IdCuentaContable = idCuentaContable,
                        IdJerarquia = jerarquiaEstandar.IdJerarquia
                    };

                    if (cuentaContable.IdCuentaContablePadre > 0)
                    {
                        var jerarquiaEstructuraPadre = jerarquiaEstructuraService.GetByAccount(jerarquiaEstandar.IdJerarquia, (int)cuentaContable.IdCuentaContablePadre);
                        jerarquiaEstructura.IdJerarquiaEstructuraPadre = jerarquiaEstructuraPadre?.IdJerarquiaEstructura;
                    }

                    jerarquiaEstructuraService.Agregar(jerarquiaEstructura);
                }

                scope.Complete();
            }
        }

        public void Editar(CuentaContable cuentaContable)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (cuentaContable.Auxiliar && cuentaContable.IdSubtipoCuentaContable > 0)
                {
                    cuentaContable.IdTipoAuxiliar = tipoAuxiliarService.GetByAccountSubtype((int)cuentaContable.IdSubtipoCuentaContable)?.IdTipoAuxiliar;
                }

                var actualCuentaContable = cuentaContableRepository.Consultar(cuentaContable.IdCuentaContable);
                if(cuentaContable.EsConcepto == true && actualCuentaContable.EsConcepto != true)
                {
                    string tipoMovimiento = "C";

                    if (cuentaContable.IdSubtipoCuentaContable > 0)
                    {
                        var subtipoCuenta = subtipoCuentaContableRepository.Consultar((int)cuentaContable.IdSubtipoCuentaContable);

                        if (subtipoCuenta.Clave == GeneralConstant.SubtypeAccountCodeIngreso)
                        {
                            tipoMovimiento = "A";
                        }
                    }

                    ConceptoFormularioDto concepto = new();
                    if (cuentaContable.IdCuentaContablePadre != null)
                    {
                        var nombreCuentaContablePadre = cuentaContableRepository.Consultar((int)cuentaContable.IdCuentaContablePadre).Nombre;
                        concepto.Nombre = nombreCuentaContablePadre + " - " + cuentaContable.Nombre;
                    }
                    else
                    {
                        concepto.Nombre = cuentaContable.Nombre;
                    }
                    concepto.IdCompania = (int)cuentaContable.IdCompania;
                    concepto.IdCuentaContable = cuentaContable.IdCuentaContable;
                    concepto.TipoMovimiento = tipoMovimiento;
                    concepto.Clave = cuentaContable.Numero;

                    TipoConcepto tipoConcepto = tipoConceptoService.ConsultarPorClave(GeneralConstant.ClaveTipoConceptoContable);
                    concepto.IdsTipoConcepto = new() { tipoConcepto.IdTipoConcepto };

                    conceptoService.Agregar(concepto);
                }

                cuentaContableValidatorService.ValidarEditar(cuentaContable);
                cuentaContableRepository.Editar(cuentaContable);


                // Se genera el nodo en la jerarquia estandar de contabildad
                var jerarquiaEstandar = jerarquiaRepository.ConsultarEstandar((int)cuentaContable.IdCompania, GeneralConstant.TypeAuxiliaryCodeAccount);

                if (jerarquiaEstandar != null)
                {
                    var estaEnLaJerarquia = jerarquiaEstructuraService.GetByAccount(jerarquiaEstandar.IdJerarquia, cuentaContable.IdCuentaContable);

                    if (estaEnLaJerarquia == null)
                    {
                        var jerarquiaEstructura = new JerarquiaEstructura()
                        {
                            IdCuentaContable = cuentaContable.IdCuentaContable,
                            IdJerarquia = jerarquiaEstandar.IdJerarquia
                        };

                        if (cuentaContable.IdCuentaContablePadre > 0)
                        {
                            var jerarquiaEstructuraPadre = jerarquiaEstructuraService.GetByAccount(jerarquiaEstandar.IdJerarquia, (int)cuentaContable.IdCuentaContablePadre);
                            jerarquiaEstructura.IdJerarquiaEstructuraPadre = jerarquiaEstructuraPadre.IdJerarquiaEstructura;
                        }

                        jerarquiaEstructuraService.Agregar(jerarquiaEstructura);
                    }
                }

                scope.Complete();
            }
        }

        public void Eliminar(int idCuentaContable)
        {
            CuentaContable cuentaContable = cuentaContableRepository.Consultar(idCuentaContable);
            cuentaContableValidatorService.ValidarEliminar(idCuentaContable);
            cuentaContableRepository.Eliminar(cuentaContable);
        }

        public IEnumerable<CuentaContable> ConsultarParaJerarquiaGrid(int idJerarquia)
        {
            return cuentaContableRepository.ConsultarParaJerarquiaGrid(idJerarquia);
        }

        public void CargarCuentas(ArchivoExcelDto archivo, int idCompania)
        {
            // Abrir archivo
            XLWorkbook workbook = ExcelHelper.ObtenerWorkbook(archivo.ArchivoBase64);

            // Obtener hoja de trabajo
            string nombreWorksheet = "Plantilla_CC";
            IXLWorksheet worksheet = ExcelHelper.ObtenerWorksheet(workbook, nombreWorksheet);

            // Obtener y validar encabezados
            const string numeroHeader = "Número";
            const string nombreHeader = "Nombre";
            const string descripcionHeader = "Descripción";
            const string tipoHeader = "Tipo";
            const string subtipoHeader = "Subtipo";
            const string cuentaPadreHeader = "Cuenta Padre";
            const string reconciliatoriaHeader = "Reconciliatoria";
            const string partidaAbiertaHeader = "Partida Abierta";
            const string auxiliarHeader = "Auxiliar";
            const string recibeMovimientosHeader = "Recibe Movimientos";
            const string automaticaHeader = "Automática";
            const string esConceptoHeader = "Es Concepto";

            IXLRangeRow headerRow = worksheet.RangeUsed().Row(3);
            ExcelHelper.ValidarHeaders(headerRow, new List<string>()
            {
                numeroHeader,
                nombreHeader,
                descripcionHeader,
                tipoHeader,
                subtipoHeader,
                cuentaPadreHeader,
                reconciliatoriaHeader,
                partidaAbiertaHeader,
                auxiliarHeader,
                recibeMovimientosHeader,
                automaticaHeader,
                esConceptoHeader
            });

            int numeroCol = ExcelHelper.ObtenerIndiceColumna(numeroHeader, headerRow);
            int nombreCol = ExcelHelper.ObtenerIndiceColumna(nombreHeader, headerRow);
            int descripcionCol = ExcelHelper.ObtenerIndiceColumna(descripcionHeader, headerRow);
            int tipoCol = ExcelHelper.ObtenerIndiceColumna(tipoHeader, headerRow);
            int subtipoCol = ExcelHelper.ObtenerIndiceColumna(subtipoHeader, headerRow);
            int cuentaPadreCol = ExcelHelper.ObtenerIndiceColumna(cuentaPadreHeader, headerRow);
            int reconciliatoriaCol = ExcelHelper.ObtenerIndiceColumna(reconciliatoriaHeader, headerRow);
            int partidaAbiertaCol = ExcelHelper.ObtenerIndiceColumna(partidaAbiertaHeader, headerRow);
            int auxiliarCol = ExcelHelper.ObtenerIndiceColumna(auxiliarHeader, headerRow);
            int recibeMovimientosCol = ExcelHelper.ObtenerIndiceColumna(recibeMovimientosHeader, headerRow);
            int automaticaCol = ExcelHelper.ObtenerIndiceColumna(automaticaHeader, headerRow);
            int esConceptoCol = ExcelHelper.ObtenerIndiceColumna(esConceptoHeader, headerRow);

            // Obtener los valores de los selectores
            var tiposCuenta = tipoCuentaContableService.ConsultarTodosParaSelector();
            var subtiposCuenta = subtipoCuentaContableService.ConsultarTodosParaSelector();

            // Filas del archivo sin encabezado
            IEnumerable<IXLRangeRow> rows = worksheet.RangeUsed().RowsUsed().Skip(3);

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                int renglon = 0;
                foreach (IXLRangeRow row in rows)
                {
                    renglon++;

                    try
                    {
                        // Obtener campos del registro
                        string numero = ExcelHelper.ObtenerValorCelda(row, numeroCol, numeroHeader, true);
                        string nombre = ExcelHelper.ObtenerValorCelda(row, nombreCol, nombreHeader, true);
                        string descripcion = ExcelHelper.ObtenerValorCelda(row, descripcionCol, descripcionHeader, true);
                        string tipoString = ExcelHelper.ObtenerValorCelda(row, tipoCol, tipoHeader, false);
                        string subtipoString = ExcelHelper.ObtenerValorCelda(row, subtipoCol, subtipoHeader, false);
                        string cuentaPadreString = ExcelHelper.ObtenerValorCelda(row, cuentaPadreCol, cuentaPadreHeader, false); ;
                        string reconciliatoriaString = ExcelHelper.ObtenerValorCelda(row, reconciliatoriaCol, reconciliatoriaHeader, true);
                        string partidaAbiertaString = ExcelHelper.ObtenerValorCelda(row, partidaAbiertaCol, partidaAbiertaHeader, true);
                        string auxiliarString = ExcelHelper.ObtenerValorCelda(row, auxiliarCol, auxiliarHeader, true);
                        string recibeMovimientosString = ExcelHelper.ObtenerValorCelda(row, recibeMovimientosCol, recibeMovimientosHeader, true);
                        string automaticaString = ExcelHelper.ObtenerValorCelda(row, automaticaCol, automaticaHeader, true);
                        string esConceptoString = ExcelHelper.ObtenerValorCelda(row, esConceptoCol, esConceptoHeader, true);

                        // Castear los strings a los valores correspondientes
                        // Tipo Cuenta Contable
                        TipoCuentaContableDto tipoCuentaContable = null;

                        if (!string.IsNullOrEmpty(tipoString))
                        {
                            tipoCuentaContable = tiposCuenta
                                .Where(tc => tc.Nombre.ToLower() == tipoString.ToLower())
                                .FirstOrDefault();

                            if (tipoCuentaContable == null)
                                throw new CdisException("El tipo de cuenta no existe.");
                        }

                        // Subtipo Cuenta Contable
                        SubtipoCuentaContableDto subtipoCuentaContable = null;

                        if (!string.IsNullOrEmpty(subtipoString))
                        {
                            subtipoCuentaContable = subtiposCuenta
                                .Where(sc => sc.Nombre.ToLower() == subtipoString.ToLower())
                                .FirstOrDefault();

                            if (subtipoCuentaContable == null)
                                throw new CdisException("El subtipo de cuenta no existe.");
                        }

                        if (subtipoCuentaContable != null && tipoCuentaContable == null)
                            throw new CdisException("El tipo de cuenta contable es necesario cuando se especifica el subtipo de cuenta");

                        if ((tipoCuentaContable != null && subtipoCuentaContable != null) &&
                            (tipoCuentaContable.IdTipoCuentaContable != subtipoCuentaContable.IdTipoCuentaContable))
                            throw new CdisException("El subtipo de cuenta no es válido para el tipo de cuenta especificado");

                        // Cuenta Padre
                        var cuentaContablePadre = cuentaContableRepository.ConsultarPorNumero(idCompania, cuentaPadreString);

                        if (!string.IsNullOrEmpty(cuentaPadreString) && (cuentaContablePadre == null))
                            throw new CdisException("La cuenta padre no existe.");

                        // Campos Booleanos
                        bool reconciliatoria = ExcelHelper.CastBoolean(reconciliatoriaHeader, reconciliatoriaString);
                        bool partidaAbierta = ExcelHelper.CastBoolean(partidaAbiertaHeader, partidaAbiertaString);
                        bool auxiliar = ExcelHelper.CastBoolean(auxiliarHeader, auxiliarString);
                        bool recibeMovimientos = ExcelHelper.CastBoolean(recibeMovimientosHeader, recibeMovimientosString);
                        bool automatica = ExcelHelper.CastBoolean(automaticaHeader, automaticaString);
                        bool esConcepto = ExcelHelper.CastBoolean(esConceptoHeader, esConceptoString);

                        // Agregar registro
                        CuentaContable cuentaContable = new()
                        {
                            Numero = numero,
                            Nombre = nombre,
                            Descripcion = descripcion,
                            IdTipoCuentaContable = tipoCuentaContable?.IdTipoCuentaContable,
                            IdSubtipoCuentaContable = subtipoCuentaContable?.IdSubtipoCuentaContable,
                            IdCuentaContablePadre = cuentaContablePadre?.IdCuentaContable,
                            Reconciliatoria = reconciliatoria,
                            RecibeMovimientos = recibeMovimientos,
                            Auxiliar = auxiliar,
                            PartidaAbierta = partidaAbierta,
                            Automatica = automatica,
                            EsConcepto = esConcepto,
                            IdCompania = idCompania
                        };

                        Agregar(cuentaContable);
                    }
                    catch (CdisException ex)
                    {
                        throw new CdisException($"En el renglón {renglon}: {ex.ErrorMessage}");
                    }
                }

                ts.Complete();
            }
        }
    }
}
