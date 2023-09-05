import { SeccionMuestraDTO } from "./seccion-muestra-dto";

export class PadecimientoMuestraDTO{
  idPadecimiento: number;
  nombrePadecimiento: string;
  seccionMuestraDTOs: SeccionMuestraDTO[];
}
