import { lastValueFrom } from 'rxjs';
import { ExpedientePadecimientoService } from '@http/seguridad/expediente-padecimiento.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AfterContentInit, AfterViewInit, Component, ComponentFactoryResolver, OnInit, QueryList, TemplateRef, ViewChild, ViewChildren, ViewContainerRef } from '@angular/core';
import { TabDirective } from 'ngx-bootstrap/tabs';
import * as moment from 'moment';

import { EncryptionService } from '@services/encryption.service';

import { GeneralConstant } from '@utils/general-constant';

import { ExternalTemplate } from '@sharedComponents/tabulador-entidad/external-template';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ExpedienteGeneralFormularioComponent } from '../expediente-general-formulario/expediente-general-formulario.component';
import { ExpedientePadecimientoGridDTO } from '@dtos/gestion-expediente/expediente-padecimiento-grid-dto';
import { ExpedientePadecimientoDTO } from '@dtos/seguridad/expediente-padecimiento-dto';
import { DashboardPadecimientoComponent } from '../dashboard-padecimiento/dashboard-padecimiento.component';
import { ExpedienteEstudioComponent } from '../expediente-estudio/expediente-estudio.component';
import { ExpedientePadecimientoComponent } from '../expediente-padecimiento/expediente-padecimiento.component';
import { ExpedienteRecomendacionComponent } from '../expediente-recomendacion/expediente-recomendacion.component';
import { ExpedienteTratamientoComponent } from '../expediente-tratamiento/expediente-tratamiento.component';

@Component({
  selector: 'app-expediente-formulario',
  templateUrl: './expediente-formulario.component.html',
  styleUrls: ['./expediente-formulario.component.scss']
})
export class ExpedienteFormularioComponent implements OnInit, AfterContentInit {

  @ViewChild(ExpedienteGeneralFormularioComponent, { static: false }) public informacionGeneral: ExpedienteGeneralFormularioComponent;

  public padecimientosList: ExpedientePadecimientoDTO[] = [];

  /**
   * Configuracion para el uso de componentes externos
   */
  public externalTemplates: ExternalTemplate[] = [];

  public accion: string;
  public claveEntidadExpedienteTrackr = GeneralConstant.ClaveEntidadExpedienteTrackr;
  public idUsuario: number;

  public value = 'Información General';

  constructor(
    private encryptionService: EncryptionService,
    private route: ActivatedRoute,
    private expedientePadecimientoService: ExpedientePadecimientoService
  ) { }

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (this.encryptionService.readUrlParams(params).a == 'esAgregar') {
        this.accion = GeneralConstant.COMPONENT_ACCION_AGREGAR;
      } else {
        this.accion = GeneralConstant.COMPONENT_ACCION_EDITAR;
        const idUsuario = this.encryptionService.readUrlParams(params).i;
        this.idUsuario = idUsuario;
      }
    });
  }

  public async ngAfterContentInit() {
    this.agregarTabInformacionGeneral();
    this.agregarTabPadecimientos();
    this.agregarTabEstudios();
    await this.agregarTabsDashboardPadecimiento();
    this.agregarTabTratamientos();
    this.agregarTabRecomendaciones();
  }

  private agregarTabInformacionGeneral(): void {
    const informacionGeneral : ExternalTemplate = {
      component : ExpedienteGeneralFormularioComponent,
      args : {
        idUsuario: this.idUsuario,
      },
      label : 'General',
      enabled : true,
      externalSubmit : true,
      submitControl : false
    };

    this.externalTemplates.push(informacionGeneral);
  }

  private agregarTabPadecimientos(): void {
    const padecimientos : ExternalTemplate = {
      component : ExpedientePadecimientoComponent,
      args : { },
      label : 'Padecimientos',
      enabled : this.idUsuario != null ? true : false,
      externalSubmit : true,
      submitControl : false
    };

    this.externalTemplates.push(padecimientos);
  }

  private agregarTabEstudios(): void {
    const estudios : ExternalTemplate = {
      component : ExpedienteEstudioComponent,
      label : 'Estudios',
      args : {
        idUsuario: this.idUsuario,
      },
      enabled : this.idUsuario != null ? true : false,
      externalSubmit : true,
      submitControl : false
    };

    this.externalTemplates.push(estudios);
  }

  private async agregarTabsDashboardPadecimiento(): Promise<void> {
    await this.consultarPadecimientos();

    this.padecimientosList.forEach((padecimiento: ExpedientePadecimientoDTO) => {
      const padecimientoExtTpl: ExternalTemplate = {
        component: DashboardPadecimientoComponent,
        args: {
          idPadecimiento: padecimiento.idPadecimiento,
          idUsuario: this.idUsuario,
          nombrePadecimiento: padecimiento.nombrePadecimiento,
        },
        label: padecimiento.nombrePadecimiento,
        enabled: this.idUsuario != null ? true : false,
        externalSubmit: true,
        submitControl: false
      };

      this.externalTemplates.push(padecimientoExtTpl);
    });
  }

  private agregarTabTratamientos(): void {
    const tratamientos : ExternalTemplate = {
      component : ExpedienteTratamientoComponent,
      label : 'Tratamientos',
      args : { },
      enabled : this.idUsuario != null ? true : false,
      externalSubmit : true,
      submitControl : false
    };

    this.externalTemplates.push(tratamientos);
  }

  private agregarTabRecomendaciones() {
    const recomendaciones: ExternalTemplate = {
      component: ExpedienteRecomendacionComponent,
      args: { },
      label : 'Recomendaciones',
      enabled : this.idUsuario != null ? true : false,
      externalSubmit : true,
      submitControl : false
    };

    this.externalTemplates.push(recomendaciones);
  }

  public async consultarPadecimientos() {
    await lastValueFrom(this.expedientePadecimientoService.consultarPorUsuario(this.idUsuario))
      .then((padecimientos) => {
        this.padecimientosList = padecimientos;
    });
  }

}
