import { Component, OnInit } from '@angular/core';
import { EntidadService } from '@http/gestion-entidad/entidad.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Entidad } from '@models/gestion-entidad/entidad';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { ConfiguracionEntidadFormularioComponent } from './configuracion-entidad-formulario/configuracion-entidad-formulario.component';

@Component({
  selector: 'app-configuracion-entidad',
  templateUrl: './configuracion-entidad.component.html',
  styleUrls: ['./configuracion-entidad.component.scss']
})
export class ConfiguracionEntidadComponent extends CrudBase<Entidad> implements OnInit {

  private readonly NOMBRE_ENTIDAD: string = "Entidad";

  protected override crudConfig: ICrudConfig = {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: "fem",
    nombrePropiedadId: "idEntidad",
    formConfig: {
      ComponenteFormulario: ConfiguracionEntidadFormularioComponent,
      modalConfig: GeneralConstant.CONFIG_MODAL_FULL,
      configAgregar: {
          acceso: CodigoAcceso.AGREGAR_ENTIDAD,
      },
      configEditar: {
          acceso: CodigoAcceso.EDITAR_ENTIDAD,
      }
    },
    configEliminar: {
      acceso: CodigoAcceso.ELIMINAR_ENTIDAD,
      elementToString: (entidad: Entidad) => entidad.nombre
    }
  };

  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'clave', minWidth: 150 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 }
  ];

  constructor(
    private entidadService: EntidadService,
    accesoService: AccesoService,
    bsModalService: BsModalService,
		mensajeService: MensajeService,
  )
  {
    super(
      accesoService,
      bsModalService,
      mensajeService
    );
  }

  ngOnInit() {
    super.onInit();
  }

  protected consultarGrid(): Observable<Entidad[]> {
    return this.entidadService.consultarTodosParaGrid();
  }

  protected actualizar(): void {
    this.entidadService.actualizarExpedienteTrackr().subscribe({
      complete: () => {
        this.mensajeService.modalExito('Entidad Actualizada');
      }
    });
  }

  protected eliminar(idEntidad: number): Observable<void> {
    return this.entidadService.eliminar(idEntidad);
  }

}
