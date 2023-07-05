import { lastValueFrom } from 'rxjs';
import { ExpedientePadecimientoService } from '@http/seguridad/expediente-padecimiento.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { TabDirective } from 'ngx-bootstrap/tabs';
import * as moment from 'moment';

import { EncryptionService } from '@services/encryption.service';



import { GeneralConstant } from '@utils/general-constant';


import { ExternalTemplate } from '@sharedComponents/tabulador-entidad/external-template';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ExpedienteGeneralFormularioComponent } from '../expediente-general-formulario/expediente-general-formulario.component';
import { ExpedientePadecimientoGridDTO } from '@dtos/gestion-expediente/expediente-padecimiento-grid-dto';
import { ExpedientePadecimientoDTO } from '@dtos/seguridad/expediente-padecimiento-dto';

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
  @ViewChild('estudios', { static: false }) private estudiosTpl : TemplateRef<any>;
  @ViewChild('padecimientoTpl', { static: false }) private padecimientosTpl : TemplateRef<any>;
  public padecimientosList: ExpedientePadecimientoDTO[] = [];

  /**
   * Configuracion para el uso de componentes externos
   */
  public externalTemplates: ExternalTemplate[] = [];

  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'El expediente administrativo ha sido agregado.';
  public mensajeEditar = 'El expediente administrativo ha sido modificado.';
  public claveEntidadExpedienteTrackr = GeneralConstant.ClaveEntidadExpedienteTrackr;
  public configDate = GeneralConstant.CONFIG_DATEPICKER;
  public fechaAux: Date;
  public fechaLlegada: Date;
  public esExpedienteViaje = false;
  public navegacion: boolean = false;
  public idUsuario: number;

  public value = 'Información General';

  constructor(
    private encryptionService: EncryptionService,
    private mensajeService: MensajeService,
    private route: ActivatedRoute,
    private router: Router,
    private expedientePadecimientoService: ExpedientePadecimientoService
  ) { }

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (this.encryptionService.readUrlParams(params).a == 'esAgregar') {
        this.accion = GeneralConstant.COMPONENT_ACCION_AGREGAR;
        this.esExpedienteViaje = false;
      } else {
        this.accion = GeneralConstant.COMPONENT_ACCION_EDITAR;
        const idUsuario = this.encryptionService.readUrlParams(params).i;
        this.idUsuario = idUsuario;
      }
    });
  }

  public async ngAfterViewInit() {

    let informacionGeneral : ExternalTemplate = {
      template : this.informacionGeneralTpl,
      label : 'Información General',
      enabled : true,
      externalSubmit : true,
      submitControl : false
    };

    
    let estudios : ExternalTemplate = {
      template : this.estudiosTpl,
      label : 'Estudios',
      enabled : this.idUsuario != null ? true : false,
      externalSubmit : true,
      submitControl : false
    };
    
    this.externalTemplates.push(informacionGeneral);
    
    await this.consultarPadecimientos();
    
    this.padecimientosList.forEach((padecimiento: ExpedientePadecimientoDTO) => {
      console.log(padecimiento);
      let padecimientoExtTpl : ExternalTemplate = {
        template : this.padecimientosTpl,
        label : padecimiento.nombrePadecimiento,
        enabled : this.idUsuario != null ? true : false,
        externalSubmit : true,
        submitControl : false
      };
      this.externalTemplates.push(padecimientoExtTpl);
    });


    this.externalTemplates.push(estudios);
  }

  
  public async consultarPadecimientos() {
    await lastValueFrom(this.expedientePadecimientoService.consultarPorUsuario(this.idUsuario))
      .then((padecimientos) => {
        this.padecimientosList = padecimientos;
    });
  }

}
