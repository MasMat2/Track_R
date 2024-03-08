import { Component, OnInit } from '@angular/core';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';

@Component({
  selector: 'app-seccion-campo-modal',
  templateUrl: './seccion-campo-modal.component.html',
  styleUrls: ['./seccion-campo-modal.component.scss']
})
export class SeccionCampoModalComponent implements OnInit {
  // Variables de entrada
  public idSeccion: number;

  public onClose: () => void;

  // Grid Campos
  public HEADER_GRID_CAMPOS = 'Campos';
  public camposList: SeccionCampo[];
  public columnsCampos = [
    { headerName: 'Clave', field: 'clave', minWidth: 30 },
    { headerName: 'Descripción', field: 'descripcion', minWidth: 30 },
    { headerName: 'Dominio', field: 'nombreDominio', minWidth: 30 },
    { headerName: 'Orden', field: 'orden', minWidth: 30 },
    { headerName: 'Requerido', field: 'requerido', minWidth: 30, valueGetter: (params: any) => this.convertirSiNo(params.data)}
  ];

  constructor(
    private seccionCampoService: SeccionCampoService,
  ) { }

  public ngOnInit(): void {
    this.consultarGridCampos();
  }

  private consultarGridCampos(): void {
    this.seccionCampoService.consultarPorSeccion(this.idSeccion)
      .subscribe((data) => {
        this.camposList = data;
      });
  }

  public cerrar() {
    this.onClose();
  }

  private convertirSiNo(valor: SeccionCampo): string {
    return valor.requerido ? 'Sí' : 'No';
  }

}
