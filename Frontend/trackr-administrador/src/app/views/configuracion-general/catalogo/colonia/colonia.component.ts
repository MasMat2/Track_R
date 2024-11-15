import { Component, OnInit } from '@angular/core';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Colonia } from '@models/catalogo/colonia';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ColoniaFormularioComponent } from './colonia-formulario/colonia-formulario.component';
import { LoadingSpinnerService } from '@services/loading-spinner.service';

@Component({
  selector: 'app-colonia',
  templateUrl: './colonia.component.html',
  styleUrls: ['./colonia.component.scss']
})
export class ColoniaComponent implements OnInit {

  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_COLONIA;
  public accesoEliminar = CodigoAcceso.ELIMINAR_COLONIA;
  public HEADER_GRID = 'Colonias';
  public MENSAJE_EXITO_ELIMINAR = 'La colonia ha sido eliminada';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Colonia';
  public coloniaList: Colonia[];
  public cargandoGrid = false;
  public loading: boolean = false;

  public columns = [
    { headerName: 'Clave', field: 'clave', minWidth: 150, width: 70 },
    { headerName: 'Codigo Postal', field: 'codigoPostal', minWidth: 150 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
  ];

  constructor(private accesoService: AccesoService,
    private coloniaService: ColoniaService,
    private mensajeService: MensajeService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private loadingSpinnerService : LoadingSpinnerService) {}
    

  ngOnInit() {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_COLONIA).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });
    this.consultarGrid();
  }

  public consultarGrid(){
    this.cargandoGrid = true;
    this.loading = true;
    this.coloniaService.consultarParaGrid().subscribe((data)=>{
      this.coloniaList = data;
      this.cargandoGrid = false;
      this.loading = false;
    })
  }

  public onGridClick(gridData: { accion: string; data: Colonia }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
      return;
    }
  }

  public agregar(): void{
    this.bsModalRef = this.modalService.show(
      ColoniaFormularioComponent,
      GeneralConstant.CONFIG_MODAL_DEFAULT
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public editar(colonia: Colonia): void{
    this.bsModalRef = this.modalService.show(
      ColoniaFormularioComponent,
      GeneralConstant.CONFIG_MODAL_DEFAULT
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
    this.bsModalRef.content.colonia = colonia;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public eliminar(colonia: Colonia): void{
    this.mensajeService
    .modalConfirmacion(
      '¿Desea eliminar la colonia <strong>' + colonia.nombre + '</strong>?',
      this.TITULO_MODAL_ELIMINAR

    )
    .then((aceptar) => {
      this.coloniaService.eliminar(colonia.idColonia).subscribe((data) => {
        this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
        this.consultarGrid();
      });
    });
  }

  sincronizarPlantilla(){
    this.loadingSpinnerService.openSpinner();
    this.coloniaService.actualizarPlantillaExcel().subscribe({
      next: () => {
        this.loadingSpinnerService.closeSpinner();
        this.mensajeService.modalExito('Plantilla actualizada exitosamente');
      },
      error: (error) => {
        this.loadingSpinnerService.closeSpinner();
        this.mensajeService.modalError('Error al actualizar la plantilla');
      }
    });
  }

}
