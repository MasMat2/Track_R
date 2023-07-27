export class CuentaContable {
    public idCuentaContable: number;
    public numero: string;
    public nombre: string;
    public descripcion: string;
    public reconciliatoria: boolean;
    public recibeMovimientos: boolean;
    public auxiliar: boolean;
    public partidaAbierta: boolean;
    public automatica: boolean;
    public idSubtipoCuentaContable: number;
    public idTipoCuentaContable: number;
    public idTipoAuxiliar: number;

    public numeroNombre: string;

    public esConcepto: boolean;

    public idCuentaContablePadre: number;
    public idAgrupadorCuentaContable: number;
    constructor() {}
  }
