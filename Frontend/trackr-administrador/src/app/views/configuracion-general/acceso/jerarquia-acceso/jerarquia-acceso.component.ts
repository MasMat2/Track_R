import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccesoService } from '@http/seguridad/acceso.service';
import { JerarquiaAccesoService } from '@http/seguridad/jerarquiaAcceso.service';
import { JerarquiaAcceso } from '@models/seguridad/jerarquia-acceso';
import { EncryptionService } from '@services/encryption.service';
import { ICatalogoConfig } from '@sharedComponents/crud/catalogo-config';
import { CrudBase } from '@sharedComponents/crud/components/crud-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { JerarquiaAccesoFormularioComponent } from './jerarquia-acceso-formulario/jerarquia-acceso-formulario.component';

@Component({
  selector: 'app-jerarquia-acceso',
  templateUrl: './jerarquia-acceso.component.html',
  styleUrls: ['./jerarquia-acceso.component.scss']
})
export class JerarquiaAccesoComponent extends CrudBase<JerarquiaAcceso> implements OnInit {

  public readonly nombreEntidad: string = "Jerarquía de Acceso";
  public columns = [
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
    { headerName: 'Tipo Compañía', field: 'nombreTipoCompania', minWidth: 150 }
  ];

  public catalogoConfig: ICatalogoConfig = {
    children: this.columns,
    titulo: 'Jerarquía de Acceso'
  };

  constructor(
    private jerarquiaAccesoService: JerarquiaAccesoService,
    accesoService: AccesoService,
		bsModalRef: BsModalRef,
		bsModalService: BsModalService,
		encryptionService: EncryptionService,
		mensajeService: MensajeService,
		router: Router,
  )
  {
    super(
      jerarquiaAccesoService,
      accesoService,
      bsModalRef,
      bsModalService,
      encryptionService,
      mensajeService,
      router);
  }

  ngOnInit() {
    this.crudConfig =
    {
      nombreEntidad: this.nombreEntidad,
      generoGramatical: "fem",
      nombrePropiedadId: "idJerarquiaAcceso",
      formConfig: {
        type: "modal",
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

    super.onInit();
  }

  protected consultarGrid(): void {
    this.jerarquiaAccesoService.consultarParaGrid().subscribe((data) => {
      this.elementos = data;
    });
  }

}
