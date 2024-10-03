import { Component, OnInit } from '@angular/core';
import { EstadoGridDto } from '@dtos/catalogo/estado-grid-dto';
import { MunicipioGridDto } from '@dtos/catalogo/municipio-grid-dto';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ACCESO_MUNICIPIO } from '@utils/codigos-acceso/catalogo.accesos';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable, tap } from 'rxjs';
import { CrudBase } from '../../../../shared/components/crud/crud-base/crud-base';
import { MunicipioFormularioComponent } from './municipio-formulario/municipio-formulario.component';
import { ArchivoService } from '@http/catalogo/archivo.service';
import { LoadingSpinnerService } from '@services/loading-spinner.service';
import { ArchivoCarga } from '@dtos/archivos/archivo-carga';

@Component({
  templateUrl: 'municipio.component.html'
})
export class MunicipioComponent extends CrudBase<MunicipioGridDto> implements OnInit {

  private readonly NOMBRE_ENTIDAD: string = "Municipio";

  protected override crudConfig: ICrudConfig =
  {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: "masc",
    nombrePropiedadId: "idMunicipio",
    formConfig: {
      ComponenteFormulario: MunicipioFormularioComponent,
      modalConfig: MODAL_CONFIG.Default,
      configAgregar: {
          acceso: ACCESO_MUNICIPIO.Agregar,
      },
      configEditar: {
          acceso: ACCESO_MUNICIPIO.Editar,
      }
    },
    configEliminar: {
      acceso: ACCESO_MUNICIPIO.Eliminar,
      elementToString: (estado: EstadoGridDto) => estado.nombre
    }
  };

  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'clave', minWidth: 80, maxWidth: 80 },
    { headerName: 'Pais', field: 'nombrePais', minWidth: 150 },
    { headerName: 'Estado', field: 'nombreEstado', minWidth: 150 },
    { headerName: 'Municipio', field: 'nombre', minWidth: 150 },
  ];

  constructor(
    private municipioService: MunicipioService,
    accesoService: AccesoService,
    modalService: BsModalService,
    mensajeService: MensajeService,
    private archivoService : ArchivoService,
    private loadingSpinnerService: LoadingSpinnerService
  ) {
    super(
      accesoService,
      modalService,
      mensajeService
    );
  }

  ngOnInit(): void {
    super.onInit();
  }

  protected consultarGrid(): Observable<MunicipioGridDto[]> {
    return this.municipioService.consultarParaGrid();
  }

  protected eliminar(idMunicipio: number): Observable<void> {
    return this.municipioService.eliminar(idMunicipio);
  }

  protected sincronizarPlantilla(){
      this.municipioService.sincronizarMunicipiosExcel().subscribe(() => {
        this.consultarGrid().subscribe((data) => {
          this.loadingSpinnerService.closeSpinner();
          this.mensajeService.modalExito("Municipios sincronizados correctamente");
        });
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
            this.archivoService.subirArchivoCargaMasivaMunicipios(archivoCargaExcel).subscribe({
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

  

  protected descargarPlantilla(){
      this.archivoService.descargarPlantillaCargaMasivaMunicipios().subscribe((data) => {
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(data);
        a.href = objectUrl;
        a.download = 'PlantillaCargaMasivaMunicipios.xlsx';
        a.click();
        URL.revokeObjectURL(objectUrl);
      });
  }


}
