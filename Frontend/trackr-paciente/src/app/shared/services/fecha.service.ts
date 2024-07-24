import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FechaService {

  protected readonly now = new Date();
  protected readonly localOffset = this.now.getTimezoneOffset() * 60000;
  protected readonly localISOTime = (new Date(this.now.getTime() - this.localOffset)).toISOString().slice(0,-1);
  protected readonly localDateTime = (new Date(this.now.getTime() - this.localOffset));

  constructor() { 

  }

  public obtenerFechaActualDate(){
    return this.localDateTime;
  }

  public obtenerFechaActualISOString(){
    return this.localISOTime;
  }

  public fechaLocalAFechaUTC(fechaLocal: string){
    return new Date(fechaLocal).toISOString().slice(0,-1);
  }

  public fechaUTCAFechaLocal(fechaUTC: string){
    const fecha = new Date(fechaUTC);
    const localISOTime = (new Date(fecha.getTime() - 2*(this.localOffset))).toISOString().slice(0,-1);
    return localISOTime;
  }

  public horaUTCAHoraLocal(hora: string){
    const dateTodayString = this.localISOTime.split('T')[0];
    const fecha = new Date(`${dateTodayString}T${hora}`);
    const localISOTime = (new Date(fecha.getTime() - 2*(this.localOffset))).toISOString().slice(0,-1);

    return localISOTime.split('T')[1].split('.')[0];
  }

  public horaLocalAHoraUTC(hora: string){
    const dateTodayString = this.localISOTime.split('T')[0];
    const fecha = new Date(`${dateTodayString}T${hora}`);

    return fecha.toISOString().split('T')[1].split('.')[0];
  }
}
