import { ColDef } from "ag-grid-community";

export class GeneralConstant {

  public static TOKEN_KEY = 'token-administrador';
  public static CLAVE_USUARIO_ADMINISTRADOR = '001';
  public static CLAVE_USUARIO_PACIENTE = '002';

  public static USUARIO_MAESTRO_ATISC = 1;
  public static CLAVE_COMPANIA_BASE = "000";
  public static NOMBRE_ACCESO_SISTEMA_DISTRIBUCION = 'Sistema Distribución';

  public static CLAVE_TOKEN_ADMINISTRADOR: string = 'token-administrador';
  public static CLAVE_TOKEN_SISTEMA_PROYECTOS: string = 'token-sistema-proyectos';
  public static CLAVE_TOKEN_SISTEMA_DISTRIBUCION: string = 'token-sistema-distribucion';

  public static GRID_ACCION_EDITAR = 'edit';
  public static GRID_ACCION_ELIMINAR = 'delete';
  public static GRID_ACCION_COPIAR = 'copy';
  public static GRID_ACCION_VER = 'see'; //debe coincidir con GRID_ACTION.Ver
  public static GRID_ACTION_REVISAR = 'play';
  public static GRID_ACCION_AJUSTE = 'ajuste';
  public static GRID_ACCION_REPORTE_INVENTARIO_FISICO = 'reporteInventarioFisico';
  public static GRID_ACCION_RECETA = 'receta';
  public static GRID_ACCION_AGREGAR = 'add';
  public static GRID_ACCION_PAGAR = 'pago';
  public static GRID_ACCION_NOTA_RESPONSIVA = 'nota';
  public static GRID_ACCION_DESCUENTO = 'descuento';
  public static GRID_ACCION_ESTUDIO = 'estudio';
  public static GRID_ACCION_RECETASYORDENES = 'recetasOrdenes';
  public static GRID_ACCION_SOMATOMETRIA = 'somatometria';
  public static GRID_ACCION_VER_RECIBO = 'verRecibo';
  public static GRID_ACCION_SELECCIONAR = 'seleccionar';
  public static GRID_ACCION_IMPRIMIR = 'imprimir';
  public static GRID_ACCION_ORDEN_SALIDA = 'orden';
  public static GRID_ACCION_ORDEN_IMAGENOLOGIA = 'imagenologia';
  public static GRID_ACCION_URGENCIA_SERVICIOS_APLICADADOS = 'serviciosAplicados';
  public static GRID_ACCION_REGISTRAR_SALIDA_PERSONAL = 'registrarSalidaPersonal';
  public static GRID_ACCION_DESGLOSAR = 'desglosar';
  public static GRID_ACCION_REPORTE_COMISIONES = 'reporteComisiones';
  public static GRID_ACCION_DESCARGAR_DOCUMENTOS = 'descargarDocumentos';
  public static GRID_ACCION_TIMBRAR = 'timbrar';
  public static GRID_ACCION_VER_POLIZA = 'verPoliza';
  public static GRID_ACCION_CONCILIAR = 'conciliar';
  public static GRID_ACCION_COMPLEMENTO = 'complementoPago';
  public static GRID_ACCION_PDF_COMPLEMENTO = 'pdfComplemento';
  public static GRID_ACCION_DESCARGAR_PDF = 'descargarPdf';
  public static GRID_ACCION_DESCARGAR_EXCEL = 'descargarExcel';

  public static COMPONENT_ACCION_AGREGAR = 'Agregar';
  public static COMPONENT_ACCION_EDITAR = 'Editar';
  public static COMPONENT_ACCION_VER = 'Consultar';

  public static MODAL_ACCION_AGREGAR = 'Agregar';
  public static MODAL_ACCION_EDITAR = 'Editar';
  public static MODAL_ACCION_VER = 'Consulta';
  public static MODAL_ACCION_NUEVA = 'Nueva';

  public static PLACEHOLDER_DROPDOWN = 'Selecciona...';
  public static PLACEHOLDER_DROPDOWN_NO_OPTIONS = 'No se encontraron opciones';
  public static MENSAJE_ERROR_FECHA = 'La fecha inicial debe ser menor o igual a la fecha final.';
  public static MENSAJE_REQUERIDOS = 'Faltan campos requeridos por ingresar';
  public static MENSAJE_EXITO_BUSQUEDA = 'La búsqueda ha sido realizada';

