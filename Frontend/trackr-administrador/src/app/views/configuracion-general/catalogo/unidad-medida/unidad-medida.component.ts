import { Component, OnInit } from '@angular/core';
import { UnidadMedidaGridDto } from '@dtos/catalogo/unidad-medida-grid-dto';
import { UnidadMedidaService } from '@http/catalogo/unidad-medida.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ACCESO_UNIDAD_MEDIDA } from '@utils/codigos-acceso/catalogo.accesos';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { UnidadMedidaFormularioComponent } from './unidad-medida-formulario/unidad-medida-formulario.component';

@Component({
  selector: 'app-unidad-medida',
  templateUrl: './unidad-medida.component.html'
})
export class UnidadMedidaComponent extends CrudBase<UnidadMedidaGridDto> implements OnInit{

 public readonly NOMBRE_ENTIDAD: string = "Unidad de Medida";

 override crudConfig: ICrudConfig = {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: "fem",
    nombrePropiedadId: "idUnidadMedida",
    formConfig: {
      ComponenteFormulario: UnidadMedidaFormularioComponent,
      modalConfig: MODAL_CONFIG.Default,
      configAgregar: {
        acceso: ACCESO_UNIDAD_MEDIDA.Agregar,
      },
      configEditar: {
        acceso: ACCESO_UNIDAD_MEDIDA.Editar,
      }
    },
    configEliminar: {
      acceso: ACCESO_UNIDAD_MEDIDA.Eliminar,
      elementToString: (unidadMedida: UnidadMedidaGridDto) => unidadMedida.nombre
    }
  };

  // Grid
  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'idUnidadMedida', maxWidth: 150 },
    { headerName: 'Unidad de Medida', field: 'nombre', minWidth: 600 }
  ];

  constructor(
    private unidadMedidaService : UnidadMedidaService,
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

  public ngOnInit(): void {
    super.onInit();
  }


  protected override consultarGrid(): Observable<UnidadMedidaGridDto[]> {
    return this.unidadMedidaService.consultarParaGrid();
  }
  protected override eliminar(idUnidadMedida: number): Observable<void> {
    return this.unidadMedidaService.eliminar(idUnidadMedida);
  }


}
