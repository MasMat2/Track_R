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

 export function diferenciaFechas(dt1: Date, dt2: Date): any {
  const ret = { days: 0, months: 0, years: 0 };

  if (dt1 === dt2) { return ret; }

  if (dt1 > dt2) {
    const dtmp = dt2;
    dt2 = dt1;
    dt1 = dtmp;
  }

  const year1 = dt1.getFullYear();
  const year2 = dt2.getFullYear();

  const month1 = dt1.getMonth();
  const month2 = dt2.getMonth();

  const day1 = dt1.getDate();
  const day2 = dt2.getDate();

  ret.years = year2 - year1;
  ret.months = month2 - month1;
  ret.days = day2 - day1;

  if (ret.days < 0) {
    const dtmp1 = new Date(dt1.getFullYear(), dt1.getMonth() + 1, 1, 0, 0, -1);

    const numDays = dtmp1.getDate();

    ret.months -= 1;
    ret.days += numDays;

  }

  if (ret.months < 0) {
    ret.months += 12;
    ret.years -= 1;
  }

  return ret;
}


export function normalizarString(texto: string): string {
  return texto.normalize('NFD').replace(/[\u0300-\u036f]/g, '');
}

export function equalsNormalized(a: string, b: string): boolean {
  return normalizarString(a.toLowerCase()) === normalizarString(b.toLowerCase());
}