  public static CLAVE_ESTATUS_FACTURA_TIMBRADA = '001';
  public static CLAVE_ESTATUS_FACTURA_CANCELADA = '002';
  public static CLAVE_ESTATUS_FACTURA_DESCONOCIDO = '003';
  public static CLAVE_ESTATUS_FACTURA_SIN_TIMBRAR = '004';

  public static CLAVE_ESTATUS_CITA_CANCELADA = '003';

  public static CLAVE_TIPO_USUARIO_INTERNO = '001';
  public static CLAVE_TIPO_USUARIO_EXTERNO = '002';
  public static CLAVE_TIPO_USUARIO_PACIENTE = '004';

  public static CLAVE_TIPO_ACCESO_MENU = '001';
  public static CLAVE_TIPO_ACCESO_EVENTO = '002';
  public static CLAVE_TIPO_ACCESO_SISTEMA = '003';
  public static CLAVE_TIPO_ACCESO_COMPONENTE = '004';

  public static CLAVE_ROL_ACCESO_DISTRIBUCION = '002';
  public static CLAVE_TIPO_COMPANIA_DISTRIBUCION = '003';

  public static CLAVE_TIPO_EXPEDIENTE_SOMATOMETRIA = '001';
  public static CLAVE_TIPO_EXPEDIENTE_CLINICO = '002';
  public static CLAVE_TIPO_EXPEDIENTE_URGENCIA = '003';
  public static CLAVE_TIPO_EXPEDIENTE_DIARIO_MEDICO = '004';
  public static CLAVE_URGENCIA = '003';

  public static CLAVE_MOTIVO_VISITA_OTRO = '003';

  public static CLAVE_TIPO_MOVIMIENTO_ENTRADA_COMPRA = '001';
  public static CLAVE_TIPO_MOVIMIENTO_OTRAS_ENTRADAS = '002';
  public static CLAVE_TIPO_MOVIMIENTO_OTRAS_SALIDAS = '004';
  public static CLAVE_TIPO_MOVIMIENTO_ENTRADA_DEVOLUCION = '011';
  public static CLAVE_TIPO_MOVIMIENTO_SALIDA_URGENCIAS = '014';
  public static CLAVE_TIPO_MOVIMIENTO_ENTRADA_POR_PRODUCCION = '015';

  public static CLAVE_ROL_ALMACENISTA = '001';
  public static CLAVE_ROL_MEDICO = '002';
  public static CLAVE_ROL_COMPRADOR = '003';
  public static CLAVE_ROL_VENDEDOR = '004';
  public static CLAVE_ROL_CAJERO = '005';
  public static CLAVE_ROL_PROVEEDOR = '006';
  public static CLAVE_ROL_CLIENTE = '007';
  public static CLAVE_ROL_CHOFER = '009';
  public static CLAVE_ROL_REPARTIDOR = '010';
  public static CLAVE_PERFIL_SIN_ACCESO = 'BASE004';

  public static CLAVE_ESTATUS_MOVIMIENTO_POR_SURTIR = '001';
  public static CLAVE_ESTATUS_MOVIMIENTO_CANCELADO = '003';

  public static CLAVE_ESTATUS_INVENTARIO_FISICO_ABIERTA = '001';
  public static CLAVE_ESTATUS_INVENTARIO_FISICO_CANCELADA = '003';

  public static CLAVE_ESTATUS_PAGO_NO_PAGADO = '001';
  public static CLAVE_ESTATUS_PAGO_PAGADO = '002';
  public static CLAVE_ESTATUS_PAGO_PARCIAL = '003';

  public static CLAVE_TIPO_PAGO_PAGO_NOTA_GASTO = '003';

  public static CLAVE_ESTATUS_NOTA_VENTA_CANCELADA = '002';

  public static CLAVE_ESTATUS_NOTA_GASTO_ACTIVA = '001';
  public static CLAVE_ESTATUS_NOTA_GASTO_CANCELADA = '002';
  public static CLAVE_ESTATUS_NOTA_GASTO_PENDIENTE = '003';
  public static CLAVE_ESTATUS_NOTA_GASTO_PAGADA = '004';

