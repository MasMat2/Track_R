import { Component, OnInit } from '@angular/core';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { CodigoPostal } from '@models/catalogo/codigo-postal';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CodigoPostalFormularioComponent } from './codigo-postal-formulario/codigo-postal-formulario.component';
import { ArchivoService } from '@http/catalogo/archivo.service';
import { LoadingSpinnerService } from '@services/loading-spinner.service';
import { ArchivoCarga } from '@dtos/archivos/archivo-carga';
import { tap } from 'rxjs';

@Component({
  selector: 'app-codigo-postal',
  templateUrl: './codigo-postal.component.html'
})
export class CodigoPostalComponent implements OnInit {

  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_CODIGO_POSTAL;
  public accesoEliminar = CodigoAcceso.ELIMINAR_CODIGO_POSTAL;
  public cargandoGrid = false;

  public HEADER_GRID = 'Código Postal';
  public MENSAJE_EXITO_ELIMINAR = 'El código postal ha sido eliminado';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Código Postal';
  public CodigoPostalList: CodigoPostal[];
  public columns = [

    { headerName: 'Código Postal', field: 'codigoPostal1', minWidth: 150 },
    { headerName: 'Colonia', field: 'colonia', minWidth: 150 },
    { headerName: 'Municipio', field: 'municipio', minWidth: 150 },
    { headerName: 'Estado', field: 'estado', minWidth: 150 },
  ];

  constructor(
    private mensajeService: MensajeService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private codigoPostalService: CodigoPostalService,
    private archivoService : ArchivoService,
    private loadingSpinnerService: LoadingSpinnerService

  ) { }

  ngOnInit() {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_CODIGO_POSTAL).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });

    this.consultarGrid();
  }
  public consultarGrid(): void {
    this.cargandoGrid = true;
    this.codigoPostalService.consultarTodosParaGrid().subscribe((data) => {
      this.CodigoPostalList = data;
    });
  }

  public onGridClick(gridData: { accion: string; data: CodigoPostal }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idCodigoPostal);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
      return;
    }
  }

  public agregar(): void {
    this.bsModalRef = this.modalService.show(
      CodigoPostalFormularioComponent,
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

  public editar(idCodigoPostal: number): void {
    this.codigoPostalService.consultar(idCodigoPostal).subscribe((data) => {
      this.bsModalRef = this.modalService.show(
        CodigoPostalFormularioComponent,
        GeneralConstant.CONFIG_MODAL_DEFAULT
      );
      this.bsModalRef.content.codigoPostal = data;
      this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      };
    });
  }

  public eliminar(codigoPostal: CodigoPostal): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el código postal <strong>' + codigoPostal.codigoPostal1 + '</strong>?',
        this.TITULO_MODAL_ELIMINAR

      )
      .then((aceptar) => {
        this.codigoPostalService.eliminar(codigoPostal.idCodigoPostal).subscribe((data) => {
          this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

  protected sincronizarPlantilla(): void {
    this.loadingSpinnerService.openSpinner();
    this.codigoPostalService.sincronizarEstadosExcel().subscribe({
      next: () => {
        this.mensajeService.modalExito('Códigos postales sincronizados correctamente');
        this.consultarGrid();
        this.loadingSpinnerService.closeSpinner();
      },
      error: (err) => {
        console.error('Error al sincronizar los códigos postales:', err);
        this.loadingSpinnerService.closeSpinner();
      },
    });
  }

  protected subirPlantilla(){
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = '.xlsx';

    input.onchange = (event: Event) => {
      if (event && event.target && (event.target as HTMLInputElement).files) {
        const file: File = (event.target as HTMLInputElement).files![0];
        if (file) {

          const reader = new FileReader();
          reader.readAsDataURL(file);
          reader.onload = () => {

            // Eliminar el prefijo data:[<mediatype>][;base64], de la cadena Base64
            const base64String = (reader.result as string).split(',')[1];

            const archivoCargaExcel = {
              archivoBase64: base64String,
              archivoNombre: file.name,
              archivoTipoMime: file.type
            } as unknown as ArchivoCarga;

            this.loadingSpinnerService.openSpinner();
            this.archivoService.subirArchivoCargaMasivaCodigosPostales(archivoCargaExcel).subscribe({
              next: () => {
                this.mensajeService.modalExito('Estados cargados correctamente');
                this.consultarGrid();
                this.loadingSpinnerService.closeSpinner();
               
              },
              error: (err) => {
                console.error('Error al cargar los estados:', err);
                this.loadingSpinnerService.closeSpinner();              }
            });
          }

   
        }
      }
    };

    input.click();
  }


  protected descargarPlantilla(){
    this.archivoService.descargarPlantillaCargaMasivaCodigosPostales().subscribe((data) => {
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(data);
      a.href = objectUrl;
      a.download = 'PlantillaCargaMasivaCodigosPostales.xlsx';
      a.click();
      URL.revokeObjectURL(objectUrl);
    });
}

}
