import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataJitsiService {

  public room: string;
  public user: string;

  constructor() { }
}