  public static CLAVE_ESTATUS_NOTA_FLUJO_ACTIVO = "001";
  public static CLAVE_ESTATUS_NOTA_FLUJO_APLICADO = "003";

  public static CLAVE_ESTATUS_ORDEN_COMPRA_CERRADA = "002";

  public static CLAVE_TIPO_NOTA_GASTO_PAGO_COMISION = '002';

  public static CLAVE_TIPO_RECIBO_DEVOLUCION = '002';
  public static CLAVE_TIPO_RECIBO_LIQUIDACION = '003';
  public static CLAVE_TIPO_RECIBO_DEVOLUCION_LIQUIDACION = '004';

  public static CLAVE_AREA_LABORATORIO = '002';
  public static CLAVE_AREA_IMAGENOLOGIA = '003';

  public static CLAVE_FORMA_PAGO_EFECTIVO = '006';
  public static CLAVE_FORMA_PAGO_TARJETA_DEBITO = '001';
  public static CLAVE_FORMA_PAGO_TARJETA_CREDITO = '002';
  public static CLAVE_FORMA_PAGO_TRANSFERENCIA_BANCARIA = '003';
  public static CLAVE_FORMA_PAGO_DEPOSITO_BANCO = '004';
  public static CLAVE_FORMA_PAGO_PAGO_TERCEROS = '005';
  public static CLAVE_FORMA_PAGO_TARJETA_DEBITO_VISA = '011';
  public static CLAVE_FORMA_PAGO_TARJETA_DEBITO_MASTERCARD = '012';
  public static CLAVE_FORMA_PAGO_DEBITO_CARNET = '013';
  public static CLAVE_FORMA_PAGO_TARJETA_CREDITO_VISA = '007';
  public static CLAVE_FORMA_PAGO_TARJETA_CREDITO_MASTERCARD = '008';
  public static CLAVE_FORMA_PAGO_CREDITO_CARNET = '009';
  public static CLAVE_FORMA_PAGO_TARJETA_CREDITO_AMERICANEXPRESS = '010';

  public static CLAVE_METODO_PAGO_CONTADO = '030';
  public static CLAVE_METODO_PAGO_CREDITO = '031';

  public static CLAVE_TIPO_COMPROBANTE_INGRESO = 'I';
  public static CLAVE_TIPO_COMPROBANTE_TRASLADO = 'T';

  public static CLAVE_PAIS_MEXICO = 'MEX';

  public static CLAVE_DEPARTAMENTO_URGENCIAS = '005';

  public static CLAVE_VIA_ADMINISTRACION_SIN_ESPECIFICAR = '000';

  public static CLAVE_TIPO_PUNTO_VENTA_DIRECTA = '001';

  public static CLAVE_CONCEPTO_CUENTA_POR_PAGAR = '003';
  public static CLAVE_CONCEPTO_INGRESO = '014';
  public static CLAVE_CONCEPTO_EGRESO = '015';

  public static CLAVE_TIPO_CONCEPTO_ALMACEN = '003';

  public static CLAVE_ESTATUS_PEDIDO_ESPERA_PAGO = '01';
  public static CLAVE_ESTATUS_PEDIDO_PAGO_REGISTRADO = '04';
  public static CLAVE_ESTATUS_PEDIDO_RECHAZADO = '07';
  public static CLAVE_ESTATUS_PEDIDO_TERMINADO = '08';

  public static CLAVE_OPCION_VENTA_PEDIDOS_EN_LINEA = '001';

  public static CLAVE_ACCION_ACEPTAR_FLUJO = '001';
  public static CLAVE_ACCION_RECHAZAR_FLUJO = '002';

  public static CLAVE_TIPO_EXPEDIENTE_ADMINISTRATIVO_VIAJE = '002';

  public static ICONO_CRUZ = 'fa fa-times';
  public static ICONO_PESOS = 'fa fa-dollar-sign';
  public static ICONO_CAMBIO = 'fas fa-exchange-alt';
  public static ICONO_CHECK_OUT = 'fas fa-door-open';
  public static ICONO_EXITO = 'fas fa-check';

  public static PRECISION_PORCENTAJE = 4;

