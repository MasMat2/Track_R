
export class Concepto {
    public idConcepto: number;
    public clave: string;
    public nombre: string;
    public idCompania: number;
    public tipoMovimiento: string;
    public idCuentaContable: number;
    public operativo: boolean;
    public idSatProductoServicio: number;
    public idSatUnidad: number;
    public idTipoAuxiliar: number;
    public idTipoConcepto: number;

    public cuentaContable: string;
    public idImpuesto: number;
    public nombreUnidadSat: string;
    public nombreProductoServicioSat: string;
    public numeroCuentaContable: string;
    public selectorLabel: string;
    public claveTipoCuentaContable: string;
    public requiereAuxiliar: boolean;
    public nombresTipoConcepto: string;
    public idsTipoConcepto: number[];

    constructor() {}
}
