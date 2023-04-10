import { NgForm } from '@angular/forms';
import * as moment from 'moment';

export function obtenerFechaActual(): Date {
  const fecha = new Date();
  fecha.setHours(0, 0, 0, 0);
  return fecha;
}

export function obtenerFechaAnterior(diasRestar: number): Date {
  const fecha = new Date();
  fecha.setDate(fecha.getDate() - diasRestar);
  fecha.setHours(0, 0, 0, 0);
  return fecha;
}

export function obtenerFechaMesYear(fecha: Date): string {
  let mes: string = "";

  const year: string = fecha.getFullYear().toString();
  switch (fecha.getMonth() + 1) {
    case 1: mes = 'Enero';
            break;
    case 2: mes = 'Febrero';
            break;
    case 3: mes = 'Marzo';
            break;
    case 4: mes = 'Abril';
            break;
    case 5: mes = 'Mayo';
            break;
    case 6: mes = 'Junio';
            break;
    case 7: mes = 'Julio';
            break;
    case 8: mes = 'Agosto';
            break;
    case 9: mes = 'Septiembre';
            break;
    case 10: mes = 'Octubre';
             break;
    case 11: mes = 'Noviembre';
             break;
    case 12: mes = 'Diciembre';
             break;
  }

  return mes + ' ' + year;
}

export function formatoMonto(monto: number | string): string {
  if (monto === null || monto === undefined) {
    return '';
  }

  const montoAux: number = +monto;

  return montoAux.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
}

export function validarCamposRequeridos(formulario: NgForm): void {
  Object.keys(formulario.controls).forEach((nombre) => {
    const control = formulario.controls[nombre];
    control.markAsTouched({ onlySelf: true });
    control.markAsDirty({ onlySelf: true });
  });
}

export function redondear(valor: number): number {
  return Math.round(valor * 100) / 100;
}

export function normalizarString(texto: string): string {
  return texto.normalize('NFD').replace(/[\u0300-\u036f]/g, '');
}

export function formatoFecha(fecha: Date): string {
  if (fecha) {
    return moment(fecha, 'YYYY-MM-DD').format('DD/MM/YYYY');
  }
  return '';
}

export function formatoHora(hora: number): string {
  if (hora) {
    return moment(hora).format('HH:mm');
  }
  return '';
}

export function formatoFechaHora(fecha: Date): string {
  if (fecha) {
    return moment(fecha).format('DD/MM/YYYY HH:mm');
  }
  return '';
}

export function isValidDate(d: number): boolean {
  return !isNaN(d);
}

export function sanitizarFecha(fecha: Date): Date {
  fecha.setHours(0, 0, 0, 0);
  return fecha;
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

export function calcularEdad(dob: Date): number | null {
  if (!dob) {
    return null;
  }

  const diffms = Date.now() - dob.getTime();
  const agedt = new Date(diffms);
  return Math.abs(agedt.getUTCFullYear() - 1970);
}

export function canDeactivateModal(): boolean {
  const submitValid = (sessionStorage.getItem('submitValid') === 'true');
  // const changed = (sessionStorage.getItem('changed') === 'true');
  const changed = false;

  if (!submitValid) {
    if (changed) {
      if (confirm('¿Quieres salir del sitio web?\nEs posible que los cambios no se guarden.')) {
        sessionStorage.removeItem('submitValid');
        sessionStorage.removeItem('changed');

        return true;
      } else {

        return false;
      }
    } else {
      sessionStorage.removeItem('submitValid');
      sessionStorage.removeItem('changed');

      return true;
    }
  } else {
    sessionStorage.removeItem('submitValid');
    sessionStorage.removeItem('changed');

    return true;
  }
}

export function toDateString(date: Date): string {
  return (date.getFullYear().toString() + '-'
    + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
    + ('0' + (date.getDate())).slice(-2))
    + 'T' + date.toTimeString().slice(0, 5);
}

export function esHoy(date: Date): boolean {
  const today = new Date();
  return date.getDate() == today.getDate() &&
    date.getMonth() == today.getMonth() &&
    date.getFullYear() == today.getFullYear();
}

export function obtenerFormatoNombreArchivo(nombreArchivo: string, extension: string): string {
  const hoy = new Date();
  return (hoy.getFullYear() + ('0' + (hoy.getMonth() + 1)).slice(-2)
    + ('0' + hoy.getDate()).slice(-2) + '_' + nombreArchivo + '.' + extension);
}

// Month in JavaScript is 0-indexed (January is 0, February is 1, etc),
// but by using 0 as the day it will give us the last day of the prior
// month. So passing in 1 as the month number will return the last day
// of January, not February
export function daysInMonth(month: number, year: number): number {
    return new Date(year, month + 1, 0).getDate();
}
