export class JerarquiaEstructuraArbol {
  public idJerarquia: number;
  public idJerarquiaEstructura: number;
  public idJerarquiaEstructuraPadre?: number;
  public cuenta: string;

  // Extra
  public hijos: JerarquiaEstructuraArbol[];
  public idAcceso: number;
  public tipoAcceso: string;

  constructor() {}
}
