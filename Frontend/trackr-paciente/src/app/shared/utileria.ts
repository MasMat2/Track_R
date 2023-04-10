import { NgForm } from "@angular/forms";
import * as moment from 'moment';


export function validarCamposRequeridos(formulario: NgForm): void {
    Object.keys(formulario.controls).forEach((nombre) => {
      const control = formulario.controls[nombre];
      control.markAsTouched({ onlySelf: true });
      control.markAsDirty({ onlySelf: true });
    });
  }

export function formatoMonto(monto: number): string {
    if (monto === null || monto === undefined) {
      return '';
    }
  
    return monto.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
}

 export function formatoFecha(fecha: Date): string {
   if (fecha) {
     return moment(fecha, 'YYYY-MM-DD').format('DD/MM/YYYY');
   }
  return '';
 }