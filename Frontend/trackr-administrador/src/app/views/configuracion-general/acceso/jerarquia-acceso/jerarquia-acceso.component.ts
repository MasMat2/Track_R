import { Component, OnInit } from '@angular/core';
import { AccesoService } from '@http/seguridad/acceso.service';
import { JerarquiaAccesoService } from '@http/seguridad/jerarquiaAcceso.service';
import { JerarquiaAcceso } from '@models/seguridad/jerarquia-acceso';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { JerarquiaAccesoFormularioComponent } from './jerarquia-acceso-formulario/jerarquia-acceso-formulario.component';
import { ColDef } from 'ag-grid-community';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';

@Component({
  selector: 'app-jerarquia-acceso',
  templateUrl: './jerarquia-acceso.component.html',
  styleUrls: ['./jerarquia-acceso.component.scss']
})
export class JerarquiaAccesoComponent extends CrudBase<JerarquiaAcceso> implements OnInit {

  private readonly NOMBRE_ENTIDAD: string = "Jerarquía de Acceso";

  protected override crudConfig: ICrudConfig = {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: "fem",
    nombrePropiedadId: "idJerarquiaAcceso",
    formConfig: {
      ComponenteFormulario: JerarquiaAccesoFormularioComponent,
      modalConfig: GeneralConstant.CONFIG_MODAL_FULL,
      configAgregar: {
          acceso: CodigoAcceso.AGREGAR_JERARQUIA_ACCESO,
      },
      configEditar: {
          acceso: CodigoAcceso.EDITAR_JERARQUIA_ACCESO,
      }
    },
    configEliminar: {
      acceso: CodigoAcceso.ELIMINAR_JERARQUIA_ACCESO,
      elementToString: (jerarquiaAcceso: JerarquiaAcceso) => jerarquiaAcceso.nombre
    }
  };

  protected columns: ColDef[] = [
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
    { headerName: 'Tipo Compañía', field: 'nombreTipoCompania', minWidth: 150 }
  ];

  constructor(
    private jerarquiaAccesoService: JerarquiaAccesoService,
    accesoService: AccesoService,
		bsModalService: BsModalService,
		mensajeService: MensajeService,
  )
  {
    super(
      accesoService,
      bsModalService,
      mensajeService);
  }

  ngOnInit() {
    super.onInit();
  }

  protected consultarGrid(): Observable<JerarquiaAcceso[]> {
    return this.jerarquiaAccesoService.consultarParaGrid();
  }

  protected eliminar(idJerarquia: number): Observable<void> {
    return this.jerarquiaAccesoService.eliminar(idJerarquia);
  }

}