  public static TABLA_EXPEDIENTE = 'Expediente';
  public static TABLA_URGENCIA = 'Urgencia';
  public static TABLA_CITA = 'Cita';

  public static CLAVE_TIPO_AUXILIAR_CUENTA = '04';

  public static RFC_PUBLICO_GENERAL = 'XAXX010101000';

  public static CLAVE_TIPO_ACTIVO_CHEQUERA = '001';

  public static POLIZA_ORIGEN_FACTURA = "Factura";
  public static POLIZA_ORIGEN_NOTA_FLUJO = "Nota de Flujo";
  public static POLIZA_ORIGEN_NOTA_GASTO = "Nota de Gasto";
  public static POLIZA_ORIGEN_NOTA_VENTA = "Nota de Venta";
  public static POLIZA_ORIGEN_MOVIMIENTO_MATERIAL = "Movimiento Material";
  public static ORIGEN_PEDIDO_PRESENTACION = "Pedido Presentación";

  // Claves de Campos
  public static CAMPO_URGENCIA_EDAD = 'URG00005';
  public static CAMPO_URGENCIA_FECHA_NACIMIENTO = 'URG00004';
  public static CAMPO_URGENCIA_HORA_EGRESO = 'URG00022';

  public static CAMPO_SOMATOMETRIA_PESO = 'SOM00001';
  public static CAMPO_SOMATOMETRIA_TALLA = 'SOM00002';
  public static CAMPO_SOMATOMETRIA_IMC = 'SOM00004';
  public static CAMPO_SOMATOMETRIA_TIEMPO = 'SOM00006';
  public static CAMPO_SOMATOMETRIA_TA_SISTOLICA = 'SOM00007';
  public static CAMPO_SOMATOMETRIA_TA_DIASTOLICA = 'SOM00008';
  public static CAMPO_SOMATOMETRIA_FC_PULSO_POR_MINUTO = 'SOM00009';
  public static CAMPO_SOMATOMETRIA_FR_POR_MINUTO = 'SOM00010';
  public static CAMPO_SOMATOMETRIA_TEMPERATURA = 'SOM00011';
  public static CAMPO_SOMATOMETRIA_GLUCEMIA_CAPILAR = 'SOM00013';
  public static CAMPO_SOMATOMETRIA_INTENSIDAD = 'SOM00015';
  public static CAMPO_SOMATOMETRIA_NIVEL_DOLOR = 'SOM00019';
  public static CAMPO_SOMATOMETRIA_SP02 = 'SOM00020';
  public static CAMPO_SOMATOMETRIA_NIVEL_OXIGENO = 'SOM00021';
  public static CAMPO_EXPEDIENTE_ACTIVIDAD_FISICA = 'ATC00023';
  public static CAMPO_EXPEDIENTE_ACTIVIDAD_FISICA_DESCRIPCION = 'ATC00024';
  public static CAMPO_EXPEDIENTE_ACTIVIDAD_OCIO = 'ATC00025';
  public static CAMPO_EXPEDIENTE_ACTIVIDAD_OCIO_DESCRIPCION = 'ATC00026';
  public static CAMPO_EXPEDIENTE_INDICE_MASA_CORPORAL = 'ATC00141';
  public static CAMPO_EXPEDIENTE_INDICE_PESO = 'ATC00142';
  public static CAMPO_EXPEDIENTE_APLICA_LABORATORIO_GABINETE = 'ATC00143';
  public static CAMPO_EXPEDIENTE_LABORATORIO_GABINETE = 'ATC00144';
  public static CAMPO_EXPEDIENTE_APLICA_PLAN_DIAGNOSTICO = 'ATC00146';
  public static CAMPO_EXPEDIENTE_PLAN_DIAGNOSTICO = 'ATC00147';
  public static CAMPO_EXPEDIENTE_BENEFICIARIO_PROGRAMA = 'DAD00005';
  public static CAMPO_EXPEDIENTE_BENEFICIARIO_PROGRAMA_ESPECIFICAR = 'DAD00006';
  public static CAMPO_EXPEDIENTE_DIALECTO_LENGUA_INDIGENA = 'DAD00008';
  public static CAMPO_EXPEDIENTE_DIALECTO_LENGUA_INDIGENA_ESPECIFICAR = 'DAD00009';
  public static CAMPO_EXPEDIENTE_EXAMEN_VPH = 'ATC00066';
  public static CAMPO_EXPEDIENTE_EXAMEN_VPH_FECHA = 'ATC00067';
  public static CAMPO_EXPEDIENTE_EXAMEN_VPH_RESULTADO = 'ATC00068';
  public static CAMPO_EXPEDIENTE_MAMOGRAFIA = 'ATC00069';
  public static CAMPO_EXPEDIENTE_MAMOGRAFIA_FECHA = 'ATC00070';
  public static CAMPO_EXPEDIENTE_MAMOGRAFIA_REPORTE = 'ATC00071';
  public static CAMPO_EXPEDIENTE_DISCAPACIDAD = 'DAD00010';
  public static CAMPO_EXPEDIENTE_ESPECIFICAR_DISCAPACIDAD = 'DAD00011';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_PESO = 'NDR00012';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_TALLA = 'NDR00013';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_FC_PULSO_MINUTO = 'NDR00014';
  public static CAMPO_SOMATOMETRIA_FC_PULSO_MINUTO = 'SOM00009';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_TEMP = 'NDR00016';
  public static CAMPO_SOMATOMETRIA_TEMP = 'SOM00011';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_TA_SISTOLICA = 'NDR00023';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_TA_DIASTOLICA = 'NDR00024';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_FR_MINUTO = 'NDR00025';
  public static CAMPO_SOMATOMETRIA_FR_MINUTO = 'SOM00010';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_FECHA = 'NDR00002';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_NOMBRE = 'NDR00007';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_EDAD = 'NDR00008';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_PACIENTE = 'NDR00010';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_SEXO = 'NDR00009';
  public static CAMPO_DIARIO_MEDICO_NOTA_REFERENCIA_NOMBRE_MEDICO = 'NDR00022';

