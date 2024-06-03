namespace TrackrAPI.Helpers
{
    public class GeneralConstant
    {
        // public const string ClaveTipoUsuarioAdministrador = "001";
        // public const string ClaveTipoUsuarioPaciente = "002";

        public const string ClaveCompaniaBase = "000";
        public static readonly string ClaveEntidadEstructuraMuestra = "006";

        public static readonly int UsuarioMaestroAtisc = 1;
        public static readonly int IdPerfilAdministradorDefault = 1;

        public static readonly int IdHospitalMuguerza = 169;
        public static readonly int IdHospitalPredeterminado =  174;
        public static readonly int IdCompaniaMuguerza = 177;

        public static readonly string ClaveTipoAccesoMenu = "001";
        public static readonly string ClaveTipoAccesoEvento = "002";
        public static readonly string ClaveTipoAccesoSistema = "003";
        public static readonly string ClaveTipoAccesoComponente = "004";

        public static readonly string ClaveTipoUsuarioAdministrador = "001";
        public static readonly string ClaveTipoUsuarioPaciente = "004";

        public static readonly string ClavePerfilPaciente = "BASE007";
        public static readonly string ClavePerfilSinAcceso = "BASE002";
        public static readonly string ClavePerfilMedico = "001";
        public static readonly string ClavePerfilAsistente = "005";

        public static readonly string RutaArchivoAcceso = "Archivo/Acceso/";
        public static readonly string RutaArchivoHospitalLogotipo = "Archivo/HospitalLogotipo/";
        public static readonly string RutaArchivoCompaniaLogotipo = "Archivo/CompaniaLogotipo/";

        public static readonly string NombreImagenAtiDefault = "Logo_ATI.png";
        public static readonly string TipoMimeAtiDefault = "image/png";

        public static readonly string SeparadorFolio = "-";

        public const string ClaveFormaPagoTarjetaDebito = "001";
        public const string ClaveFormaPagoTarjetaCredito = "002";
        public const string ClaveFormaPagoTransferencia = "003";
        public const string ClaveFormaPagoDeposito = "004";
        public const string ClaveFormaPagoTerceros = "005";
        public const string ClaveFormaPagoEfectivo = "006";
        public const string ClaveFormaPagoTarjetaCreditoVisa = "007";
        public const string ClaveFormaPagoTarjetaCreditoMastercard = "008";
        public const string ClaveFormaPagoTarjetaCreditoCarnet = "009";
        public const string ClaveFormaPagoTarjetaCreditoAmericanExpress = "010";
        public const string ClaveFormaPagoTarjetaDebitoVisa = "011";
        public const string ClaveFormaPagoTarjetaDebitoMastercard = "012";
        public const string ClaveFormaPagoTarjetaDebitoCarnet = "013";
        public const string ClaveFormaPagoTrasladoSaldo = "026";

        public const string ClaveMetodoPagoContado = "030";
        public const string ClaveMetodoPagoCredito = "031";

        public const string ClaveFormaPagoOtasFormasPago = "200";

        public const string ClaveConceptoEfectivo = "007";
        public const string ClaveConceptoTarjetaCreditoVisa = "100";
        public const string ClaveConceptoTarjetaCreditoMastercard = "101";
        public const string ClaveConceptoTarjetaCreditoCarnet = "102";
        public const string ClaveConceptoTarjetaCreditoAmericanExpress = "103";
        public const string ClaveConceptoTarjetaDebitoVisa = "200";
        public const string ClaveConceptoTarjetaDebitoMastercard = "201";
        public const string ClaveConceptoTarjetaDebitoCarnet = "202";

        public const string ClaveAreaFarmacia = "001";
        public const string ClaveAreaLaboratorio = "002";
        public const string ClaveAreaImagenologia = "003";
        public const string ClaveAreaUrgencias = "007";
        public const string ClaveAreaSinEspecificar = "BASE004";

        public const string ClaveServicioFarmacia = "001";
        public const string ClaveServicioConsultaMedica = "001";

        public const string ClaveEstatusNoPagado = "001";
        public const string ClaveEstatusPagado = "002";
        public const string ClaveEstatusPagoParcial = "003";

        public const string ClaveEstiloNotaReferencia = "001";

        public const string ClaveEstatusSurtido = "002";

        public const string ClaveEstatusNotaGastoActivo = "001";
        public const string ClaveEstatusNotaGastoCancelado = "002";
        public const string ClaveEstatusNotaGastoPendiente = "003";
        public const string ClaveEstatusNotaGastoPagada = "004";

        public const string ClaveEstatusNotaFlujoActivo = "001";
        public const string ClaveEstatusNotaFlujoCancelada = "002";
        public const string ClaveEstatusNotaFlujoAplicado = "003";

        public const string ClaveEstatusRemisionPendiente = "001";
        public const string ClaveEstatusRemisionParcial = "002";
        public const string ClaveEstatusRemisionPagada = "003";

        public const string ClaveEstatusLiquidacion = "002";

        public const string ClaveTipoNotaGastoGeneral = "001";
        public const string ClaveTipoNotaGastoComisiones = "002";
        public const string ClaveTipoNotaGastoSueldoDiario = "003";
        public const string ClaveTipoNotaGastoEntradaOrdenCompra = "004";

        // ===== TIPOS MOVIMIENTO MATERIAL  ======= //
        public const string ClaveTipoMovimientoEntradaPorOrdenCompra = "001";
        public const string ClaveTipoMovimientoOtrasEntradas = "002";
        public const string ClaveTipoMovimientoSalidaPorVenta = "003";
        public const string ClaveTipoMovimientoOtrasSalidas = "004";
        public const string ClaveTipoMovimientoEntradaPorAjuste = "005";
        public const string ClaveTipoMovimientoSalidaPorAjuste = "006";
        public const string ClaveTipoMovimientoEntradaPorTraspaso = "007";
        public const string ClaveTipoMovimientoSalidaPorTraspaso = "008";
        public const string ClaveTipoMovimientoCancelacionDeSalida = "009";
        public const string ClaveTipoMovimientoCancelacionDeEntrada = "010";
        public const string ClaveTipoMovimientoEntradaPorDevolucion = "011";
        public const string ClaveTipoMovimientoSalidaPorMerma = "012";
        public const string ClaveTipoMovimientoSalidaPorCaducidad = "013";
        public const string ClaveTipoMovimientoEntradaPorProduccion = "015";
        public const string ClaveTipoMovimientoEntradaPorDevolucionCaduco = "016";

        public const string ClaveTipoMovimientoCancelacionEntradaOrdenCompra = "017";
        public const string ClaveTipoMovimientoSalidaPorDevolucionProveedor = "018";
        public const string ClaveTipoMovimientoSalidaProduccion = "019";
        public const string ClaveTipoMovimientoCancelacionSalidaProduccion = "020";
        public const string ClaveTipoMovimientoEntradaPorDevolucionProduccion = "021";
        public const string ClaveTipoMovimientoCancelacionEntradaProduccion = "022";
        public const string ClaveTipoMovimientoCancelacionSalidaPorVenta = "023";


        public const string ClaveTipoNotaDeVenta = "001";
        public const string ClaveTipoNotaDeDevolucion = "002";

        public const string ClaveTipoActivoCirculanteChequera = "001";
        public const string ClaveTipoActivoCirculanteCaja = "002";

        public const string ClaveTipoPresentacionServicio = "001";
        public const string ClaveTipoPresentacionMedicamento = "002";

        public const string ClaveTipoConceptoContable = "001";
        public const string ClaveTipoConceptoGasto = "002";
        public const string ClaveTipoConceptoAlmacen = "003";
        public const string ClaveTipoConceptoAdministrativo = "004";

        public const string ClaveEstatusPagoNoPagado = "001";

        public const string ClaveEstatusEstudioEnProceso = "001";
        public const string ClaveEstatusEstudioCerrado = "003";

        public const string ClaveEstatusInventarioFisicoCancelado = "003";

        public const string ClaveCitaLaboratorio = "002";

        public const string ClaveEstatusMovimientoSurtido = "002";
        public const string ClaveEstatusMovimientoCancelado = "003";

        public const string ClaveEstatusOrdenCompraPorSurtir = "001";
        public const string ClaveEstatusOrdenCompraCerrada = "002";

        public const string ClaveRolMedico = "002";
        public const string ClaveRolVendedor = "004";
        public const string ClaveRolCajero = "005";
        public const string ClaveRolProveedor = "006";
        public const string ClaveRolCliente = "007";
        public const string ClaveRolClienteLinea = "008";
        public const string ClaveRolGestorFlujos = "011";
        public const string ClaveRolPaciente = "014";
        public const string ClaveRolAsistente = "014";

        public const string RFCPublicoGeneral = "XAXX010101000";

        public const string ClaveRolAccesoATI = "001";
        public const string ClaveRolAccesoDistribucion = "002";
        public const string ClaveAccesoSistemaDistribucion = "APSDIT0000";

        public const string TablaUrgencia = "Urgencia";
        public const string TablaExpediente = "Expediente";
        public const string TablaCita = "Cita";

        public const string ConfiguracionRequierePagoCita = "EXMED001";


        public const string ClaveTipoRecibo = "001";
        public const string ClaveTipoReciboDevolucion = "002";
        public const string ClaveTipoReciboLiquidacion = "003";
        public const string ClaveTipoDevolucionLiquidacion = "004";

        public const string ClaveTipoPagoCobranza = "001";
        public const string ClaveTipoPagoDevolucion = "002";
        public const string ClaveTipoPagoNotaGasto = "003";

        public const string ClaveEstatusNotaVentaActiva = "001";
        public const string ClaveEstatusNotaVentaCancelada = "002";
        public const string ClaveTipoUsuarioMedicoExterno = "002";

        public const string ClaveTipoDescuentoSinDescuento = "001";
        public const string EstatusDescuentoActivo = "Activo";

        public const string ClaveTipoComisionSinComision = "001";

        public const string ClaveConceptoCuentaPorPagar = "003";
        public const string ClaveConceptoPagoDeComisiones = "004";
        public const string ClaveConceptoSueldoDiario = "005";
        public const string ClaveConceptoMovimientosBancarios = "009";
        public const string ClaveConceptoVentaNoFacturada = "010";
        public const string ClaveConceptoVentaFacturada = "011";
        public const string ClaveConceptoCuentaPorCobrar = "013";
        public const string ClaveConceptoIngreso = "014";
        public const string ClaveConceptoEgreso = "015";
        public const string ClaveConceptoEntradaPorCompraMaterial = "016";
        public const string ClaveConceptoCobranzaCliente = "017";
        public const string ClaveConceptoSalidaInventarioProceso = "019";

        public const string ClaveTipoExpedienteViaje = "002";

        public const string ClaveAgrupadorCuentaAtisc = "001";

        public const string ClaveImpuestoIVA16 = "001";

        public const string ClaveMonedaPesoMexicano = "MXN";

        public const string ComisionSueldoDiario = "Sueldo Diario";
        public const string ComisionVentas = "Ventas";


        public const string ClaveTipoVigenciaLicencia = "001";

        public static readonly string Enero = "Enero";
        public static readonly string Febrero = "Febrero";
        public static readonly string Marzo = "Marzo";
        public static readonly string Abril = "Abril";
        public static readonly string Mayo = "Mayo";
        public static readonly string Junio = "Junio";
        public static readonly string Julio = "Julio";
        public static readonly string Agosto = "Agosto";
        public static readonly string Septiembre = "Septiembre";
        public static readonly string Octubre = "Octubre";
        public static readonly string Noviembre = "Noviembre";
        public static readonly string Diciembre = "Diciembre";

        public const string OrigenFactura = "Factura";
        public const string OrigenNotaFlujo = "Nota de Flujo";
        public const string OrigenNotaGasto = "Nota de Gasto";
        public const string OrigenNotaVenta = "Nota de Venta";
        public const string OrigenMovimientoMaterial = "Movimiento Material";
        public const string OrigenLiquidacion = "Liquidación";
        public const string OrigenPedidoPresentacion = "Pedido Presentación";
        public const string OrigenCobranza = "Cobranza";

        // ===== Facturacion  ======= //

        public static readonly string CredencialesPruebasProfact = "mvpNUXmQfK8=";
        public static readonly string CredencialesPRODUCCIONProfact = "2TvNOqELjBdAiGE17ig/IA==|Aifa6011";

        public static readonly string ClaveSatTipoComprobanteIngreso = "I";
        public static readonly string ClaveSatTipoComprobanteTraslado = "T";

        public static readonly string ClaveSatTipoFactorTasa = "001";

        public static readonly string ClaveEstatusFacturaTimbrada = "001";
        public static readonly string ClaveEstatusFacturaCancelada = "002";
        public static readonly string ClaveEstatusFacturaDesconocido = "003";
        public static readonly string ClaveEstatusFacturaSinTimbrar = "004";

        // ===== Sistema de pedidos en linea ======= //
        public static readonly string OpenpayApiKey = "sk_e77bb9bd30fd4499a9baf362578b5ab1";
        public static readonly string OpenpayMerchantId = "myjxebm678t7m4reoiqq";

        public static readonly string ClaveEstatusPedidoEnProcesoLiberacion = "01";
        public static readonly string ClaveEstatusPedidoEnPreparacion = "02";
        public static readonly string ClaveEstatusPedidoRechazado = "07";
        public static readonly string ClaveEstatusPedidoTerminado = "08";

        public static readonly string ClaveMetodoPagoTransferencia = "003";
        public static readonly string ClaveMetodoPagoAlEntregar = "01";
        public static readonly string ClaveMetodoPagoTarjeta = "020";
        public static readonly string ClaveMetodoPagoPaypal = "040";

        public static readonly string ClaveOpcionVentaPedidosEnLinea = "001";
        public static readonly string ClaveOpcionVentaPuntoDeVenta = "002";

        public const string ClaveTipoFlujoDefault = "001";

        public const string ClaveAccionAceptarFlujo = "001";
        public const string ClaveAccionRechazarFlujo = "002";

        // ===== Sistema Distribución ======= //
        public static readonly string NombrePerfilAdministrador = "Administrador";
        public static readonly string ClaveTipoCompaniaDistribucion = "003";
        public static readonly string ClaveTipoCompaniaProyectos = "006";

        // ===== Facturacion ======= //
        public static readonly string RfcPublicoGeneral = "XAXX010101000";
        public static readonly string RFCAmbientePruebas = "IIA040805DZ4";
        public static readonly string ClaveUsoCFDIGastosGeneral = "G03";
        public static readonly string ClaveMetodoPagoUnaExhibicion = "PUE";
        public static readonly string ClaveMotivoCancelacionConRelacion = "01";

        // ===== Complemento Pago ======= //
        public static readonly string ClaveUsoCFDIPagos = "CP01";
        public static readonly string ClaveProductoServicioFacturacion = "84111506";
        public static readonly string ClaveUnidadActividad = "ACT";
        public static readonly string ClaveNingunaMoneda = "XXX";

        // ===== Gestión de Entidad Estructura ======= //
        public static readonly string ClaveEntidadPadecimiento = "003";

        // ===== Contabilidad ======= //
        public static readonly string MensajeErrorReferenciaCircular = "La fórmula contiene una referencia circular y no se puede calcular correctamente. "
        + "Las referencias circulares son las referencias incluidas en una fórmula que dependen de los resultados de esa misma fórmula.";

        public static readonly string DescriptionSeparator = " - ";

        public static readonly string Cargo = "Cargo";
        public static readonly string Abono = "Abono";

        public static readonly string TypeAuxiliaryCodeActivo = "01";
        public static readonly string TypeAuxiliaryCodePasivo = "02";
        public static readonly string TypeAuxiliaryCodeCapital = "03";
        public static readonly string TypeAuxiliaryCodeCentroCosto = "05";
        public static readonly string TypeAuxiliaryCodeAccount = "04";

        public static readonly string TypeAccountCodeBalance = "01";
        public static readonly string TypeAccountCodeResult = "02";
        public static readonly string TypeAccountCodeOrden = "03";

        public static readonly string PolicyTypeCodePresupuesto = "PRESUPUESTO";
        public static readonly string PolicyTypeCodeDiaria = "001";
        public static readonly string PolicyTypeCodeIngreso = "002";
        public static readonly string PolicyTypeCodeEgreso = "003";

        // Subtipos de cuentas
        // Nota: Estos codigos no deben de cambiarse, ya que el store procedure sp_BalanceAccount
        // utiliza estos codigos para realizar consultas de saldos. En caso que se requieran cambiar los codigos
        // se debera de actualizar el store procedure.
        public const string SubtypeAccountCodeActivo = "01";
        public const string SubtypeAccountCodePasivo = "02";
        public const string SubtypeAccountCodeCapital = "03";
        public const string SubtypeAccountCodeIngreso = "04";
        public const string SubtypeAccountCodeCosto = "05";
        public const string SubtypeAccountCodeGasto = "06";

        public static readonly string ConfiguracionMesContableActual = "CONTA001";
        public static readonly string ConfiguracionAnioContableActual = "CONTA002";
        public static readonly string ConfiguracionNumeroPolizaActual = "CONTA003";
        public static readonly string ConfiguracionMesContableAjuste = "CONTA004";
        public static readonly string ConfiguracionAnioContableAjuste = "CONTA005";
        public static readonly string ConfiguracionCuentaUtilidadFinanciera = "CONTA006";
        public static readonly string ConfiguracionContraCuentaUtilidadFinanciera = "CONTA007";
        public static readonly string ConfiguracionDescripcionMesContableActual = "Mes contable actual";
        public static readonly string ConfiguracionDescripcionAnioContableActual = "Año contable actual";
        public static readonly string ConfiguracionDescripcionMesContableAjuste = "Mes contable de ajuste";
        public static readonly string ConfiguracionDescripcionAnioContableAjuste = "Año contable de ajuste";
        public static readonly string ConfiguracionDescripcionNumeroPolizaActual = "Número actual de póliza";
        public static readonly string ConfiguracionDescripcionCuentaResultado = "Cuenta de resultado";
        public static readonly string ConfiguracionDescripcionCuentaCapital = "Contra de capital";

        public static readonly string ConfiguracionConsecutivoActualFoliosFacturas= "FACTU001";
        public static readonly string ConfiguracionConsecutivoActualExpedientesAdministrativos= "FACTU002";

        public const string AcumulaSaldoInicial = "saldoInicial";
        public const string AcumulaCargo = "cargo";
        public const string AcumulaAbono = "abono";
        public const string AcumulaSaldoFinal = "saldoFinal";

        public static readonly string RegexFormula = "[A-Z]+[0-9]+";

        // ===== SAT WebServices ======= //
        public static readonly string urlAutentica = "https://cfdidescargamasivasolicitud.clouda.sat.gob.mx/Autenticacion/Autenticacion.svc";
        public static readonly string urlAutenticaAction = "http://DescargaMasivaTerceros.gob.mx/IAutenticacion/Autentica";

        public static string urlSolicitud = "https://cfdidescargamasivasolicitud.clouda.sat.gob.mx/SolicitaDescargaService.svc";
        public static string urlSolicitudAction = "http://DescargaMasivaTerceros.sat.gob.mx/ISolicitaDescargaService/SolicitaDescarga";

        public static string urlVerificarSolicitud = "https://cfdidescargamasivasolicitud.clouda.sat.gob.mx/VerificaSolicitudDescargaService.svc";
        public static string urlVerificarSolicitudAction = "http://DescargaMasivaTerceros.sat.gob.mx/IVerificaSolicitudDescargaService/VerificaSolicitudDescarga";

        public static string urlDescargarSolicitud = "https://cfdidescargamasiva.clouda.sat.gob.mx/DescargaMasivaService.svc";
        public static string urlDescargarSolicitudAction = "http://DescargaMasivaTerceros.sat.gob.mx/IDescargaMasivaTercerosService/Descargar";

        public static readonly string urlDescargaMasivaTerceros = "http://DescargaMasivaTerceros.sat.gob.mx";

        public static string GetMonthName(string month)
        {
            return GetMonthName(int.Parse(month));
        }

        public static string GetMonthName(int month)
        {
            return month switch
            {
                1 => GeneralConstant.Enero,
                2 => GeneralConstant.Febrero,
                3 => GeneralConstant.Marzo,
                4 => GeneralConstant.Abril,
                5 => GeneralConstant.Mayo,
                6 => GeneralConstant.Junio,
                7 => GeneralConstant.Julio,
                8 => GeneralConstant.Agosto,
                9 => GeneralConstant.Septiembre,
                10 => GeneralConstant.Octubre,
                11 => GeneralConstant.Noviembre,
                12 => GeneralConstant.Diciembre,
                _ => "",
            };
        }

        // Origen de escenarios para errores
        public static readonly string OrigenEscenarioConfiguracionContable = "configuracion-contable";
        public static readonly string OrigenEscenarioSalidaPersonal = "salida-personal";

        // ===== Tipos Movimiento Bitacora Usuario ===== //
        public static readonly string TipoMovimientoUsuarioLogIn = "LogIn";
        public static readonly string TipoMovimientoUsuarioLogOut = "LogOut";
        public static readonly string TipoMovimientoUsuarioEdicion = "Edición";
        public static readonly string TipoMovimientoUsuarioEliminacion = "Eliminación";
        public static readonly string TipoMovimientoUsuarioEntradaPersonal = "Entrada Personal";
        public static readonly string TipoMovimientoUsuarioSalidaPersonal = "Salida Personal";
        public static readonly string[] WidgetsDefault = { "w-sue" , "w-pes" , "w-fre" , "w-pas" , "w-omr" };
        public static readonly string ClaveNotificacionRecordatorio = "GRL";
        public static readonly int IdTipoUsuarioChatAdmin = 1;
        public static readonly string ClaveTipoUsuarioChatAdmin = "001";
        public static readonly int idEstatusExamenTerminado = 3;
        public static readonly int idEstatusExamenPresentandose = 2;
        public static readonly int idEstatusExamenProgramado = 1;

        public const string SlackJsonMessageStart = @"{
                'blocks': [
                    {
                        'type': 'header',
                        'text': {
                            'type': 'plain_text',
                            'text': 'Nueva Excepcion en {0}',
                            'emoji': true
                        }
                    },
                    {
                        'type': 'section',
                        'fields': [
                            {
                                'type': 'mrkdwn',
                                'text': '*Cuando*: \n{1}'
                            },
                            {
                                'type': 'mrkdwn',
                                'text': '*Error:*\n {2}'
                            }
                        ]
                    },
                    {
                        'type': 'context',
                        'elements': [
                            {
                                'type': 'image',
                                'image_url': 'https://pbs.twimg.com/profile_images/625633822235693056/lNGUneLX_400x400.jpg',
                                'alt_text': 'cute cat'
                            },
                            {
                                'type': 'image',
                                'image_url': 'https://pbs.twimg.com/profile_images/625633822235693056/lNGUneLX_400x400.jpg',
                                'alt_text': 'cute cat'
                            },
                            {
                                'type': 'image',
                                'image_url': 'https://pbs.twimg.com/profile_images/625633822235693056/lNGUneLX_400x400.jpg',
                                'alt_text': 'cute cat'
                            },
                            {
                                'type': 'plain_text',
                                'text': '{3}',
                                'emoji': true
                            }
                        ]
                    },
                    {
                        'type': 'divider'
                    },
                    {
                        'type': 'section',
                        'text': {
                            'type': 'mrkdwn',
                            'text': '";

        public const string SlackJsonMessageEnd = @"'
                    }
                }
            ]
        }";


    }
}
