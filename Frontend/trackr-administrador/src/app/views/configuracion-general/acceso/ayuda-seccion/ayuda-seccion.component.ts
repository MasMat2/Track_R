import { Component, OnInit } from '@angular/core';
import { AccesoService } from '@http/seguridad/acceso.service';
import { AyudaSeccionService } from '@http/seguridad/ayuda-seccion.service';
import { AyudaSeccion } from '@models/seguridad/ayuda-seccion';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AyudaSeccionFormularioComponent } from './ayuda-seccion-formulario/ayuda-seccion-formulario.component';


@Component({
  selector: 'app-ayuda-seccion',
  templateUrl: './ayuda-seccion.component.html',
  styleUrls: ['./ayuda-seccion.component.scss']
})
export class AyudaSeccionComponent implements OnInit {

  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_AYUDA_SECCION;
  public accesoEliminar = CodigoAcceso.ELIMINAR_AYUDA_SECCION;
  public HEADER_GRID = 'Secciones de Ayuda';
  public MENSAJE_EXITO_ELIMINAR = 'La sección ayuda ha sido eliminada';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Sección Ayuda';
  public secciones: AyudaSeccion[] = [];

  public columns = [
    { headerName: 'Clave', field: 'clave', minWidth: 150 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 }
  ];

  constructor(private ayudaSeccionService: AyudaSeccionService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private modalMensajeService: MensajeService) { }

  ngOnInit() {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_AYUDA_SECCION).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });
    this.consultarGrid();
  }

  public consultarGrid(): void {
    this.ayudaSeccionService.consultarParaGrid().subscribe((data) => {
      this.secciones = data;
    })
  }

  public onGridClick(gridData: { accion: string; data: AyudaSeccion }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

  public agregar() {
    this.bsModalRef = this.modalService.show(
      AyudaSeccionFormularioComponent,
      GeneralConstant.CONFIG_MODAL_LARGE
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public editar(seccion: AyudaSeccion) {
    this.bsModalRef = this.modalService.show(
      AyudaSeccionFormularioComponent,
      GeneralConstant.CONFIG_MODAL_LARGE
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
    this.bsModalRef.content.seccion = seccion;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public eliminar(ayudaSeccion: AyudaSeccion): void {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar la sección ayuda <strong>' + ayudaSeccion.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar) => {
        this.ayudaSeccionService.eliminar(ayudaSeccion.idAyudaSeccion).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

}
