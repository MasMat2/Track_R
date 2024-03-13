export class AccesoAyuda {
  public idAccesoAyuda: number;
  public idAcceso: number;
  public etiquetaCampo: string;
  public descripcionAyuda: string;
  public orden: number;

  public imagen?: string;
  public tipoMime: string;

  // Extras para imagen
  public nombreArchivo: string;
  public imagenBase64: string;

  public idAyudaSeccion: number;
  public nombreAyudaSeccion: string;
  constructor() {}
}

export class AccesoAyudaImagenBase64{
  public idAccesoAyuda: number;
  public idAcceso: number;
  public etiquetaCampo: string;
  public descripcionAyuda: string;
  public orden: number;

  public imagen: string;
  public imagenBase64: string;
  public tipoMime: string;
  public nombreArchivo: string;

  constructor() {}
}

