import { Component, OnInit } from '@angular/core';
import { EstadoGridDto } from '@dtos/catalogo/estado-grid-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ACCESO_ESTADO } from '@utils/codigos-acceso/catalogo.accesos';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subject, tap } from 'rxjs';
import { EstadoFormularioComponent } from './estado-formulario/estado-formulario.component';
import { ArchivoService } from '@http/catalogo/archivo.service';
import { LoadingSpinnerService } from '@services/loading-spinner.service';
import { ArchivoCarga } from '@dtos/archivos/archivo-carga';

@Component({
  templateUrl: 'estado.component.html',
})
export class EstadoComponent extends CrudBase<EstadoGridDto> implements OnInit {
  protected readonly HEADER_GRID: string = 'Estados';

  private destroy$: Subject<void> = new Subject<void>();

  public readonly NOMBRE_ENTIDAD: string = "Estado";

  override crudConfig: ICrudConfig =
  {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: "masc",
    nombrePropiedadId: "idEstado",
    formConfig: {
      ComponenteFormulario: EstadoFormularioComponent,
      modalConfig: MODAL_CONFIG.Default,
      configAgregar: {
          acceso: ACCESO_ESTADO.Agregar,
      },
      configEditar: {
          acceso: ACCESO_ESTADO.Editar,
      }
    },
    configEliminar: {
      acceso: ACCESO_ESTADO.Eliminar,
      elementToString: (estado: EstadoGridDto) => estado.nombre
    }
  };

  // Grid
  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'clave', minWidth: 150, width: 70 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
    { headerName: 'País', field: 'nombrePais', minWidth: 150 },
  ];

  constructor(
    private estadoService: EstadoService,
    accesoService: AccesoService,
    modalService: BsModalService,
    mensajeService: MensajeService,
    private archivoService: ArchivoService,
    private loadingSpinnerService : LoadingSpinnerService
  ) {
    super(
      accesoService,
      modalService,
      mensajeService
    );
  }

  public ngOnInit(): void {
    super.onInit();
  }

  protected override consultarGrid(): Observable<EstadoGridDto[]> {
    return this.estadoService.consultarParaGrid();
  }

  protected override eliminar(idEstado: number): Observable<void> {
    return this.estadoService.eliminar(idEstado);
  }

  protected descargarPlantillaCargaMasiva(): void {
    this.archivoService.descargarPlantillaCargaMasivaEstados().subscribe((data) => {
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(data);
      a.href = objectUrl;
      a.download = 'PlantillaCargaMasivaEstados.xlsx';
      a.click();
      URL.revokeObjectURL(objectUrl);
    })
  }

  protected sincronizarPlantilla(){    
    this.loadingSpinnerService.openSpinner();
    this.estadoService.sincronizarEstadosExcel().subscribe(() => {
      this.loadingSpinnerService.closeSpinner();
      this.mensajeService.modalExito('Estados sincronizados correctamente');
    });
  }

  protected subirPlantillaExcel(){
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
            this.archivoService.subirArchivoCargaMasivaEstados(archivoCargaExcel).subscribe({
              next: () => {
                this.mensajeService.modalExito('Estados cargados correctamente');
                this.consultarGrid().pipe(
                  tap(() => this.loadingSpinnerService.closeSpinner())
                ).subscribe();
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

  }
