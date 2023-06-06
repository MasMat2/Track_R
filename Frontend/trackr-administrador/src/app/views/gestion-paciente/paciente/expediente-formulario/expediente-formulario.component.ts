import { ActivatedRoute, Router } from '@angular/router';
import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { TabDirective } from 'ngx-bootstrap/tabs';
import * as moment from 'moment';

import { EncryptionService } from '@services/encryption.service';



import { GeneralConstant } from '@utils/general-constant';


import { ExternalTemplate } from '@sharedComponents/tabulador-entidad/external-template';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ExpedienteGeneralFormularioComponent } from '../expediente-general-formulario/expediente-general-formulario.component';

@Component({
  selector: 'app-expediente-formulario',
  templateUrl: './expediente-formulario.component.html',
  styleUrls: ['./expediente-formulario.component.scss']
})
export class ExpedienteFormularioComponent implements OnInit, AfterViewInit {

  @ViewChild(ExpedienteGeneralFormularioComponent, { static: false }) public informacionGeneral: ExpedienteGeneralFormularioComponent;

  /**
   * Referencias a templates de los componentes externos a utilizar en el tabulador de entidades.
   */
  @ViewChild('informacionGeneral', { static: false }) private informacionGeneralTpl : TemplateRef<any>;
  @ViewChild('viaje', { static: false }) private viajeTpl : TemplateRef<any>;

  /**
   * Configuracion para el uso de componentes externos
   */
  public externalTemplates: ExternalTemplate[] = [];

  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'El expediente administrativo ha sido agregado.';
  public mensajeEditar = 'El expediente administrativo ha sido modificado.';
  public claveEntidadExpedienteAdministrativo = GeneralConstant.ClaveEntidadExpedienteAdministrativo;
  // public expedienteAdministrativo = new ExpedienteAdministrativo();
  // public expedienteAdministrativoMercancia: ExpedienteAdministrativoMercancia[] = [];
  public configDate = GeneralConstant.CONFIG_DATEPICKER;
  public fechaAux: Date;
  public fechaLlegada: Date;
  public esExpedienteViaje = false;
  public navegacion: boolean = false;
  public idExpedienteAdministrativo: number;

  public value = 'Información General';

  constructor(
    private encryptionService: EncryptionService,
    // private expedienteAdministrativoService: ExpedienteAdministrativoService,
    private mensajeService: MensajeService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (this.encryptionService.readUrlParams(params).a == 'esAgregar') {
        this.accion = GeneralConstant.COMPONENT_ACCION_AGREGAR;
        this.esExpedienteViaje = false;
      } else {
        this.accion = GeneralConstant.COMPONENT_ACCION_EDITAR;
        const idExpedienteAdministrativo = this.encryptionService.readUrlParams(params).i;
        this.idExpedienteAdministrativo = idExpedienteAdministrativo;
        // this.expedienteAdministrativoService.consultar(idExpedienteAdministrativo).subscribe((data: any) => {
        //   this.cargarInformacion(data);
        // });
      }
    });
  }

  public ngAfterViewInit(): void {

    let informacionGeneral : ExternalTemplate = {
      template : this.informacionGeneralTpl,
      label : 'Información General',
      enabled : true,
      externalSubmit : true,
      submitControl : false
    };


    this.externalTemplates.push(informacionGeneral);
  }


}
