import { Usuario } from '@models/seguridad/usuario';
import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-busqueda-expediente',
  templateUrl: './busqueda-expediente.component.html',
  styleUrls: ['./busqueda-expediente.component.scss']
})
export class BusquedaExpedienteComponent implements OnInit {

  
  public elementos: Usuario[];

  public pageElements: Usuario[];
  public onClose: any;
  public seleccionado = 0;
  public currentPage = 1;
  public itemsPerPage = 10;


  constructor() { }

  ngOnInit() {
    this.pageElements = this.elementos.slice(0, this.itemsPerPage);
  }

  public pageChanged(event: any): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.pageElements = this.elementos.slice(startItem, endItem);
  }

  public seleccionar(id: number): void {
    let seleccionado = this.pageElements.find(element => element.idUsuario == id);
    this.onClose(seleccionado);
  }

  public cancelar(): void {
    this.onClose(null);
  }

}
