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

  public fechaLocalAFechaUTC(fechaLocal: string | Date){
    if(fechaLocal instanceof Date){
      return fechaLocal.toISOString().slice(0,-1);
    }
    else if(typeof fechaLocal == "string"){
      if(fechaLocal.endsWith('Z')){
        fechaLocal = fechaLocal.slice(0, -1);
      }
      return new Date(fechaLocal).toISOString().slice(0,-1);
    }
    else{
      throw new Error("Tipo no válido.");
    }
  }

  public fechaUTCAFechaLocal(fechaUTC: string | Date){
    if(fechaUTC instanceof Date){
      const localISOTime = (new Date(fechaUTC.getTime() - 2*(this.localOffset))).toISOString().slice(0,-1);
      return localISOTime;
    }
    else if(typeof fechaUTC == "string"){
      if(fechaUTC.endsWith('Z')){
        fechaUTC = fechaUTC.slice(0, -1);
      }
      const fecha = new Date(fechaUTC);
      const localISOTime = (new Date(fecha.getTime() - 2*(this.localOffset))).toISOString().slice(0,-1);
      return localISOTime;
    }
    else{
      throw new Error("Tipo no válido.");
    }
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
