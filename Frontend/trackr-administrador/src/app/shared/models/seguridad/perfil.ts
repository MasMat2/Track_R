
export class Perfil {
  public idPerfil: number;
  public nombre: string;
  public clave: string;
  public idTipoCompania: number;
  public idJerarquiaAcceso?: number;

  public idsAcceso: number[];
  public nombreJerarquia: string;

  constructor() {}
}
