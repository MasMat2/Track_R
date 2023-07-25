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
import { Observable } from 'rxjs';
import { CrudBase } from '../../../../shared/components/crud/crud-base/crud-base';
import { MunicipioFormularioComponent } from './municipio-formulario/municipio-formulario.component';

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
}
