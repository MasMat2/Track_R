import { Component, OnInit } from '@angular/core';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { SeccionService } from '@http/gestion-entidad/seccion.service';
import { Seccion } from '@models/gestion-entidad/seccion';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ConfiguracionSeccionesFormularioComponent } from './configuracion-secciones-formulario/configuracion-secciones-formulario.component';

@Component({
  selector: 'app-configuracion-secciones',
  templateUrl: './configuracion-secciones.component.html',
  styleUrls: ['./configuracion-secciones.component.scss']
})
export class ConfiguracionSeccionesComponent implements OnInit {

  private MENSAJE_EXITO_ELIMINAR = 'La sección ha sido eliminada';
  private TITULO_MODAL_ELIMINAR = 'Eliminar Sección';

  // Grid secciones
  public HEADER_GRID_SECCIONES = 'Secciones';
  public seccionesList: Seccion[] = [];

  public columnsSeccion = [
    { headerName: 'Clave', field: 'clave', minWidth: 50 },
    { headerName: 'Parámetro', field: 'nombre', minWidth: 150 }
  ];

  constructor(
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private seccionService: SeccionService,
    private mensajeService: MensajeService,
    private seccionCampoService: SeccionCampoService
  ) { }

  public ngOnInit(): void {
    this.consultarSecciones();
  }

  public onGridClickSeccion(gridData: { accion: string; data: Seccion }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.edicionSeccion(gridData.data.idSeccion);
    }
    else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminarSeccion(gridData.data);
    }
  }

  public consultarSecciones(): void{
    this.seccionService.consultarGeneral().subscribe((data) => {
      this.seccionesList = data;
    });
  }

  public agregarSeccion(): void {
    const initialState = {
      accion: GeneralConstant.MODAL_ACCION_AGREGAR,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarSecciones();
        }
        this.bsModalRef.hide();
      }
    };

    this.bsModalRef = this.modalService.show(
      ConfiguracionSeccionesFormularioComponent,
      {
        initialState, ... GeneralConstant.CONFIG_MODAL_MEDIUM
      }
    );
  }

  public async edicionSeccion(idSeccion: number) : Promise<void> {
    const seccion = await this.seccionService.consultar(idSeccion)
    .toPromise()
    .catch(() => null);

    const campos = await this.seccionCampoService.consultarPorSeccion(idSeccion)
    .toPromise()
    .catch(() => []);

    const initialState = {
      accion: GeneralConstant.MODAL_ACCION_EDITAR,
      seccion: seccion,
      camposList: campos,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarSecciones();
        }
        this.bsModalRef.hide();
      }
    };

    this.bsModalRef = this.modalService.show(
      ConfiguracionSeccionesFormularioComponent,
      {
        initialState, ... GeneralConstant.CONFIG_MODAL_LARGE
      }
    );
  }

  public eliminarSeccion(seccion: Seccion): void {
    this.mensajeService
    .modalConfirmacion(
      '¿Desea eliminar la sección <strong>' + seccion.clave + " - " + seccion.nombre + '</strong>?',
      this.TITULO_MODAL_ELIMINAR
    )
    .then((aceptar) => {
      this.seccionService.eliminar(seccion.idSeccion).subscribe(() => {
        this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
        this.consultarSecciones();
      });
    });
  }
}