  // Claves de Secciones

  // Diario Médico
  public static DIARIO_MEDICO = 'DMO00000';
  public static SECCION_DIARIO_MEDICO_LINEA_DE_VIDA = 'LDV00000';
  public static SECCION_DIARIO_MEDICO_NOTAS_DE_EVOLUCION = 'NDE00000';
  public static SECCION_DIARIO_MEDICO_NOTAS_DE_REFERENCIA = 'NDR00000';
  public static SECCION_DIARIO_MEDICO_SOMATOMETRIA = 'SMM00000';
  public static SECCION_DIARIO_MEDICO_ORDENES_DE_LABORATORIO = 'ODL00000';
  public static SECCION_DIARIO_MEDICO_ORDENES_DE_IMAGENOLOGIA = 'ODI00000';

  // Expediente Clínico
  public static SECCION_EXPEDIENTE_CLINICO_DATOS_ADICIONALES = 'DTA00000';
  public static SECCION_EXPEDIENTE_CLINICO_ANTECEDENTES_CLINICO = 'ATC00000';
  public static SECCION_EXPEDIENTE_CLINICO_DATOS_SOCIALES = 'DTS00000';
  public static SECCION_EXPEDIENTE_CLINICO_ANTECEDENTES_H_F = 'AHF00000';
  public static SECCION_EXPEDIENTE_CLINICO_APP = 'APP00000';
  public static SECCION_EXPEDIENTE_CLINICO_APNP = 'ANP00000';

  // Bitacora
  public static BITACORA = 'BIT00000';

  public static SECCION_RECETAS = 'REC00000';

  // Configuracion Contabilidad
  public static ConfiguracionMesContableActual = "CONTA001";
  public static ConfiguracionAnioContableActual = "CONTA002";
  public static ConfiguracionNumeroPolizaActual = "CONTA003";
  public static ConfiguracionMesContableAjuste = "CONTA004";
  public static ConfiguracionAnioContableAjuste = "CONTA005";
  public static ConfiguracionCuentaUtilidadFinanciera = "CONTA006";
  public static ConfiguracionContraCuentaUtilidadFinanciera = "CONTA007";

  public static ClaveTipoCuentaContableBalance = "01";

  // Configuracion Egresos
  public static ConfiguracionConsecutivoActualFoliosFacturas = "FACTU001"
  public static ConfiguracionConsecutivoActualExpedientesAdministrativos = "FACTU002"

