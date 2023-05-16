import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EntidadService } from '@http/gestion-entidad/entidad.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Entidad } from '@models/gestion-entidad/entidad';
import { EncryptionService } from '@services/encryption.service';
import { ICatalogoConfig } from '@sharedComponents/crud/catalogo-config';
import { CrudBase } from '@sharedComponents/crud/components/crud-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ConfiguracionEntidadFormularioComponent } from './configuracion-entidad-formulario/configuracion-entidad-formulario.component';

@Component({
  selector: 'app-configuracion-entidad',
  templateUrl: './configuracion-entidad.component.html',
  styleUrls: ['./configuracion-entidad.component.scss']
})
export class ConfiguracionEntidadComponent extends CrudBase<Entidad> implements OnInit {

  public readonly nombreEntidad: string = "Entidad";
  public columns = [
    { headerName: 'Clave', field: 'clave', minWidth: 150 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 }
  ];

  public catalogoConfig: ICatalogoConfig = {
    children: this.columns,
    titulo: 'Entidad'
  };

  constructor(
    private entidadService: EntidadService,
    accesoService: AccesoService,
    bsModalRef: BsModalRef,
    bsModalService: BsModalService,
    encryptionService: EncryptionService,
		mensajeService: MensajeService,
		router: Router,
  )
  {
    super(
      entidadService,
      accesoService,
      bsModalRef,
      bsModalService,
      encryptionService,
      mensajeService,
      router
    );
  }

  ngOnInit() {
    this.crudConfig =
    {
      nombreEntidad: this.nombreEntidad,
      generoGramatical: "fem",
      nombrePropiedadId: "idEntidad",
      formConfig: {
        type: "modal",
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

    super.onInit();
  }

  protected consultarGrid(): void {
    this.entidadService.consultarTodosParaGrid().subscribe((data) => {
      this.elementos = data;
    });
  }

}