  // Configuracion Gestion Paciente - Exp. Medico
  public static ConfiguracionRequerirPagoCitas = "EXMED001";

  // Gestión de Entidades
  public static ClaveEntidadExpedienteAdministrativo = "001";
  public static ClaveEntidadExpedienteTrackr = "002";

  public static CONFIG_DATEPICKER: any = {
    dateInputFormat: 'DD/MM/YYYY',
    showWeekNumbers: false,
    selectFromOtherMonth: true,
    isAnimated: true,
    todayHighlight: true,
    dropdownParent: 'body'
  };

  public static CONFIG_DATEPICKER_WEEKS: any = {
    dateInputFormat: 'DD/MM/YYYY',
    showWeekNumbers: true,
    selectFromOtherMonth: true,
    isAnimated: true,
    todayHighlight: true
  };

  public static CONFIG_DATEPICKER_MONTH: any = {
    dateInputFormat: 'MMMM',
    showWeekNumbers: false,
    selectFromOtherMonth: true,
    isAnimated: true,
    todayHighlight: true,
    minMode : 'month'
  };

  public static CONFIG_DATEPICKER_YEAR: any = {
    dateInputFormat: 'YYYY',
    showWeekNumbers: false,
    selectFromOtherMonth: true,
    isAnimated: true,
    todayHighlight: true,
    minMode : 'year'
  };

  public static CONFIG_DROPDOWN_DEFAULT: any = {
    dropdownDirection: 'down',
    plugins: {
      remove_button: { title: 'Eliminar' }
    },
    mode: 'multi',
    maxItems: 1
  };

  public static CONFIG_MODAL_DEFAULT: any = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    size: 'md',
    class: 'modal-md modal-size-md modal-position-center'
  };

  public static CONFIG_MODAL_MEDIUM: any = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    size: 'md',
    class: 'modal-medium modal-size-md modal-position-center'
  };

  public static CONFIG_MODAL_SMALL: any = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    size: 'md',
    class: 'modal-xs modal-size-xs modal-position-center'
  };

  public static CONFIG_MODAL_FULL: any = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    size: 'sm',
    class: 'modal-xxlg modal-size-lg modal-position-center'
  };

  public static CONFIG_MODAL_LARGE: any = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    size: 'sm',
    class: 'modal-xlg modal-size-md modal-position-center dual-modal'
  };

  public static CONFIG_DATEPICKER_DROPDOWN = {
    singleDatePicker: true,
    parentEl: 'body',
    autoUpdateInput: false,
    showDropdowns: true,
    locale: { format: 'dd/MM/yyyy' }
  };

  public static CONFIG_COLUMN_ACTION: ColDef = {
    headerName: '',
    field: '',
    cellClass: 'center-ag',
    minWidth: 44,
    maxWidth: 44,
    suppressMovable: true,
    resizable: false,
    filter: false,
    lockPinned: true,
    pinned: 'right',
    lockPosition: true
  };

  public static MONTHS = [
    { "nombreMes" : "Enero", "valor" : 1},
    { "nombreMes" : "Febrero", "valor" : 2},
    { "nombreMes" : "Marzo", "valor" : 3},
    { "nombreMes" : "Abril", "valor" : 4},
    { "nombreMes" : "Mayo", "valor" : 5},
    { "nombreMes" : "Junio", "valor" : 6},
    { "nombreMes" : "Julio", "valor" : 7},
    { "nombreMes" : "Agosto", "valor" : 8},
    { "nombreMes" : "Septiembre", "valor" : 9},
    { "nombreMes" : "Octubre", "valor" : 10},
    { "nombreMes" : "Noviembre", "valor" : 11},
    { "nombreMes" : "Diciembre", "valor" : 12},
  ]

  public static ORIGEN_ESCENARIO_CONFIGURACION_CONTABLE = "configuracion-contable";
  public static ORIGEN_ESCENARIO_SALIDA_PERSONAL = "salida-personal";

  public static  TIPO_NOTIFICACION = {
    General: 'GRL',
    Chat: 'CHT',
    Video: 'VID',
    Alerta: 'ALT',
    Recomendacion: 'RCM',
    Eecordatorio: 'RCD'
  };

}
